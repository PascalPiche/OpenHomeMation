using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;

namespace OHM.Sys
{
    public interface IOhmSystemInstallGateway
    {
        ILogger Logger { get; }

        bool RegisterInterface(string key);

    }

    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(INode node);

        bool RemoveNode(INode node);

    }
}
