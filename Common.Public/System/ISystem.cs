using OHM.Plugins;
using OHM.System;

namespace OHM.System
{

    public interface IOhmSystem
    {

        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);
        
    }
}
