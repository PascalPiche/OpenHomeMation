using log4net;
using System;

namespace OHM.Logger
{
    public class LoggerManager : ILoggerManager
    {
        public ILogger GetLogger(string repository, Type type)
        {

            log4net.LogManager.CreateRepository(repository);

            return new DefaultLogger(LogManager.GetLogger(repository, type));
        }

        public ILogger GetLogger(string repository, string name)
        {
            log4net.LogManager.CreateRepository(repository);

            return new DefaultLogger(log4net.LogManager.GetLogger(repository, name));
        }
    }
}
