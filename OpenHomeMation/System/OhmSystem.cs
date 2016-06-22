﻿using OHM.Common.Vr;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;

namespace OHM.Sys
{
    public sealed class OhmSystem : IOhmSystemInternal
    {
        private ILoggerManager _loggerMng;
        private IInterfacesManager _interfacesMng;
        private IDataManager _dataMng;
        private IVrManager _vrMng;
        private IPluginsManager _pluginsMng;
        private IAPI _api;

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

        internal ILoggerManager LoggerMng { get { return _loggerMng; } }

        internal IInterfacesManager InterfacesMng { get { return _interfacesMng; } }

        public IDataManager DataMng { get { return _dataMng; } }

        internal IVrManager vrMng { get { return _vrMng; } }

        #endregion

        #region Public Api

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

        #endregion  
     
        public IAPI API
        {
            get
            {
                return _api;
            }
        }

        public IDataStore GetOrCreateDataStore(string key)
        {
            return this._dataMng.GetOrCreateDataStore(key);
        }
        
        public sealed class APIInstance : IAPI
        {
            private OhmSystem _system;
            internal APIInstance(OhmSystem system)
            {
                _system = system;
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
        }
    }
}
