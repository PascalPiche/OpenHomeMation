using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections;

//TODO: CHANGE NAMESPACE
namespace ConsoleApplication1
{
    /// <summary>
    /// Main console Program
    /// </summary>
    public class Program
    {
        #region Public function

        /// <summary>Main function and Entry points</summary>
        /// <param name="args">Arguments passed to the console</param>
        public static void Main(string[] args)
        {
            // Local variable to detect exit Main_LoopExecute
            bool exitRequested = false;

            // Main start routine
            Main_Start();

            // Core Main INFINITE Loop
            while (!exitRequested)
            {
                // Main loop code : Return true if exit command
                exitRequested = Main_LoopExecute();
            }

            //Main end routine
            Main_End();
        }
        
        #endregion

        #region Private

        #region Main Functions

        /// <summary>
        /// 
        /// </summary>
        private static void Main_Start()
        {
            //Write Header
            Console.WriteLine("OHM Console");
        }

        /// <summary>
        /// Main function to process infinite loop
        /// Execute Read, Interpretation And output result
        /// </summary>
        /// <returns></returns>
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
                    // No Local command found
                    // Potentially a command to a instance of OHM
                    if (!executeCommand(line))
                    {
                        // Command Not found
                        Console.WriteLine("Command not found : " + line);
                    }
                }
            }
            
            // return the exit requested command
            return exitRequested;
        }

        /// <summary>
        /// Shutdown and destroy local instance or close connection to remote
        /// Show last message to the console
        /// </summary>
        private static void Main_End()
        {
            //Shutdown local host if available
            if (embedInstance != null)
            {
                embedInstance.Shutdown();
            }

            //Wait before final close for last logging
            Console.WriteLine("Press enter to close");
            Console.ReadLine();
        }

        #endregion

        /// <summary>
        /// Read command from user console
        /// </summary>
        /// <returns>The line writted by the user as a string</returns>
        private static string Main_LoopExecute_Read()
        {
            //Write indicateur
            Console.Write(">");

            // Wait Carret caracter to return the line typed
            return Console.ReadLine();
        }

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
        /// Will return true if the command is handled
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
            else if (line.ToUpper() == "LAUNCH-LOCAL")
            {
                LaunchLocal();
                result = true;
            }
            else if (line.ToUpper() == "")
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
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
                Console.WriteLine(result.Result);
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

        #region Embed Instance OpenHomeMation part

        /// <summary>
        /// Private local member of the Embeded Local Instance Controller
        /// </summary>
        private static EmbedInstanceControler embedInstance;

        /// <summary>
        /// Create and lanch the local Embeded instance
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
        }

        #endregion

        #region Help Output

        /// <summary>
        /// Output basic help command
        /// </summary>
        private static void outputBasicHelp()
        {
            Console.WriteLine("------- HELP GENERAL ---------");
            Console.WriteLine("----- CONSOLE COMMANDS --------");
            Console.WriteLine("exit            : Exit console application. Will shutdown local instance and disconnect from remote instance");
            Console.WriteLine("launch-local    : Create and launch a local Embed instance in the console");
            //Console.WriteLine("discover-local : Search for instance on the localhost");
            //Console.WriteLine("connect        : ");

            Console.WriteLine("----------System nodes --------");
            Console.WriteLine("Root nodes : ");
            Console.WriteLine("- plugins");
            Console.WriteLine("- hal");

            //Console.WriteLine("Base commands : ");
            //Console.WriteLine("- list");
            //Console.WriteLine("- execute");
        }

        #endregion
    }
        #endregion
}
