using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections;
using System.ServiceModel;

namespace OHM.Apps.ConsoleApp
{
    /// <summary>
    /// Main console Application program
    /// </summary>
    public class ConsoleApp
    {
        #region Public function

        /// <summary>Main function and Entry points</summary>
        /// <param name="args">Arguments passed to the console</param>
        public static void Main(string[] args)
        {
            bool exitRequested;
            Main_Start();
            do {
                exitRequested = Main_LoopExecute(); //Return true if exit command
            } while (!exitRequested);

            Main_End();
        }
        
        #endregion

        #region Private

        #region Main Functions

        private static void Main_Start()
        {
            //Write Header
            System.Console.WriteLine("OHM Console");
        }

        /// <summary>
        /// Main function to process infinite loop
        /// Execute Read, Interpretation And output result
        /// </summary>
        /// <returns>true when exit is requested</returns>
        private static bool Main_LoopExecute()
        {
            //Local member for exit request
            bool exitRequested = false;

            // READ INPUT
            string line = Main_LoopExecute_Read();

            // Detect Exit command
            exitRequested = IsExitCommand(line);

            // Process if exit not requested
            if (!exitRequested)
            {
                //Process Local command
                if (!ProcessConsoleCommand(line))
                {
                    //Refactor? Do we have a local _instance or a connection to a remote _instance?
                    //One or multiple connection?
                    // No Local command found
                    // Potentially a command to a _instance of OHM
                    if (!executeCommand(line))
                    {
                        // Command Not found
                        System.Console.WriteLine("Command not found : " + line);
                    }
                }
            }
            
            // return the exit requested command
            return exitRequested;
        }

        /// <summary>
        /// Shutdown and destroy local _instance or close connection to remote
        /// Show last message to the console
        /// </summary>
        private static void Main_End()
        {
            //Shutdown local host if available
            /*if (embedInstance != null)
            {
                embedInstance.Shutdown();
            }*/

            //Wait before final close for last logging
            System.Console.WriteLine("Press enter to close");
            System.Console.ReadLine();
        }

        /// <summary>
        /// Read command from user console
        /// </summary>
        /// <returns>The line writted by the user as a string</returns>
        private static string Main_LoopExecute_Read()
        {
            //Write indicateur
            System.Console.Write(">");

            // Wait Carret caracter to return the line typed
            return System.Console.ReadLine();
        }

        #endregion

        /// <summary>
        /// Detect if the string is the exit command
        /// </summary>
        /// <param name="line">The string line to check</param>
        /// <returns>true if the line is the Exit command</returns>
        private static bool IsExitCommand(string line)
        {
            // Detection
            return (line.ToUpper() == "EXIT");
        }

        /// <summary>
        /// Detect and process internal Console command
        /// Will return true if the command is handled as an internal command
        /// </summary>
        /// <param name="line">The command line to interpret</param>
        /// <returns>Return true if the command was detected as an internal command</returns>
        private static bool ProcessConsoleCommand(string line)
        {
            // Local variable for result
            bool result = false;

            // Interpret help command
            if (line.ToUpper() == "HELP")
            {
                outputBasicHelp();
                result = true;
            }
            /*else if (line.ToUpper() == "LAUNCH-LOCAL")
            {
                LaunchLocal();
                result = true;
            }*/
            else if (line.ToUpper() == "CONNECT")
            {
                launchConnectWizzard();
                result = true;
            }
            return result;
        }

        private static IOpenHomeMationServer launchConnectWizzard()
        {
            System.Console.Write("Enter Ip: ");
            string enteredIp = System.Console.ReadLine();

            //Todo valid ip

            //Try Connect
            WSDualHttpBinding myBinding = new WSDualHttpBinding();
            EndpointAddress myEndpoint = new EndpointAddress("http://localhost:8080/ohm/api/");
            var temp = new ServerCallback();
            DuplexChannelFactory<IOpenHomeMationServer> myChannelFactory = new DuplexChannelFactory<IOpenHomeMationServer>(temp, myBinding, myEndpoint);
            IOpenHomeMationServer server = myChannelFactory.CreateChannel();

            //Try Login
            server.LoginAsync("test");

            //Channel created... return server?
            return server;
        }

        /// <summary>
        /// TODO Execute the command
        /// </summary>
        /// <param name="command">the command string to execute</param>
        private static bool executeCommand(string command)
        {
            return false;
            /*Console.WriteLine("Sending Command " + command + "");
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
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private static void outputCommandResult(IAPIResult result)
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
                System.Console.WriteLine(result.Result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private static void outputItemListResult(object item)
        {
            if (item is IPlugin)
            {
                IPlugin plugin = item as IPlugin;
                System.Console.WriteLine("PLUGIN : ");
                System.Console.WriteLine("Name : " + plugin.Name);
                System.Console.WriteLine("Id : " + plugin.Id);
                System.Console.WriteLine("State : " + plugin.State);
            }
            else if (item is string)
            {
                System.Console.WriteLine("STRING : " + item.ToString());
            }
            else
            {
                System.Console.WriteLine("No conversion to console output found for type : " + item.ToString());
            }
        }

        /*#region Embed Instance OpenHomeMation part

        /// <summary>
        /// Private local member of the Embeded Local Instance Controller
        /// </summary>
        private static EmbedInstanceControler embedInstance;

        /// <summary>
        /// Launch and launch the local Embeded _instance
        /// </summary>
        private static bool LaunchLocal()
        {
            bool result = false;

            if (embedInstance == null)
            {
                embedInstance = new EmbedInstanceControler();
                embedInstance.Create();
                result = embedInstance.Start();
            }
            return result;
        }*/

        #region Help Output

        /// <summary>
        /// Output basic help command
        /// </summary>
        private static void outputBasicHelp()
        {
            System.Console.WriteLine("------- HELP GENERAL ---------");
            System.Console.WriteLine("----- CONSOLE COMMANDS --------");
            System.Console.WriteLine("exit            : Exit console application. Will shutdown local instance and disconnect from remote instance");
            //System.Console.WriteLine("launch-local    : Create and launch a local Embed instance in the console");
            //System.Console.WriteLine("discover-local : Search for _instance on the localhost");
            //System.Console.WriteLine("connect        : ");

            System.Console.WriteLine("----------System nodes --------");
            System.Console.WriteLine("Root nodes : ");
            System.Console.WriteLine("- plugins");
            System.Console.WriteLine("- hal");

            //System.Console.WriteLine("Base commands : ");
            //System.Console.WriteLine("- list");
            //System.Console.WriteLine("- execute");
        }

        #endregion

        #endregion
    }
}

public class ServerCallback : IOpenHomeMationServerCallback
{

    public void CallBackFunction(string str)
    {
        throw new NotImplementedException();
    }
}
