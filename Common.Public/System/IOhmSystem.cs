using OHM.Interfaces;
using OHM.Plugins;
using OHM.Sys;

namespace OHM.Sys
{

    public interface IOhmSystem
    {

        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf);
        
    }
}
