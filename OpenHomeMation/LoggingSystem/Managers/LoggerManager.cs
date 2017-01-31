using log4net;
using System;

namespace OHM.Logger
{
    public class LoggerManager : ILoggerManager
    {
        public ILogger GetLogger(string repository, Type type)
        {
            try
            {
                log4net.LogManager.CreateRepository(repository);
            }
            catch (Exception)
            {

            }

            return new DefaultLogger(LogManager.GetLogger(repository, type));
        }

        public ILogger GetLogger(string repository, string name)
        {
            try
            {
                log4net.LogManager.CreateRepository(repository);
            }
            catch (Exception)
            {
                
            }

            return new DefaultLogger(log4net.LogManager.GetLogger(repository, name));
        }
    }
}
