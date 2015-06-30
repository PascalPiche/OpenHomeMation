﻿using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OpenHomeMationService
{
    public partial class OpenHomeMationService : ServiceBase
    {

        private OpenHomeMation ohm;
        private ILoggerManager loggerMng;
        private IPluginsManager pluginMng;
        private IInterfacesManager interfacesMng;

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
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng);
            ohm.start();
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
