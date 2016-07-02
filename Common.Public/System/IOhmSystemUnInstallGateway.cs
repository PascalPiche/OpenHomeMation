using OHM.Logger;
using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemUnInstallGateway
    {
        ILogger Logger { get; }

        bool UnRegisterInterface(string key);

    }
}
