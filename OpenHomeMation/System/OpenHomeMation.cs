using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;

namespace OHM.Sys
{
    public class OpenHomeMation
    {
        private ILogger _logger;
        private bool _isRunning = false;
        private ILoggerManager _loggerMng;
        private IPluginsManager _pluginsMng;
        private IDataManager _dataMng;
        private OhmSystem _ohmSystem;
        private IInterfacesManager _interfacesMng;

        public OpenHomeMation(IPluginsManager pluginsMng, IDataManager dataMng, ILoggerManager loggerMng, IInterfacesManager interfacesMng)
        {
            //Store dependency
            this._loggerMng = loggerMng;
            this._pluginsMng = pluginsMng;
            this._dataMng = dataMng;
            this._interfacesMng = interfacesMng;
        }

        #region "Public"

        public IOhmSystem System
        {
            get
            {
                return _ohmSystem;
            }
        }

        public void start()
        {
            _logger = _loggerMng.GetLogger("root");
            _logger.Info("Starting OHM");
            _ohmSystem = new OhmSystem( _interfacesMng, _loggerMng, _dataMng);
            _dataMng.Init();

            if (!StartPluginMng())
            {
                _logger.Fatal("PluginManager failed Init. Abording start");
                return;
            }

            if (!StartInterfacesMng())
            {
                _logger.Fatal("InterfacesManager failed Init. Abording start");
                return;
            }

            OpenHomeMationServerImplementation.Run();
            this._isRunning = true;
            _logger.Info("Started OHM"); 
        }

        public void Shutdown()
        {
            _logger.Info("Stoping OHM");
            //Save All Data Store
            _dataMng.Shutdown();
            this._isRunning = false;
            _logger.Info("Stoped OHM");
        }

        public bool IsRunning()
        {
            return this._isRunning;
        }

        #endregion

        #region "Private"

        private bool StartPluginMng()
        {
            IDataStore data = _dataMng.GetDataStore("PluginsManager");
            if (data == null)
            {
                _logger.Debug("Data Store for Plugins Manager not found");
                _logger.Info("Creating new Data Store for Plugins Manager");
                data = _dataMng.GetOrCreateDataStore("PluginsManager");
            }
            return _pluginsMng.Init(data);
        }

        private bool StartInterfacesMng()
        {
            IDataStore data = _dataMng.GetDataStore("InterfacesManager");
            if (data == null)
            {
                _logger.Debug("Data Store for Interfaces Manager not found");
                _logger.Info("Creating new Data Store for Interfaces Manager");
                data = _dataMng.GetOrCreateDataStore("InterfacesManager");

            }
            return _interfacesMng.Init(data, _ohmSystem);
        }
    
        #endregion
    }
}
