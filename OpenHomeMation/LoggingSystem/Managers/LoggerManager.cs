using log4net;
using log4net.Repository;
using System;

namespace OHM.Logger
{
    public class LoggerManager : ILoggerManager
    {



        /// <summary>
        /// Core CTor 
        /// </summary>
        public LoggerManager() : base()
        {
        
        }

        public ILogger GetLogger(string repository, string name)
        {
            ILoggerRepository rep = log4net.LogManager.GetRepository(repository);
            
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
