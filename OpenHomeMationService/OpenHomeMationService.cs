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
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
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
            var dataMng = new FileDataManager(AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            vrMng = new VrManager(loggerMng, pluginMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
            var result = ohm.Start();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            switch (powerStatus)
            {
                case PowerBroadcastStatus.BatteryLow:
                    break;
                case PowerBroadcastStatus.OemEvent:
                    break;
                case PowerBroadcastStatus.PowerStatusChange:
                    break;
                case PowerBroadcastStatus.QuerySuspend:
                    break;
                case PowerBroadcastStatus.QuerySuspendFailed:
                    break;
                case PowerBroadcastStatus.ResumeAutomatic:
                    break;
                case PowerBroadcastStatus.ResumeCritical:
                    break;
                case PowerBroadcastStatus.ResumeSuspend:
                    break;
                case PowerBroadcastStatus.Suspend:
                    break;
            }
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnStop()
        {
            ohm.Shutdown();
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            //between 128 CanPauseAndContinue 256 inclusive
            switch (command)
            {
                case 128:
                    break;
                case 129:
                    break;
            }
        } 
    }
}
