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

        /// <summary>
        /// Core Static Subroutine for Main app
        /// </summary>
        /// <param name="args">List of arguments used to configure the server </param>
        static void Main(string[] args)
        {
            Console.WriteLine("Console: Starting Console Server");

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

            app.Start();

            Console.WriteLine("Console: Press any key to end server instance");
            Console.ReadKey();
            app.Shutdown();
            Console.WriteLine("Console: Press any key to close console");
            Console.ReadKey();
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
