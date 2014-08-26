using OHM.Plugins;
using OHM.System;

namespace OHM.Logger
{

    public interface IOhmSystem
    {

        IOhmSystemInstallGateway getInstallGateway(IPlugin plugin);
        
    }
}
