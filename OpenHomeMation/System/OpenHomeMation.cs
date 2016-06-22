using OHM.Common.Vr;
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
        private IVrManager _vrMng;

        public OpenHomeMation(IPluginsManager pluginsMng, IDataManager dataMng, ILoggerManager loggerMng, IInterfacesManager interfacesMng, IVrManager vrMng)
        {
            //Store dependency
            this._loggerMng = loggerMng;
            this._pluginsMng = pluginsMng;
            this._dataMng = dataMng;
            this._interfacesMng = interfacesMng;
            this._vrMng = vrMng;

            _ohmSystem = new OhmSystem(_interfacesMng, _vrMng, _loggerMng, _dataMng, _pluginsMng);
        }

        #region Public

        public IAPI API { get { return _ohmSystem.API; } }

        public bool start()
        {
            _logger = _loggerMng.GetLogger("OHM");
            _logger.Debug("Starting");

            //Init DataManager
            _dataMng.Init();

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

            //OpenHomeMationServerImplementation.Run();

            //Switch inner flag
            this._isRunning = true;

            //Log data
            _logger.Info("Started");

            return true;
        }

        public void Shutdown()
        {
            //Log data
            _logger.Debug("Stoping");

            //Shutdown DataManager
            _dataMng.Shutdown();

            //Switch inner flag
            this._isRunning = false;

            //Log data
            _logger.Info("Stoped");
        }

        public bool IsRunning()
        {
            return this._isRunning;
        }

        #endregion

        #region "Private"

        private bool InitPluginsMng()
        {
            return _pluginsMng.Init(_dataMng.GetOrCreateDataStore("PluginsManager"));
        }

        private bool InitInterfacesMng()
        {
            return _interfacesMng.Init(_dataMng.GetOrCreateDataStore("InterfacesManager"), _ohmSystem);
        }

        private bool InitVrMng()
        {
            return _vrMng.Init(_dataMng.GetOrCreateDataStore("VrManager"), _ohmSystem);
        }
    
    
        #endregion
    }
}
