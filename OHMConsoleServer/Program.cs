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
        /// Main static subroutine for the Console Server Application
        /// </summary>
        /// <param name="args">List of arguments used to configure the server </param>
        static void Main(string[] args)
        {
            Console.WriteLine("Console: Starting Console Server");

            String baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Main_Start_Header();

            // TODO? Trim args
            List<string> appArgs = new List<string>(args);

            //TODO REMOVES
            //TEMP DEV CODE
            //FORCE SERVER ENABLED = TRUE;
            appArgs.Add("-server-api.enabled");
            appArgs.Add("true");
            appArgs.Add("-server-api.port");
            appArgs.Add("8080");
            appArgs.Add("-system.require-server-api");
            appArgs.Add("true");
            
            appArgs.Add("-system.launch-on-start");
            appArgs.Add("false");

            /*appArgs.Add("-system.config-file");
            appArgs.Add("config.cfg");*/
            //END TEMP DEV CODE

            // Launch logger Manager
            ILoggerManager loggerMng = CreateLoggerManager();

            // Launch Data manager
            var dataMng = new FileDataManager(baseDirectory + "\\data\\");

            // Launch Plugin manager
            var pluginMng = new PluginsManager(loggerMng, baseDirectory + "\\plugins\\");

            // Launch Interface manager
            var interfacesMng = new InterfacesManager(loggerMng, pluginMng);

            // Launch VrManager
            var vrMng = new VrManager(loggerMng, pluginMng);

            // Launch OHM
            app = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);

            if (app.Start(appArgs))
            {
                Console.WriteLine("Console: Press any key to end server instance");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Console: Application can not start. shutdowning.");
            }
            //Shutdown app
            app.Shutdown();
            Console.WriteLine("Console: Press any key to close console");
            Console.ReadKey();
        }

        private static ILoggerManager CreateLoggerManager()
        {
            // Launch Log Console Output Appender
            AppConsoleOutput appender = new AppConsoleOutput();

            // Config Console Layout
            appender.Layout = OHM.Logger.LoggerManager.DefaultPatternLayout;
            
            // Launch Logger Manager arguments
            IList<IAppender> col = new List<IAppender>();
            col.Add(appender);

            // Launch Final Logger Manager
            return new LoggerManager(col);
        }

        private static void Main_Start_Header()
        {
            //Write Header
            Console.WriteLine("OHM Console Server");
        }
    }
}
