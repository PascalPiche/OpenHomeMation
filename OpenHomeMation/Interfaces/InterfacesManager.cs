﻿
using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Interfaces
{

    public class InterfacesManager : IInterfacesManager
    {

        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private IPluginsManager _pluginsMng;
        private IDataStore _data;
        private IDataDictionary _dataRegisteredInterfaces;
        private IList<IInterface> _runningInterfaces = new ObservableCollection<IInterface>();
        private Dictionary<String, IInterface> _runningDic = new Dictionary<string, IInterface>();

        #region "Public Property"

        public IList<IInterface> RunnableInterfaces
        {
            get { return _runningInterfaces; }
        }

        #endregion

        #region Ctor

        public InterfacesManager(ILoggerManager loggerMng, IPluginsManager pluginsMng)
        {
            _loggerMng = loggerMng;
            _pluginsMng = pluginsMng;
        }

        #endregion

        #region "Public api"

        public bool Init(IDataStore data, IOhmSystem system)
        {
            _data = data;
            _logger = _loggerMng.GetLogger("InterfacesManager");
            _dataRegisteredInterfaces = _data.GetDataDictionary("RegisteredInterfaces");
            if (_dataRegisteredInterfaces == null)
            {
                _dataRegisteredInterfaces = new DataDictionary();
                _data.StoreDataDictionary("RegisteredInterfaces", _dataRegisteredInterfaces);
                _data.Save();
            }
            loadRegisteredInterfaces(system);
            return true;
        }

        public bool RegisterInterface(string key, IPlugin plugin, IOhmSystem system)
        {
            bool result = false;
            //Detect if already created
            IDataDictionary _interfaceData = _dataRegisteredInterfaces.GetDataDictionary(key);
            if (_interfaceData != null)
            {
                //Interface already registered ???
                _logger.Warn("Cannot register interface " + key + ", Key already used");
            }
            else 
            {
                try {
                    IInterface newInterface = CreateInterface(key, plugin, system);
                    if (newInterface == null)
                    {
                        _logger.Error("Cannot register interface " + key + ", Error on creating Interface");
                    }
                    else
                    {
                        _interfaceData = CreateInterfaceData(key, plugin);

                        _dataRegisteredInterfaces.StoreDataDictionary(key, _interfaceData);

                        result = _data.Save();
                        if (result)
                        {
                            _logger.Info("Registered interfaces : " + key + " with plugin " + plugin.Name);
                        }
                    }
                } 
                catch (Exception ex)
                {
                    _logger.Error("Cannot register interface " + key + ", Error on creating Interface", ex);
                }
            }
            
            return result;
        }

        public bool StartInterface(string key)
        {
            bool result = false;
            IInterface interf = null;
            if (_runningDic.TryGetValue(key, out interf))
            {
                if (interf.State == InterfaceState.Disabled)
                {
                    interf.Start();
                    result = true;
                }
            }
            return result;
        }

        public bool StopInterface(string key)
        {
            bool result = false;
            IInterface interf = null;
            if (_runningDic.TryGetValue(key, out interf))
            {
                if (interf.State == InterfaceState.Enabled)
                {
                    interf.Shutdown();
                    result = true;
                }
            }
            return result;
        }

        public bool ExecuteCommand(string interfaceKey, string commandKey, Dictionary<string, object> arguments)
        {
            //Find interface
            IInterface interf = GetRunningInterface(interfaceKey);
            if (interf != null)
            {
                return interf.ExecuteCommand(commandKey, arguments);
            }
            return false;
        }

        public bool CanExecuteCommand(string interfaceKey, string commandKey)
        {
            IInterface interf = GetRunningInterface(interfaceKey);
            if (interf != null)
            {
                return interf.CanExecuteCommand(commandKey);
            }
            return false;
        }

        #endregion

        #region "Private"

        private void loadRegisteredInterfaces(IOhmSystem system)
        {
            foreach (var key in _dataRegisteredInterfaces.GetKeys())
            {
                try
                {
                    CreateInterface(key, system);
                }
                catch (Exception ex)
                {
                    _logger.Error("Cannot create interface : " + key, ex);
                }
            }
       }

        private IPlugin GetPluginForInterface(string key)
        {
            IPlugin result = null;
            _logger.Info("Loading interface : " + key);
            IDataDictionary _dataPlugin = _dataRegisteredInterfaces.GetDataDictionary(key);
            String id = "";
            if (_dataPlugin == null)
            {

            }
            else
            {
                id = _dataPlugin.GetString("PluginId");
                result = _pluginsMng.GetPlugin(new Guid(id));
            }
            return result;
        }

        private InterfaceAbstract CreateInterface(string key, IOhmSystem system)
        {
            return CreateInterface(key, GetPluginForInterface(key), system);
        }

        private string GetInterfaceLoggerKey(string key, IPlugin plugin)
        {
            return plugin.Id.ToString() + '.' + key;
        }

        private InterfaceAbstract CreateInterface(string key, IPlugin plugin, IOhmSystem system)
        {
            InterfaceAbstract result = null;

            if (plugin != null)
            {
                var interfaceLogger = _loggerMng.GetLogger(GetInterfaceLoggerKey(key, plugin));
                result = plugin.CreateInterface(key, interfaceLogger);
                
                if (result != null)
                {
                    result.Init(system.GetInterfaceGateway(result));
                    _runningInterfaces.Add(result);
                    _runningDic.Add(key, result);
                }
            }

            return result;
        }

        private IDataDictionary CreateInterfaceData(string key, IPlugin plugin) {
            var result = new DataDictionary();

            result.StoreString("PluginId", plugin.Id.ToString());

            return result;
        }

        private IInterface GetRunningInterface(string key)
        {
            IInterface result;

            if (!_runningDic.TryGetValue(key, out result))
            {
                result = null;
            }
            return result;
        }

        #endregion

    }
}
