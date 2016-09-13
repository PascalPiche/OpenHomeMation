using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;

namespace OHM.SYS
{
    public class OpenHomeMation
    {
        #region Private Members

        private ILogger _logger;
        private bool _isRunning = false;
        private OhmSystem _ohmSystem;

        #endregion 

        #region Public Ctor

        public OpenHomeMation(IPluginsManager pluginsMng, DataManagerAbstract dataMng, ILoggerManager loggerMng, IInterfacesManager interfacesMng, IVrManager vrMng)
        {
            _ohmSystem = new OhmSystem(interfacesMng, vrMng, loggerMng, dataMng, pluginsMng);
        }

        #endregion

        #region Public

        public IAPI API { get { return _ohmSystem.API; } }

        public bool Start()
        {
            bool result = false;
            _logger = _ohmSystem.LoggerMng.GetLogger("OHM","OHM");

            _logger.Debug("Starting");

            if (_ohmSystem.Start())
            {
                _logger.Info("System Started");

                //TODO: STARTING INTERNAL SERVER IF NEEDED
                //OpenHomeMationServerImplementation.Run();

                this._isRunning = true;
            } 
            else
            {
                _logger.Fatal("Can not start the system.");
            }

            return result;
        }

        public void Shutdown()
        {
            //Log data
            _logger.Debug("Stoping");

            //Shutdown DataManager
            _ohmSystem.DataMng.Shutdown();

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

    }
}
