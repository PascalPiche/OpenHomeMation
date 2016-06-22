using OHM.Common.Vr;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Sys
{
    public sealed class OhmSystem : IOhmSystemInternal
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

            private OhmSystem _system;

            public event PropertyChangedEventHandler PropertyChanged;

            internal APIInstance(OhmSystem system)
            {
                _system = system;
                ((ObservableCollection<IPlugin>)_system._pluginsMng.InstalledPlugins).CollectionChanged += InstalledPlugins_CollectionChanged;
            }

            public IAPIResult ExecuteCommand(string key, Dictionary<String, object> arguments)
            {
                string[] splitedKed = key.Split('/');
                bool resultBool = false;
                object result = null;

                IAPIResult commandResult = new APIResultFalse();

                if (splitedKed.Length >= 2)
                {
                    //Check first value
                    if (splitedKed[0] == "plugins")
                    {
                        if (splitedKed[1] == "install")
                        {
                            resultBool = _system._pluginsMng.InstallPlugin((Guid)arguments["guid"], _system);
                        }
                        else if (splitedKed[1] == "uninstall")
                        {
                            resultBool = _system._pluginsMng.UnInstallPlugin((Guid)arguments["guid"], _system);
                        }
                        else if (splitedKed[1] == "list")
                        {
                            if (splitedKed.Length > 2)
                            {
                                if (splitedKed[2] == "availables")
                                {
                                    resultBool = true;
                                    result = _system._pluginsMng.AvailablesPlugins;
                                }
                                else if (splitedKed[2] == "installed")
                                {
                                    resultBool = true;
                                    result = _system._pluginsMng.InstalledPlugins;
                                }
                            }
                        }
                    }
                }

                if (resultBool)
                {
                    if (result != null)
                    {
                        commandResult = new APIResultTrue(result);
                    }
                    else
                    {
                        commandResult = new APIResultTrue(resultBool);
                    }
                }

                return commandResult;
            }

            public IAPIResult ExecuteCommand(string key)
            {
                return this.ExecuteCommand(key, null);
            }

            private void NotifyPropertyChanged(String propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            private void InstalledPlugins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                NotifyPropertyChanged("plugins/installed/");
            }
        }

        #endregion

        #region Internal Properties

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

        #region Internal Properties

        internal ILoggerManager LoggerMng { get { return _loggerMng; } }

        internal IDataManager DataMng { get { return _dataMng; } }

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
