using log4net;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Logger
{
    public class ConsoleLoggerManager : ILoggerManager
    {

        public ConsoleLoggerManager()
        {
            
        }

        public ILogger GetLogger(Type type)
        {
            return new ConsoleLogger(LogManager.GetLogger(type));
        }

        public ILogger GetLogger(string name)
        {
            return new ConsoleLogger(LogManager.GetLogger(name));
        }
    }
}
