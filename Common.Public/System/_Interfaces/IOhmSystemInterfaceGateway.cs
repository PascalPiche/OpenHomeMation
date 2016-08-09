using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(AbstractTreeNode node);

        bool RemoveNode(AbstractTreeNode node);
    }
}
