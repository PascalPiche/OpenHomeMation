using System;

namespace OHM.Logger
{
    public interface ILoggerManager
    {

        ILogger GetLogger(string repository, Type type);

        ILogger GetLogger(string repository, string name);
    }
}
