using log4net;

namespace OHM.Logger
{
    public interface ILoggerManager
    {
        ILog GetLogger(string name);
    }
}
