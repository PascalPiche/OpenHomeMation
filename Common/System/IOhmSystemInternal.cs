
using OHM.Plugins;
namespace OHM.SYS
{
    public interface IOhmSystemInternal : IOhmSystem
    {
        IOhmSystemInstallGateway GetInstallGateway(IPlugin plugin);

        IAPI API { get; }
    }
}
