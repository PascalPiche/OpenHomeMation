using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipsAI
{
    public class API
    {

        public static string Eval(String query) {
             return LaunchCommandLineApp(query);
        }

        /// <summary>
        /// Launch the legacy application with some options set.
        /// </summary>
        static string LaunchCommandLineApp(string query)
        {
            // For the example
            /*const string ex1 = "C:\\";
            const string ex2 = "C:\\Dir";*/

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;

            startInfo.FileName = "C:\\Program Files (x86)\\CLIPSDOS64.exe";
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.OutputDataReceived += exeProcess_OutputDataReceived;
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "";
        }

        static void exeProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            sender.ToString();
        }
    }
}
