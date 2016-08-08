using OHM.Nodes;

namespace ZWaveLib.Nodes
{
    public interface IZWaveHomeNode : ITreeNode, INode
    {
        uint? HomeId { get; }
    }

    public interface IZWaveDriverControlerNode : IZWaveHomeNode
    { }
}
