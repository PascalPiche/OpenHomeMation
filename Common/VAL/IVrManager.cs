using OHM.Data;
using OHM.Plugins;
using OHM.SYS;

namespace OHM.VAL
{
    public interface IVrManager
    {

        bool Init(IDataStore data, IOhmSystemInternal system);

        bool RegisterVrType(string key, IPlugin plugin);

        bool CreateRootNode(string model, string key, string name);
    }
}
