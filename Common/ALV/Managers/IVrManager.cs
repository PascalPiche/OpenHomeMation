using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.SYS;

namespace OHM.Managers.ALV
{
    public interface IVrManager
    {
        bool Init(ILoggerManager loggerMng, IDataStore data, IOhmSystemInternal system);

        bool RegisterVrType(string key, IVrNodeCreator creator);

        bool CreateRootNode(string model, string key, string name);
    }
}
