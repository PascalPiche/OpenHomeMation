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
        #region Private Members

        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private IPluginsManager _pluginsMng;
        private IDataStore _data;
        private IDataDictionary _dataRegisteredInterfaces;
        private IList<IInterface> _runningInterfaces = new ObservableCollection<IInterface>();
        private Dictionary<string, IInterface> _runningDic = new Dictionary<string, IInterface>();
        private IOhmSystemInternal _system;

        #endregion

        #region Public Properties

        public IList<IInterface> RunnableInterfaces
        {
            get { return _runningInterfaces; }
        }

        #endregion

        #region Public Ctor

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
            //_data.Save();
            _system = system;
            loadRegisteredInterfaces(system);
            _logger.Debug("Inited");
            return true;
        }

        public bool RegisterInterface(string key, IPlugin plugin)
        {
            bool result = false;
            IDataDictionary _interfaceMetaData = _dataRegisteredInterfaces.GetOrCreateDataDictionary(key);
            
            try {
                IInterface newInterface = CreateInterface(key, plugin, _system);
                if (newInterface == null)
                {
                    _logger.Error("Cannot register interface " + key + ", Error on creating Interface");
                }
                else
                {
                    _interfaceMetaData.StoreString("PluginId", plugin.Id.ToString());
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

            //Interface may running
            if (_runningDic.ContainsKey(key))
            {
                _runningDic[key].Shutdowning();
                _runningInterfaces.Remove(_runningDic[key]);
                _runningDic.Remove(key);
            }

            result = _dataRegisteredInterfaces.RemoveKey(key);

            if (!result) {
                _logger.Warn("Cannot uninstall interface " + plugin.Id + ": Interface Not found");
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
            _logger.Debug("Looking for Plugin Id from interface key : " + key);

            if (!_dataRegisteredInterfaces.ContainKey(key))
            {
                _logger.Error("Cannot find Plugin for interface : " + key + " in the registered interface data");
            }
            else
            {
                IDataDictionary _dataPlugin = _dataRegisteredInterfaces.GetOrCreateDataDictionary(key);
                String id = _dataPlugin.GetString("PluginId");
                result = _pluginsMng.GetPlugin(new Guid(id));
                _logger.Debug("Looking for Plugin Id from interface key : " + key + " was successfull");
            }
            return result;
        }

        private RalInterfaceNodeAbstract CreateInterface(string key, IOhmSystemInternal system)
        {
            return CreateInterface(key, GetPluginForInterface(key), system);
        }

        private string GetInterfaceLoggerKey(string key, IPlugin plugin)
        {
            return plugin.Id.ToString() + '.' + key;
        }

        private RalInterfaceNodeAbstract CreateInterface(string key, IPlugin plugin, IOhmSystemInternal system)
        {
            RalInterfaceNodeAbstract result = null;

            if (plugin != null)
            {
                var interfaceLogger = _loggerMng.GetLogger(GetInterfaceLoggerKey(key, plugin));
                var tempResult = plugin.CreateInterface(key);

                if (tempResult != null)
                {
                    var _interfData = system.GetOrCreateDataStore(plugin.Id.ToString() + "." + tempResult.Key);

                    tempResult.Init(_interfData, interfaceLogger, system.GetInterfaceGateway(tempResult));
                    _runningInterfaces.Add(tempResult);
                    _runningDic.Add(key, tempResult);

                    result = tempResult;
                }
            }

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
