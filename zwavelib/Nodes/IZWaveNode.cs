using OHM.Nodes;

namespace ZWaveLib.Data
{
    public interface IZWaveNode : IZWaveHomeNode
    {
        byte? NodeId { get; }
    }
}
