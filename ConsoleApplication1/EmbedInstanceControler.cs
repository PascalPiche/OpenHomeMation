using log4net.Appender;
using OHM.Apps.Console.Tools.Logger;
using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public class EmbedInstanceControler
    {
     
        /// <summary>
        /// 
        /// </summary>
        public void Create()
        {
            // Launch logger Manager
            ILoggerManager loggerMng = CreateLoggerManager();

            // Launch Data manager
            var dataMng = new FileDataManager(AppDomain.CurrentDomain.BaseDirectory + "\\data\\");

            // Launch Plugin manager
            var pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");

            // Launch Interface manager
            var interfacesMng = new InterfacesManager(loggerMng, pluginMng);

            // Launch VrManager
            var vrMng = new VrManager(loggerMng, pluginMng);

            // Launch OHM
            app = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
        }

        /// <summary>
        /// Start Local embeded instance
        /// </summary>
        /// <returns>True if the start was successfull</returns>
        public bool Start()
        {
            return app.Start();
        }

        /// <summary>
        /// Shutdown local embeded instance
        /// </summary>
        public void Shutdown()
        {
            if (app != null)
            {
                app.Shutdown();
            }
        }

        #region Private

        /// <summary>
        /// 
        /// </summary>
        private OpenHomeMation app;

        /// <summary>
        /// Launch the custom logger for the console 
        /// and configure base logger with passed args if available.
        /// </summary>
        /// <returns>The created ILoggerManager</returns>
        private ILoggerManager CreateLoggerManager()
        {
            // Launch Log Console Output Appender
            AppConsoleOutput appender = new AppConsoleOutput();

            // Config Console Layout
            appender.Layout = OHM.Logger.LoggerManager.DefaultPatternLayout;

            // Launch Logger Manager arguments
            IList<IAppender> col = new System.Collections.Generic.List<IAppender>();
            col.Add(appender);

            // Launch Final Logger Manager
            return new OHM.Logger.LoggerManager(col);
        }

        #endregion
    }
}
