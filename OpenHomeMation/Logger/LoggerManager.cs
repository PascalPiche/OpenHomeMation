using log4net;
using System;

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
