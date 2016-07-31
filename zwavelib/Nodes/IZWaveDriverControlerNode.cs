using OHM.Nodes;

namespace ZWaveLib.Data
{
    public interface IZWaveHomeNode : ITreeNode 
    {
        uint? HomeId { get; }
    }

    public interface IZWaveDriverControlerNode : IZWaveHomeNode
    {
        
    }
}
