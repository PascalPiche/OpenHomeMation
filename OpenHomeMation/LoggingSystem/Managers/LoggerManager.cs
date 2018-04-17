using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OHM.Logger
{
    public class LoggerManager : ILoggerManager
    {

        private Hierarchy hierarchy;

        private PatternLayout m_patternLayout;

        public PatternLayout DefaultPatternLayout
        {
            get
            {
                return m_patternLayout;
            }
        }
        /// <summary>
        /// Core CTor 
        /// </summary>
        public LoggerManager(IList<IAppender> appender = null)
            : base()
        {
            hierarchy = (Hierarchy)LogManager.GetRepository();

            m_patternLayout = new PatternLayout();
            m_patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            m_patternLayout.ActivateOptions();

            MemoryAppender memory = new MemoryAppender();
            memory.Threshold = Level.All;
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            foreach (IAppender item in appender)
            {
                hierarchy.Root.AddAppender(item);
            }


            /*ManagedColoredConsoleAppender console = new ManagedColoredConsoleAppender();
            //console.Target = "Console.Out";
            console.Threshold = Level.All;
            console.ActivateOptions();
            hierarchy.Root.AddAppender(console);*/

            /*RollingFileAppender roller = new RollingFileAppender();
           roller.AppendToFile = false;
           roller.File = @"Logs\EventLog.txt";
           roller.Layout = DefaultPatternLayout;
           roller.MaxSizeRollBackups = 5;
           roller.MaximumFileSize = "1GB";
           roller.Threshold = Level.Warn;
           roller.RollingStyle = RollingFileAppender.RollingMode.Size;
           roller.StaticLogFileName = true;

           roller.ActivateOptions();*/

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
            hierarchy.EmittedNoAppenderWarning = true;
            hierarchy.Threshold = Level.All;
        }

        public ILog GetLogger(string name)
        {
            return log4net.LogManager.GetLogger(name);
        }
    }
}
