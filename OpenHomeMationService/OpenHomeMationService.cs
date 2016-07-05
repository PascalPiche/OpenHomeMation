using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using OHM.VAL;
using System;
using System.ServiceProcess;

namespace OpenHomeMationService
{
    public partial class OpenHomeMationService : ServiceBase
    {
        private OpenHomeMation ohm;
        private ILoggerManager loggerMng;
        private IPluginsManager pluginMng;
        private IInterfacesManager interfacesMng;
        private IVrManager vrMng;

        public OpenHomeMationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
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
