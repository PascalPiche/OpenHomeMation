using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(AbstractPowerTreeNode node);

        bool RemoveNode(AbstractPowerTreeNode node);
    }
}
