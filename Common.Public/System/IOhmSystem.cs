using OHM.Data;
using OHM.Plugins;
using OHM.RAL;

namespace OHM.SYS
{

    public interface IOhmSystemPlugins
    {

        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IOhmSystemUnInstallGateway GetUnInstallGateway(IPlugin plugin);
        
    }
}
