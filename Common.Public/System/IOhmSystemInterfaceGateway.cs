using OHM.Nodes;

namespace OHM.SYS
{
    public interface IOhmSystemInterfaceGateway
    {
        bool CreateNode(ITreePowerNode node);

        bool RemoveNode(ITreePowerNode node);

    }
}
