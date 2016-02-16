using ConsoleApplication1.Logger;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static OpenHomeMation ohm;
        private static ILoggerManager loggerMng;
        private static IPluginsManager pluginMng;
        private static IInterfacesManager interfacesMng;

        static void Main(string[] args)
        {
            bool exit = false;

            loggerMng = new ConsoleLoggerManager();
            var dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng);
            ohm.start();

            while (!exit)
            {
                string line = Console.ReadLine(); 

                if (line == "exit") {
                    exit = true;
                }
            }

            ohm.Shutdown();
            //System.Threading.Thread.CurrentThread.Suspend();
        }
    }
}
