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
                    IInterface newInterface = CreateInterface(key, plugin, _system);
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
                    _logger.Error("Cannot register interface " + key + ", Unhandled exception on creating Interface", ex);
                }
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

            IDataDictionary _interfaceData = _dataRegisteredInterfaces.GetDataDictionary(key);
            

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
                if (interf.State == InterfaceStates.Enabled)
                {
                    interf.Shutdowning();
                    result = true;
                }
            }
            return result;
        }

        public bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, string> arguments)
        {
            _logger.Info("Executing Command -> Node Key : " + nodeKey + " -> Command Key : " + commandKey);

            string interfaceKey = nodeKey;

            if (nodeKey.Contains("/")) {
                interfaceKey = nodeKey.Split('/')[0];
            }

            //Find interface
            IInterface interf = GetRunningInterface(interfaceKey);
            if (interf != null)
            {
                return interf.ExecuteCommand(nodeKey, commandKey, arguments);
            }
            else
            {
                //TODO LOG DEBUG
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
            IDataDictionary _dataPlugin = _dataRegisteredInterfaces.GetDataDictionary(key);
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

        private IInterface GetRunningInterface(string key)
        {
            IInterface result;

            if (!_runningDic.TryGetValue(key, out result))
            {
                result = null;
            }
            return result;
        }

        private bool StartInterface(IInterface interf)
        {
            bool result = false;
            if (interf.State == InterfaceStates.Disabled)
            {
                interf.Starting();
                result = true;
            }
            return result;
        }

        #endregion

    }
}
