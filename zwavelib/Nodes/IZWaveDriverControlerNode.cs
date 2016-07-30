using OHM.Nodes;

namespace ZWaveLib.Data
{
    public interface IZWaveDriverControlerNode : ITreeNode
    {
        uint? HomeId { get; }
    }

}
