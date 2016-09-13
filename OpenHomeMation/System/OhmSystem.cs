using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.Nodes.ALR;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.SYS
{
    internal sealed class OhmSystem : IOhmSystemInternal
    {
        #region Private Members

        private ILogger _logger;
        private ILoggerManager _loggerMng;
        private IInterfacesManager _interfacesMng;
        private DataManagerAbstract _dataMng;
        private IVrManager _vrMng;
        private IPluginsManager _pluginsMng;
        private IAPI _api;

        #endregion

        #region Internal Ctor

        internal OhmSystem(IInterfacesManager interfacesMng, IVrManager vrMng, ILoggerManager loggerMng, DataManagerAbstract dataMng, IPluginsManager pluginsMng)
        {
            _loggerMng = loggerMng;
            _interfacesMng = interfacesMng;
            _dataMng = dataMng;
            _vrMng = vrMng;
            _pluginsMng = pluginsMng;
            _api = new APIInstance(this);
        }

        #endregion

        #region Public Properties

        public IAPI API
        {
            get
            {
                return _api;
            }
        }

        #endregion

        #region Public Functions

        public IOhmSystemInstallGateway GetInstallGateway(Plugins.IPlugin plugin)
        {
            return new OhmSystemInstallGateway(plugin, _loggerMng.GetLogger("PluginsManager", plugin.Name), _interfacesMng, _vrMng);
        }

        public IOhmSystemInterfaceGateway GetInterfaceGateway(IALRInterface interf)
        {
            return new OhmSystemInterfaceGateway(this, interf);
        }

        public IOhmSystemUnInstallGateway GetUnInstallGateway(Plugins.IPlugin plugin)
        {
            return new OhmSystemUnInstallGateway(plugin, _loggerMng.GetLogger("PluginsManager", plugin.Name), _interfacesMng);
        }

        public IDataStore GetOrCreateDataStore(string key)
        {
            return this._dataMng.GetOrCreateDataStore(key);
        }

        #endregion  
     
        #region Public sealed class

        public sealed class APIInstance : IAPI
        {
            #region Private members

            private OhmSystem _system;

            #endregion

            #region Public Events

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            #region Public Methods

            public IAPIResult ExecuteCommand(String key)
            {
                return this.ExecuteCommand(key, null);
            }

            public IAPIResult ExecuteCommand(string key, Dictionary<string, string> arguments)
            {
                string[] splitedKey = key.Split('/');
                IAPIResult commandResult = new APIResultFalse();

                if (splitedKey.Length > 2)
                {
                    switch (splitedKey[0])
                    {
                        case "datas":
                            commandResult = ExecuteDatasTree(splitedKey, arguments);
                            break;
                        case "plugins":
                            commandResult = ExecutePluginsTree(splitedKey, arguments);
                            break;
                        case "ral":
                            commandResult = ExecuteHalTree(splitedKey, arguments);
                            break;
                        case "val":
                            commandResult = ExecuteValTree(splitedKey, arguments);
                            break;
                    }
                }
                return commandResult;
            }

            #endregion

            #region Internal Methods

            internal APIInstance(OhmSystem system)
            {
                _system = system;
                ((ObservableCollection<IPlugin>)_system._pluginsMng.InstalledPlugins).CollectionChanged += InstalledPlugins_CollectionChanged;
            }

            #endregion

            #region Private Methods

            private IAPIResult ExecuteDatasTree(string[] splitedKey, Dictionary<string, string> arguments)
            {
                IAPIResult result = new APIResultFalse();

                if (splitedKey[1] == "list")
                {
                    if (splitedKey.Length > 3)
                    {
                        if (splitedKey[2] == "store")
                        {
                            //_system._dataMng.
                        }
                    }
                }
                    

                return result;
            }

            private IAPIResult ExecutePluginsTree(string[] splitedKey, Dictionary<string, string> arguments)
            {
                IAPIResult result = new APIResultFalse();

                if (splitedKey[1] == "list")
                {
                    if (splitedKey.Length > 3)
                    {
                        if (splitedKey[2] == "availables")
                        {
                            result = new APIResultTrue(_system._pluginsMng.AvailablesPlugins);
                        }
                        else if (splitedKey[2] == "installed")
                        {
                            result = new APIResultTrue(_system._pluginsMng.InstalledPlugins);
                        }
                    }
                    else
                    {
                        //Output Available list
                        Collection<string> list = new Collection<string>();
                        list.Add("availables");
                        list.Add("installed");
                        result = new APIResultTrue(list);
                    }
                    
                }
                else if (splitedKey[1] == "execute")
                {
                    if (splitedKey.Length > 2)
                    {
                        if (splitedKey[2] == "install")
                        {
                            if (_system._pluginsMng.InstallPlugin(new Guid(arguments["guid"]), _system))
                            {
                                result = new APIResultTrue(true);
                            }
                            else
                            {
                                //TODO
                            }
                        }
                        else if (splitedKey[2] == "uninstall")
                        {
                            if (_system._pluginsMng.UnInstallPlugin(new Guid(arguments["guid"]), _system))
                            {
                                result = new APIResultTrue(true);
                            }
                            else
                            {
                                //TODO
                            }
                        }
                    }
                    else
                    {
                        //Output Available list
                        Collection<string> list = new Collection<string>();
                        list.Add("install");
                        list.Add("uninstall");
                        result = new APIResultTrue(list);
                    }
                    
                }

                return result;
            }

            private IAPIResult ExecuteHalTree(string[] splitedKey, Dictionary<string, string> arguments)
            {

                IAPIResult result = new APIResultFalse();

                #region list

                if (splitedKey[1] == "list")
                {
                    if (splitedKey.Length > 3)
                    {
                        if (splitedKey[2] == "interfaces")
                        {
                            result = new APIResultTrue(_system._interfacesMng.RunnableInterfaces);
                        }
                    }
                    else
                    {
                        //Output Available list
                        Collection<string> list = new Collection<string>();
                        list.Add("interfaces");
                        result = new APIResultTrue(list);
                    }
                }
                #endregion

                #region Execute

                else if (splitedKey[1] == "execute")
                {
                    if (splitedKey.Length > 4)
                    {
                        #region start

                        if (splitedKey[2] == "start")
                        {
                            if (_system._interfacesMng.StartInterface(splitedKey[3]))
                            {
                                result = new APIResultTrue(true);
                            }
                        }
                        #endregion

                        #region stop

                        else if (splitedKey[2] == "stop")
                        {
                            if (_system._interfacesMng.StopInterface(splitedKey[3]))
                            {
                                result = new APIResultTrue(true);
                            }
                        }
                        #endregion

                        else
                        {
                            if (_system._interfacesMng.ExecuteCommand(splitedKey[3], splitedKey[2], arguments))
                            {
                                result = new APIResultTrue(true);
                            }
                        }
                    }
                    else
                    {
                        //Output Available list
                        Collection<string> list = new Collection<string>();
                        list.Add("start");
                        list.Add("stop");
                        list.Add("*");
                        result = new APIResultTrue(list);
                    }

                }
                #endregion

                #region can-execute

                else if (splitedKey[1] == "can-execute")
                {
                    if (splitedKey.Length > 4)
                    {
                        if (_system._interfacesMng.CanExecuteCommand(splitedKey[3], splitedKey[2]))
                        {
                            result = new APIResultTrue(true);
                        }
                    }
                }

                #endregion

                return result;
            }

            private IAPIResult ExecuteValTree(string[] splitedKey, Dictionary<string, string> arguments)
            {

                IAPIResult result = new APIResultFalse();

                #region list

                if (splitedKey[1] == "list")
                {
                    if (splitedKey.Length > 3)
                    {
                        /*if (splitedKey[2] == "interfaces")
                        {
                            result = new APIResultTrue(_system._interfacesMng.RunnableInterfaces);
                        }*/
                    }
                    else
                    {
                        //Output Available list
                        /*Collection<string> list = new Collection<string>();
                        list.Add("interfaces");
                        result = new APIResultTrue(list);*/
                    }
                }
                #endregion

                #region Execute

                else if (splitedKey[1] == "execute")
                {
                    if (splitedKey.Length > 3)
                    {
                        #region start

                        if (splitedKey[2] == "addVrNode")
                        {
                            if (_system._vrMng.CreateRootNode(arguments["nodeType"], arguments["key"], arguments["name"]))
                            {
                                result = new APIResultTrue(true);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //Output Available list
                        Collection<string> list = new Collection<string>();
                        list.Add("addVrNode");
                        result = new APIResultTrue(list);
                    }

                }
                #endregion

                #region can-execute

                else if (splitedKey[1] == "can-execute")
                {
                    if (splitedKey.Length > 4)
                    {
                        if (_system._interfacesMng.CanExecuteCommand(splitedKey[3], splitedKey[2]))
                        {
                            result = new APIResultTrue(true);
                        }
                    }
                }

                #endregion

                return result;
            }

            private void InstalledPlugins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                NotifyPropertyChanged("plugins/installed/");
            }

            private void NotifyPropertyChanged(String propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            
            #endregion
        }

        #endregion

        #region Internal Properties

        internal ILoggerManager LoggerMng { get { return _loggerMng; } }

        internal IDataManager DataMng { get { return _dataMng; } }

        #endregion

        #region Internal Methods

        internal bool Start()
        {
            bool result = false;
            _logger = _loggerMng.GetLogger("OHM", "OhmSystem");
            result = true;

            //Init DataManager
            if (!DataMng.Init(_loggerMng))
            {
                _logger.Fatal("DataManager failed Init. Abording start");
                return false;
            }

            //Init PluginsManager
            if (!InitPluginsMng())
            {
                _logger.Fatal("PluginsManager failed Init. Abording start");
                return false;
            }

            //Init InterfacesManager
            if (!InitInterfacesMng())
            {
                _logger.Fatal("InterfacesManager failed Init. Abording start");
                return false;
            }

            //Init InterfacesManager
            if (!InitVrMng())
            {
                _logger.Fatal("VirtualRealityManager failed Init. Abording start");
                return false;
            }

            return result;
        }

        #endregion

        #region Private functions

        private bool InitPluginsMng()
        {
            return _pluginsMng.Init(DataMng.GetOrCreateDataStore("PluginsManager"));
        }

        private bool InitInterfacesMng()
        {
            return _interfacesMng.Init(DataMng.GetOrCreateDataStore("InterfacesManager"), this);
        }

        private bool InitVrMng()
        {
            return _vrMng.Init(DataMng.GetOrCreateDataStore("VrManager"), this);
        }

        #endregion
    }
}
