using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Apps.Consoletools
{
    public class ConsoleTools
    {

        public static void writeLineAndWait(string msg)
        {
            System.Console.WriteLine(msg);
            System.Console.ReadKey();
        }
    }
}
