using OHM.Nodes;

namespace ZWaveLib.Data
{
    public interface IZWaveHomeNode : ITreePowerNode 
    {
        uint? HomeId { get; }
    }

    public interface IZWaveDriverControlerNode : IZWaveHomeNode
    {
        
    }
}
