using ConsoleApplication1.Logger;
using log4net.Appender;
using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static OpenHomeMation app;

        static void Main(string[] args)
        {
            CreateApp();

            // Start App
            app.Start();

            // Local variable to detect exit loop
            bool exit = false;

            while (!exit)
            {
                exit = loop();
            }

            app.Shutdown();
            Console.WriteLine("Press enter to close");
            Console.ReadLine(); 
        }

        static ILoggerManager CreateLoggerManager()
        {
            // Create Log Console Output Appender
            AppConsoleOutput appender = new AppConsoleOutput();

            // Config Console Layout
            appender.Layout = OHM.Logger.LoggerManager.DefaultPatternLayout;

            // Create Logger Manager arguments
            IList<IAppender> col = new System.Collections.Generic.List<IAppender>();
            col.Add(appender);

            // Create Final Logger Manager
            return new OHM.Logger.LoggerManager(col);
        }

        static void CreateApp()
        {
            // Create logger Manager
            ILoggerManager loggerMng = CreateLoggerManager();

            // Create Data manager
            var dataMng = new FileDataManager(AppDomain.CurrentDomain.BaseDirectory + "\\data\\");

            // Create Plugin manager
            var pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");

            // Create Interface manager
            var interfacesMng = new InterfacesManager(loggerMng, pluginMng);

            // Create VrManager
            var vrMng = new VrManager(loggerMng, pluginMng);

            // Create OHM
            app = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
        }

        static bool loop()
        {
            string line = Console.ReadLine();

            if (line == "exit")
            {
                return true;
            }
            else if (line == "help")
            {
                outputBasicHelp();
            }
            else
            {
                executeCommand(line);
            }
            return false;
        }

        static void executeCommand(string command)
        {
            Console.WriteLine("Sending Command " + command + "");
            string commandKey = command;
            Dictionary<String, String> args = new Dictionary<string, string>();

            //Split Args and key
            if (command.Contains('?'))
            {
                string[] splitedCommand = command.Split('?');
                commandKey = splitedCommand[0];

                //Fill Args dictionary
                string[] splittedArgs = splitedCommand[1].Split('&');
                foreach (string item in splittedArgs)
                {
                    string[] splittedArg = item.Split('=');
                    if (splittedArg.Length == 2)
                    {
                        args.Add(splittedArg[0], splittedArg[1]);
                    }
                }
            }

            IAPIResult result = app.API.ExecuteCommand(command, args);
            if (result.IsSuccess)
            {
                Console.WriteLine("Command " + command + " successfully executed");
                outputCommandResult(result);
            }
            else
            {
                Console.WriteLine("Command " + command + " was not executed");
            }
        }

        static void outputCommandResult(IAPIResult result)
        {
            //Check Result Type
            if (result.Result is IEnumerable)
            {
                IEnumerable list = result.Result as IEnumerable;
                IEnumerator enu = list.GetEnumerator();

                //Proceed list
                while (enu.MoveNext())
                {
                    outputItemListResult(enu.Current);
                }

            }
            else if (result.Result is Boolean)
            {
                Console.WriteLine(result.Result);
            }
        }

        static void outputItemListResult(object item)
        {
            if (item is IPlugin)
            {
                IPlugin plugin = item as IPlugin;
                Console.WriteLine("PLUGIN : ");
                Console.WriteLine("Name : " + plugin.Name);
                Console.WriteLine("Id : " + plugin.Id);
                Console.WriteLine("State : " + plugin.State);
            }
            else if (item is string)
            {
                Console.WriteLine("STRING : " + item.ToString());
            }
            else
            {
                Console.WriteLine("No conversion to console output found for type : " + item.ToString());
            }
        }

        static void outputBasicHelp()
        {
            Console.WriteLine("-----HELP--------");
            Console.WriteLine("Root nodes : ");
            Console.WriteLine("- plugins");
            Console.WriteLine("- hal");

            Console.WriteLine("Base commands : ");
            Console.WriteLine("- list");
            Console.WriteLine("- execute");
        }
    }
}
