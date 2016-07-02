using OHM.Data;
using OHM.Plugins;
using OHM.RAL;

namespace OHM.SYS
{

    public interface IOhmSystem
    {
        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IOhmSystemUnInstallGateway GetUnInstallGateway(IPlugin plugin);

        IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf);
        
        IDataStore GetOrCreateDataStore(string key);
    }
}
