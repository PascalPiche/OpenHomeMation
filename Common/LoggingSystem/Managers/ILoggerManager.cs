using System;

namespace OHM.Logger
{
    public interface ILoggerManager
    {

        ILogger GetLogger(Type type);

        ILogger GetLogger(String name);
    }
}
