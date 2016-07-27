using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(ITreeNode node);

        bool RemoveNode(ITreeNode node);

    }
}
