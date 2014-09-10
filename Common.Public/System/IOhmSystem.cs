using OHM.Interfaces;
using OHM.Plugins;

namespace OHM.Sys
{

    public interface IOhmSystem
    {
        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf);
        
    }
}
