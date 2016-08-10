using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.SYS;
using System;
using System.ServiceProcess;

namespace OpenHomeMationService
{
    public partial class OpenHomeMationService : ServiceBase
    {
        private OpenHomeMation ohm;

        public OpenHomeMationService()
        {
            this.AutoLog = true;
            this.CanHandlePowerEvent = false;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = false;
            this.CanShutdown = true;
            this.CanStop = false;
            this.ServiceName = "OvWra";
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            ILoggerManager loggerMng;
            IPluginsManager pluginMng;
            IInterfacesManager interfacesMng;
            IVrManager vrMng;

            loggerMng = new LoggerManager();
            var dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            vrMng = new VrManager(loggerMng, pluginMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
            ohm.Start();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnStop()
        {
            ohm.Shutdown();
        }
    }
}
