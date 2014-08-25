using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public OpenHomeMation(IPluginsManager pluginsMng, IDataManager dataMng, ILoggerManager loggerMng)
        {
            //Store dependency
            this._loggerMng = loggerMng;
            this._pluginsMng = pluginsMng;
            this._dataMng = dataMng;
        }

        public void start()
        {
            _logger = _loggerMng.GetLogger("root");
            _logger.Info("Starting OHM");
            _ohmSystem = new OhmSystem();
            _ohmSystem.Logger = _logger;
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
                data = _dataMng.CreateDataStore("PluginsManager");
               
            }
            return _pluginsMng.Init(data);
        }

        public ISystem System
        {
            get
            {
                return new OhmSystem();
            }
        }
    }
}
