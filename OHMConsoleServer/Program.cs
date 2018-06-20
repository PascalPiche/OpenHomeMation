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

namespace OHMConsoleServer
{
    class Program
    {
        private static OpenHomeMation app;

        static void Main(string[] args)
        {
            String baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Main_Start_Header();

            // Create logger Manager
            ILoggerManager loggerMng = CreateLoggerManager();

            // Create Data manager
            var dataMng = new FileDataManager(baseDirectory + "\\data\\");

            // Create Plugin manager
            var pluginMng = new PluginsManager(loggerMng, baseDirectory + "\\plugins\\");

            // Create Interface manager
            var interfacesMng = new InterfacesManager(loggerMng, pluginMng);

            // Create VrManager
            var vrMng = new VrManager(loggerMng, pluginMng);

            // Create OHM
            app = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
        }

        private static ILoggerManager CreateLoggerManager()
        {
            // Create Log Console Output Appender
            AppConsoleOutput appender = new AppConsoleOutput();

            // Config Console Layout
            appender.Layout = OHM.Logger.LoggerManager.DefaultPatternLayout;
            
            // Create Logger Manager arguments
            IList<IAppender> col = new List<IAppender>();
            col.Add(appender);

            // Create Final Logger Manager
            return new LoggerManager(col);
        }

        private static void Main_Start_Header()
        {
            //Write Header
            Console.WriteLine("OHM Console Server");
        }
    }
}
