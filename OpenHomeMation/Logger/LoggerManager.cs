using log4net;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Logger
{
    public class LoggerManager : ILoggerManager
    {

        public ILogger GetLogger(Type type)
        {
            return new DefaultLogger(LogManager.GetLogger(type));
        }

        public ILogger GetLogger(string name)
        {
            return new DefaultLogger(log4net.LogManager.GetLogger(name));
        }
    }

}
