using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(TreeNodeAbstract node);

        bool RemoveNode(TreeNodeAbstract node);

    }
}
