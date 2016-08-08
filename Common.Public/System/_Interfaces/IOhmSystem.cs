using OHM.Plugins;

namespace OHM.SYS
{
    public interface IOhmSystemPlugins
    {
        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IOhmSystemUnInstallGateway GetUnInstallGateway(IPlugin plugin);
    }
}
