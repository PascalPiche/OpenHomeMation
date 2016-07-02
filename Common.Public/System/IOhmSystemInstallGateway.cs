using OHM.Logger;
using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInstallGateway
    {
        ILogger Logger { get; }

        bool RegisterInterface(string key);

        bool RegisterVrType(string key);
    }
}
