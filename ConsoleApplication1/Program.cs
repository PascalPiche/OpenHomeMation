using ConsoleApplication1.Logger;
using OHM.Data;
using OHM.Managers.ALR;
using OHM.Plugins;
using OHM.SYS;
using OHM.VAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static OpenHomeMation ohm;

        static void Main(string[] args)
        {
            bool exit = false;

            var loggerMng = new ConsoleLoggerManager();
            var dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            var pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            var interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            var vrMng = new VrManager(loggerMng, pluginMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
            ohm.Start();

            while (!exit)
            {
                string line = Console.ReadLine(); 

                if (line == "exit") {
                    exit = true;
                }
                else if (line == "help")
                {
                    outputBasicHelp();
                }
                else
                {
                    executeCommand(line);
                }
            }

            ohm.Shutdown();
            Console.WriteLine("Press enter to close");
            Console.ReadLine(); 
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

            IAPIResult result = ohm.API.ExecuteCommand(command, args);
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
