using OHM.Interfaces;
using OHM.Plugins;

namespace OHM.Sys
{

    public interface IOhmSystem
    {
        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IOhmSystemUnInstallGateway GetUnInstallGateway(IPlugin plugin);

        IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf);
        
    }
}
