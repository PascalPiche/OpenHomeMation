
using OHM.Data;
using OHM.Plugins;

namespace OHM.Interfaces
{
    public interface IInterfacesManager
    {

        bool Init(IDataStore data);

        bool RegisterInterface(string key, IPlugin plugin);
    }

}
