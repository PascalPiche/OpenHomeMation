using OHM.Nodes;

namespace ZWaveLib.Data
{
    public interface IZWaveNode : IZWaveDriverControlerNode
    {
        byte? NodeId { get; }
    }
}
