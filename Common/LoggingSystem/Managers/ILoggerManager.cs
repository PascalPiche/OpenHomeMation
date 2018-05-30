using log4net;
using log4net.Core;
using System;

namespace OHM.Logger
{
    public interface ILoggerManager
    {

        ILog GetLogger(string name);
    }
}
