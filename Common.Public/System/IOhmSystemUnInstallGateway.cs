using OHM.Logger;
using OHM.Nodes;

namespace OHM.Sys
{
    public interface IOhmSystemUnInstallGateway
    {
        ILogger Logger { get; }

        bool UnRegisterInterface(string key);

    }
}
