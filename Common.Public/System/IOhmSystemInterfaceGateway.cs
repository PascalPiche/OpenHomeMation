using OHM.Nodes;

namespace OHM.Sys
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(INode node);

        bool RemoveNode(INode node);

    }
}
