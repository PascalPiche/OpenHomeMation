using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.RAL
{

    public class InterfacesManager : IInterfacesManager
    {

        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private IPluginsManager _pluginsMng;
        private IDataStore _data;
        private IDataDictionary _dataRegisteredInterfaces;
        private IList<IInterface> _runningInterfaces = new ObservableCollection<IInterface>();
        private Dictionary<string, IInterface> _runningDic = new Dictionary<string, IInterface>();
        private IOhmSystemInternal _system;

        #region Public Properties

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

        #region Public API

        public bool Init(IDataStore data, IOhmSystemInternal system)
        {
            _data = data;
            _logger = _loggerMng.GetLogger("InterfacesManager");
            _logger.Debug("Initing");
            _dataRegisteredInterfaces = _data.GetOrCreateDataDictionary("RegisteredInterfaces");
            _data.Save();
            _system = system;
            loadRegisteredInterfaces(system);
            _logger.Debug("Inited");
            return true;
        }

        public bool RegisterInterface(string key, IPlugin plugin)
        {
            bool result = false;
            IDataDictionary _interfaceData = _dataRegisteredInterfaces.GetOrCreateDataDictionary(key);
            
            try {
                IInterface newInterface = CreateInterface(key, plugin, _system);
                if (newInterface == null)
                {
                    _logger.Error("Cannot register interface " + key + ", Error on creating Interface");
                }
                else
                {
                    _interfaceData = CreateInterfaceData(key, plugin);
                    result = _data.Save();

                    if (result)
                    {
                        _logger.Info("Registered interfaces : " + key + " with plugin " + plugin.Name);
                    }
                }
            } 
            catch (Exception ex)
            {
                _logger.Error("Cannot register interface " + key + ", Unhandled exception on creating Interface", ex);
            }
            
            return result;
        }

        public bool UnRegisterInterface(String key, IPlugin plugin)
        {
            bool result = false;

            if (_runningDic.ContainsKey(key))
            {
                //Interface is running
                _runningDic[key].Shutdowning();
                _runningInterfaces.Remove(_runningDic[key]);
                _runningDic.Remove(key);
            }

            IDataDictionary _interfaceData = _dataRegisteredInterfaces.GetOrCreateDataDictionary(key);
            

            if (_interfaceData != null)
            {
                result = _dataRegisteredInterfaces.RemoveKey(key);
            }
            else
            {
                _logger.Warn("Cannot uninstall interface " + plugin.Id + ": Interace Not found");
            }
            _data.Save();
            return result;
            
        }

        public bool StartInterface(string key)
        {
            bool result = false;
            IInterface interf = null;
            if (_runningDic.TryGetValue(key, out interf))
            {
                result = StartInterface(interf);
            }
            return result;
        }

        public bool StopInterface(string key)
        {
            bool result = false;
            IInterface interf = null;
            if (_runningDic.TryGetValue(key, out interf))
            {
                if (interf.InterfaceState == InterfaceStates.Enabled)
                {
                    interf.Shutdowning();
                    result = true;
                }
            }
            return result;
        }

        public bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, string> arguments)
        {
            _logger.Debug("Executing Command -> Node Key : " + nodeKey + " -> Command Key : " + commandKey);

           

            //Find interface
            IInterface interf = GetRunningInterface(nodeKey);


            if (interf != null)
            {
                return interf.ExecuteCommand(nodeKey, commandKey, arguments);
            }
            else
            {
                _logger.Debug("Interface for nodeKey (" + nodeKey + ") was not found while executing command " + commandKey);
            }
            return false;
        }

        public bool CanExecuteCommand(string nodeKey, string commandKey)
        {
            IInterface interf = GetRunningInterface(nodeKey);

            if (interf != null)
            {
                return interf.CanExecuteCommand(nodeKey, commandKey);
            }
            return false;
        }

        #endregion

        #region Private

        private void loadRegisteredInterfaces(IOhmSystemInternal system)
        {
            foreach (var key in _dataRegisteredInterfaces.Keys)
            {
                try
                {
                    var interf = CreateInterface(key, system);
                    if (interf != null)
                    {
                        if (interf.StartOnLaunch)
                        {
                            StartInterface(interf);
                        }
                    }
                    else
                    {
                        _logger.Error("Cannot create interface : " + key);
                    }
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
            IDataDictionary _dataPlugin = _dataRegisteredInterfaces.GetOrCreateDataDictionary(key);
            String id = "";
            if (_dataPlugin != null)
            {
                id = _dataPlugin.GetString("PluginId");
                result = _pluginsMng.GetPlugin(new Guid(id));
            }
            else
            {
                _logger.Error("Loading interface : " + key + " failed, plugin not found");
            }
            return result;
        }

        private InterfaceAbstract CreateInterface(string key, IOhmSystemInternal system)
        {
            return CreateInterface(key, GetPluginForInterface(key), system);
        }

        private string GetInterfaceLoggerKey(string key, IPlugin plugin)
        {
            return plugin.Id.ToString() + '.' + key;
        }

        private InterfaceAbstract CreateInterface(string key, IPlugin plugin, IOhmSystemInternal system)
        {
            InterfaceAbstract result = null;

            if (plugin != null)
            {
                var interfaceLogger = _loggerMng.GetLogger(GetInterfaceLoggerKey(key, plugin));
                var tempResult = plugin.CreateInterface(key, interfaceLogger);

                if (tempResult != null)
                {
                    var _interfData = system.GetOrCreateDataStore(plugin.Id.ToString() + "." + tempResult.Key);

                    tempResult.Init(_interfData, system.GetInterfaceGateway(tempResult));
                    _runningInterfaces.Add(tempResult);
                    _runningDic.Add(key, tempResult);

                    result = tempResult;
                }
            }

            return result;
        }

        private IDataDictionary CreateInterfaceData(string key, IPlugin plugin) {
            var result = new DataDictionary();

            result.StoreString("PluginId", plugin.Id.ToString());

            return result;
        }

        private IInterface GetRunningInterface(string nodeKey)
        {
            IInterface result;
            string interfaceKey = nodeKey;

            if (nodeKey.Contains("."))
            {
                interfaceKey = nodeKey.Split('.')[0];
            }

            if (!_runningDic.TryGetValue(interfaceKey, out result))
            {
                result = null;
            }

            return result;
        }

        private bool StartInterface(IInterface interf)
        {
            bool result = false;
            if (interf.InterfaceState == InterfaceStates.Disabled)
            {
                interf.Starting();
                result = true;
            }
            return result;
        }

        #endregion

    }
}
