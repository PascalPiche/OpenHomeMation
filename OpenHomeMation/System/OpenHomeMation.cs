using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;

namespace OHM.System
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

        public void start()
        {
            _logger = _loggerMng.GetLogger("root");
            _logger.Info("Starting OHM");
            _ohmSystem = new OhmSystem();
            _ohmSystem.LoggerMng = _loggerMng;
            _ohmSystem.InterfacesMng = _interfacesMng;
            _dataMng.Init();

            if (StartPluginMng())
            {
                this._isRunning = true;
                _logger.Info("Started OHM");
            }
            else
            {
                _logger.Fatal("PluginManager failed Init. Abording start");
            }
            
        }

        public void shutdown()
        {
            _logger.Info("Stoping OHM");
            this._isRunning = false;
            _logger.Info("Stoped OHM");
        }

        public bool isRunning()
        {
            return this._isRunning;
        }

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

        public IOhmSystem System
        {
            get
            {
                return new OhmSystem();
            }
        }
    }
}
