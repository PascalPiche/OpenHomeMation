using log4net;
using System;

namespace OHM.Logger
{
    public interface ILoggerManager
    {

        ILog GetLogger(string name);
    }
}
