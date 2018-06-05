using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Collections.Generic;

namespace OHM.Logger
{
    public class LoggerManager : ILoggerManager
    {
        #region Static section

        #region Private Static Members

        private static PatternLayout m_patternLayout;

        #endregion

        #region Static Ctor

        static LoggerManager()
        {
            m_patternLayout = new PatternLayout();
            m_patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            m_patternLayout.ActivateOptions();
        }

        #endregion

        #region Static Property

        public static PatternLayout DefaultPatternLayout
        {
            get
            {
                return m_patternLayout;
            }
        }

        #endregion

        #endregion 

        #region Private Members

        private Hierarchy hierarchy;
        private MemoryAppender memoryLog;

        #endregion

        #region Public Ctor

        /// <summary>
        /// Core CTor 
        /// </summary>
        public LoggerManager(IList<IAppender> appender = null)
            : base()
        {
            // Core Repository
            hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            hierarchy.EmittedNoAppenderWarning = true;
            hierarchy.Threshold = Level.All;

            // Memory Logging
            memoryLog = new MemoryAppender();
            memoryLog.Threshold = Level.All;
            memoryLog.ActivateOptions();
            hierarchy.Root.AddAppender(memoryLog);

            // File Logging
            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = @"Logs\EventLog.txt";
            roller.Layout = DefaultPatternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "100MB";
            roller.Threshold = Level.Warn;
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);
            
            if (appender != null)
            {
                foreach (IAppender item in appender)
                {
                    hierarchy.Root.AddAppender(item);
                }
            }
        }

        #endregion

        #region Public properties

        public ILog GetLogger(string name)
        {
            return new log4net.Core.LogImpl(hierarchy.GetLogger(name));
        }

        #endregion
    }
}
