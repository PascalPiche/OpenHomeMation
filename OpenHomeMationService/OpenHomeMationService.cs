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
    /// <summary>
    /// Core windows service class for Open Home Mation
    /// </summary>
    public partial class OpenHomeMationService : ServiceBase
    {
        #region Private member

        /// <summary>
        /// Main instance 
        /// </summary>
        private static OpenHomeMation ohm;

        /// <summary>
        /// Main log instance
        /// </summary>
        private static ILogger log;

        /// <summary>
        /// Configured log level from data configuration or args passed
        /// </summary>
        private static int logLevel = 0;

        #endregion

        #region Public Ctor

        /// <summary>
        /// Core constructor. 
        /// Set all service properties listed in the remarks.
        /// </summary>
        /// <remarks>
        /// AutoLog.....................: true
        /// CanHandlePowerEvent.........: true
        /// CanHandleSessionChangeEvent.: false
        /// CanPauseAndContinue.........: false
        /// CanShutdown.................: true
        /// CanStop.....................: true
        /// ServiceName.................: OvWra
        /// </remarks>
        public OpenHomeMationService()
        {
            //Set properties
            this.AutoLog = true;
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = false;
            this.CanShutdown = true;
            this.CanStop = true;
            this.ServiceName = "OvWra";

            //Initialise component
            InitializeComponent();
        }

        #endregion

        #region Protected override

        /// <summary>
        /// Create all object needed for the system and start the system.
        /// </summary>
        /// <param name="args">Arguments configuring the system</param>
        protected override void OnStart(string[] args)
        {
            ParseArgs(args);

            //TODO: We need to setup LoggerManager based on the args
            ILoggerManager loggerMng = new LoggerManager();

            //Get Internal logger
            ILogger log = loggerMng.GetLogger("OHM", "Service");

            //FROM HERE WE SHOULD USE INTERNAL LOG

            //TODO: Extract Data directory from args
            DataManagerAbstract dataMng = new FileDataManager(AppDomain.CurrentDomain.BaseDirectory + "\\data\\");

            //TODO: Extract Plugin directory from data And/Or args
            IPluginsManager pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");

            //
            IInterfacesManager interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            IVrManager vrMng = new VrManager(loggerMng, pluginMng);

            //Create main instance
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);

            //Write event starting
            this.EventLog.WriteEntry("OHM.Service: OnStart: OHM Starting", System.Diagnostics.EventLogEntryType.Information);

            bool result = ohm.Start();

            if (result)
            {
                this.EventLog.WriteEntry("OHM.Service: OnStart: OHM Started", System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            else
            {
                this.EventLog.WriteEntry("OHM.Service: OnStart: OHM Not started", System.Diagnostics.EventLogEntryType.Error, 1);
            }

            base.OnStart(args);
        }

        /// <summary>
        /// Shutdown the system
        /// </summary>
        protected override void OnStop()
        {
            //TODO: Really shutdown? 
            //We should make a difference between stop and shutdown
            ohm.Shutdown();
            base.OnStop();
        }

        /// <summary>
        /// Shutdown the system (Same as Stop)
        /// </summary>
        protected override void OnShutdown()
        {
            ohm.Shutdown();
            base.OnShutdown();
        }

        /// <summary>
        /// Handle custom command
        /// </summary>
        /// <param name="command">Integer of the requested command</param>
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            //between 128 - 256 inclusive
            switch (command)
            {
                case 128:
                    break;
                case 129:
                    break;
            }
        }

        /// <summary>
        /// Handle power event
        /// </summary>
        /// <remarks>
        /// TODO: notify system with new power status.
        /// </remarks>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            switch (powerStatus)
            {
                case PowerBroadcastStatus.BatteryLow:
                    //
                    break;
                case PowerBroadcastStatus.OemEvent:
                    //
                    break;
                case PowerBroadcastStatus.PowerStatusChange:
                    //
                    break;
                case PowerBroadcastStatus.QuerySuspend:
                    //
                    break;
                case PowerBroadcastStatus.QuerySuspendFailed:
                    //
                    break;
                case PowerBroadcastStatus.ResumeAutomatic:
                    //
                    break;
                case PowerBroadcastStatus.ResumeCritical:
                    //
                    break;
                case PowerBroadcastStatus.ResumeSuspend:
                    //
                    break;
                case PowerBroadcastStatus.Suspend:
                    //
                    break;
            }

            return base.OnPowerEvent(powerStatus);
        }

        #region Pause And Continue

        /// <summary>
        /// NOT SUPPORTED
        /// </summary>
        protected override void OnContinue()
        {
            base.OnContinue();
        }

        /// <summary>
        /// NOT SUPPORTED
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
        }

        #endregion

        #endregion

        #region Private method

        /// <summary>
        /// Extract data from args if available
        /// </summary>
        /// <param name="args"></param>
        private void ParseArgs(string[] args)
        {

        }

        #endregion
    }
}
