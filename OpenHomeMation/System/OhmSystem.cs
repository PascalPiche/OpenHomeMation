﻿using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.RAL;
using OHM.VAL;
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
        private IDataManager _dataMng;
        private IVrManager _vrMng;
        private IPluginsManager _pluginsMng;
        private IAPI _api;

        #endregion

        #region Internal Ctor

        internal OhmSystem(IInterfacesManager interfacesMng, IVrManager vrMng, ILoggerManager loggerMng, IDataManager dataMng, IPluginsManager pluginsMng)
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
            return new OhmSystemInstallGateway(plugin, _loggerMng.GetLogger(plugin.Name), _interfacesMng);
        }

        public IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf)
        {
            return new OhmSystemInterfaceGateway(this, interf);
        }

        public IOhmSystemUnInstallGateway GetUnInstallGateway(Plugins.IPlugin plugin)
        {
            return new OhmSystemUnInstallGateway(plugin, _loggerMng.GetLogger(plugin.Name), _interfacesMng);
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

            public IAPIResult ExecuteCommand(string key, Dictionary<string, string> arguments)
            {
                string[] splitedKey = key.Split('/');
                IAPIResult commandResult = new APIResultFalse();

                if (splitedKey.Length >= 2)
                {
                    bool isPlugins = splitedKey[0] == "plugins";
                    bool isHal = splitedKey[0] == "hal";

                    //Check first value
                    if (isPlugins)
                    {
                        commandResult = ExecutePluginsCommand(splitedKey, arguments);
                    }
                    else if (isHal)
                    {
                        commandResult = ExecuteHalCommand(splitedKey, arguments);
                    }
                }
                return commandResult;
            }

            public IAPIResult ExecuteCommand(String key)
            {
                return this.ExecuteCommand(key, null);
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

            private IAPIResult ExecutePluginsCommand(string[] splitedKey, Dictionary<string, string> arguments)
            {
                IAPIResult result = new APIResultFalse();

                if (splitedKey[1] == "list")
                {
                    if (splitedKey.Length >= 2)
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

                            }
                        }
                    }
                    else
                    {
                        //Output availaible execute
                    }
                    
                }

                return result;
            }

            private IAPIResult ExecuteHalCommand(string[] splitedKey, Dictionary<string, string> arguments)
            {
                IAPIResult result = new APIResultFalse();

                if (splitedKey[1] == "list")
                {
                    if (splitedKey.Length > 2)
                    {
                        if (splitedKey[2] == "interfaces")
                        {
                            result = new APIResultTrue(_system._interfacesMng.RunnableInterfaces);
                        }
                    }
                    else
                    {
                        //TODO: Output Available list

                    }
                }
                else if (splitedKey[1] == "execute")
                {
                    if (splitedKey.Length > 2)
                    {
                        if (splitedKey[2] == "start")
                        {
                            /*if (_system._interfacesMng.StartInterface())
                            {
                                result = new APIResultTrue(true);
                            }
                            else
                            {

                            }*/
                        }
                        else if (splitedKey[2] == "stop")
                        {
                            /*if (_system._interfacesMng.StopInterface())
                            {
                                result = new APIResultTrue(true);
                            }
                            else
                            {

                            }*/
                        }
                        else if (splitedKey[2] == "command")
                        {
                            /*if (_system._interfacesMng.ExecuteCommand())
                            {

                            }
                            else
                            {

                            }*/
                        }
                    }
                    else
                    {
                        //Output availaible execute
                    }

                }

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
            _logger = _loggerMng.GetLogger("OhmSystem");
            result = true;

            //Init DataManager
            if (!DataMng.Init())
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
