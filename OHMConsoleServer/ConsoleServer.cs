using log4net.Appender;
using OHM.Apps.Console.Tools.Logger;
using OHM.Apps.Consoletools;
using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;

namespace OHM.Apps.ConsoleServer
{

    /// <summary>
    /// Main Server Console Program
    /// </summary>
    class ConsoleServer
    {

        /// <summary>
        /// Main static subroutine for the Console Server Application
        /// </summary>
        /// <param name="args">List of arguments used to configure the server </param>
        static void Main(string[] args)
        {
            Main_Start_Header();
            OpenHomeMation app = createApp();
            
            if (app.Start(parseArgs(args)))
            {
                ConsoleTools.writeLineAndWait("Console: Press any key to end server instance");
            }
            else
            {
                System.Console.WriteLine("Console: Application can not start. shutdowning.");
            }

            //Shutdown app
            app.Shutdown();
            ConsoleTools.writeLineAndWait("Console: Press any key to close console");
        }

        #region Private

        private static OpenHomeMation createApp()
        {
            String baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            IPluginsManager pluginMng = new PluginsManager(baseDirectory + "\\plugins\\");
            DataManagerAbstract dataMng = new FileDataManager(baseDirectory + "\\data\\");
            IInterfacesManager interfacesMng = new InterfacesManager(pluginMng);

            return OpenHomeMation.Create(
                pluginMng,
                dataMng,
                CreateLoggerManager(),
                interfacesMng,
                new VrManager()
            );
        }

        private static List<string> parseArgs(string[] args)
        {
            // TODO: ? Trim args
            List<string> appArgs = new List<string>(args);

            // TODO REMOVES
            // TEMP DEV CODE
            _DEBUG_InjectArgs(appArgs);
            // END TEMP DEV CODE

            return appArgs;
        }

        /// <summary>
        /// Generate full configured Logger Manager for console output
        /// </summary>
        /// <returns>A new ILoggerManager with output appender and a defaultPatternlayout</returns>
        private static ILoggerManager CreateLoggerManager()
        {
            // Create Log Console Output Appender
            AppConsoleOutput consoleAppender = new AppConsoleOutput();
            consoleAppender.Layout = OHM.Logger.LoggerManager.DefaultPatternLayout;
            
            // Prepare Logger Manager arguments
            IList<IAppender> col = new List<IAppender>();
            col.Add(consoleAppender);

            // Create Final Logger Manager for the app
            return new LoggerManager(col);
        }

        /// <summary>
        /// Output basic Console Header
        /// </summary>
        private static void Main_Start_Header()
        {
            //Write Header
            System.Console.WriteLine("Console: Starting Console Server");
            System.Console.WriteLine("OHM Console Server");
        }

        #region _DEBUG

        private static void _DEBUG_InjectArgs(List<string> appArgs)
        {
            //FORCE SERVER ENABLED = TRUE;
            appArgs.Add("-server-api.enabled");
            appArgs.Add("true");
            appArgs.Add("-server-api.port");
            appArgs.Add("8080");
            appArgs.Add("-system.require-server-api");
            appArgs.Add("true");

            appArgs.Add("-system.launch-on-start");
            appArgs.Add("false");

            /*appArgs.Add("-system._config-file");
            appArgs.Add("_config.cfg");*/
        }

        #endregion

        #endregion
    }
}
