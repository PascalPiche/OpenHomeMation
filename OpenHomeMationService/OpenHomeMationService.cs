using log4net;
using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.SYS;
using System;
using System.ServiceProcess;

//https://docs.microsoft.com/en-us/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer#BK_AddInstallers

namespace OpenHomeMationService
{
    /// <summary>
    /// Core windows service class for Open Home Mation
    /// </summary>
    public partial class OpenHomeMationService : ServiceBase
    {
        #region Private member

        /// <summary>
        /// Main _instance 
        /// </summary>
        private static OpenHomeMation ohm;

        /// <summary>
        /// Main log _instance
        /// </summary>
        private static ILog log;
        
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

            //Initialise EventLog
            string eventSourceName = "OpenHomeMation";
            string logName = "Service";
            
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            this.EventLog.Source = eventSourceName;
            this.EventLog.Log = logName; 
        }

        #endregion

        #region Protected override

        /// <summary>
        /// Create all object needed for the system and start the system.
        /// </summary>
        /// <param name="args">Arguments configuring the system</param>
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            this.EventLog.WriteEntry("OHM.Service: OnStart: BEGIN", System.Diagnostics.EventLogEntryType.Information);

            ParseArgs(args);

            this.EventLog.WriteEntry("OHM.Service: OnStart: Args Parsed", System.Diagnostics.EventLogEntryType.Information);

            //TODO: We need to setup LoggerManager based on the args
            ILoggerManager loggerMng = new LoggerManager();

            //Get Internal logger
            ILog log = loggerMng.GetLogger("Service");

            //FROM HERE WE SHOULD USE INTERNAL LOG


            //TODO: Extract Parameters directory from args
            DataManagerAbstract dataMng = new FileDataManager(AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            IPluginsManager pluginMng = new PluginsManager(AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            IInterfacesManager interfacesMng = new InterfacesManager(pluginMng);
            IVrManager vrMng = new VrManager();

            //Create main _instance
            ohm = OpenHomeMation.Create(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);

            //Write event starting
            log.Debug("OHM.Service: OnStart: OHM Starting");
            //this.EventLog.WriteEntry("OHM.Service: OnStart: OHM Starting", System.Diagnostics.EventLogEntryType.Information);

            if (ohm.Start())
            {
                log.Info("OHM.Service: OnStart: OHM Started");
                //this.EventLog.WriteEntry("OHM.Service: OnStart: OHM Started", System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            else
            {
                log.Info("OHM.Service: OnStart: OHM Not started");
                //this.EventLog.WriteEntry("OHM.Service: OnStart: OHM Not started", System.Diagnostics.EventLogEntryType.Error, 1);
            }
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
        /// <remarks>Command parameter must be between 128 and 256 inclusive</remarks>
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            //between 128 - 256 inclusive
            this.EventLog.WriteEntry("OHM.Service: OnCustomCommand: command received", System.Diagnostics.EventLogEntryType.Information);

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
            this.EventLog.WriteEntry("OHM.Service: OnPowerEvent: powerStatus received", System.Diagnostics.EventLogEntryType.Information);

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
        /// <param name="args">List of arguments</param>
        private void ParseArgs(string[] args)
        {
            //TODO Parse and set flags
        }

        #endregion
    }
}
