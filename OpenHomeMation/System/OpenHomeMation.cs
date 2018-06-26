using log4net;
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

        private ILog _logger;
        private bool _isRunning = false;
        private OhmSystem _ohmSystem;

        #endregion 

        #region Public Ctor

        public OpenHomeMation(IPluginsManager pluginsMng, DataManagerAbstract dataMng, ILoggerManager loggerMng, IInterfacesManager interfacesMng, IVrManager vrMng)
        {
            _ohmSystem = new OhmSystem(interfacesMng, vrMng, loggerMng, dataMng, pluginsMng);
        }

        #endregion

        #region Public Properties

        public IAPI API { get { return _ohmSystem.API; } }

        public bool IsRunning { get { return this._isRunning; } }

        #endregion

        #region Public Method

        public bool Start()
        {
            bool result = false;
            //Check if we are already running
            if (!IsRunning)
            {
                _logger = _ohmSystem.LoggerMng.GetLogger("OHM");
                _logger.Debug("Starting.");

                if (_ohmSystem.Start())
                {
                    this._isRunning = true;
                    _logger.Info("System Started");

                    //TODO: STARTING INTERNAL SERVER IF NEEDED
                    //OpenHomeMationServerImplementation.Run();
                }
                else
                {
                    //Log fatal erro
                    _logger.Fatal("Can not start the system.");
                }
            }
            else
            {
                _logger.Warn("Already running.");
            }

            return result;
        }

        public void Shutdown()
        {
            //Log data
            _logger.Debug("Stoping system");

            //TODO Shutdown AI Layer

            //TODO Shutdown AI Manager

            //TODO Shutdown VR Layer

            //TODO Shutdown VR Manager

            //TODO Shutdown Real Layer

            //TODO Shutdown Interface Manager

            //TODO Shutdown Plugins Manager
            

            //TODO Shutdown System Status

            //Shutdown DataManager
            _ohmSystem.DataMng.Shutdown();

            //TODO shutdown Logger
            //_ohmSystem.LoggerMng.Shutdown();

            //Switch inner flag
            this._isRunning = false;

            //Log data
            _logger.Info("Stoped");
        }

        #endregion
    }
}
