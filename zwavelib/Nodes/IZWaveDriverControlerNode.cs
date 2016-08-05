using OHM.Nodes;

namespace ZWaveLib.Data
{
    public interface IZWaveHomeNode : ITreeNode, INode
    {
        uint? HomeId { get; }
    }

    public interface IZWaveDriverControlerNode : IZWaveHomeNode
    {
        
    }
}
