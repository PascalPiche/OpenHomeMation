using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(INode node);

        bool RemoveNode(INode node);

    }
}
