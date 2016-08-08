
namespace ZWaveLib.Nodes
{
    public interface IZWaveNode : IZWaveHomeNode
    {
        byte? NodeId { get; }
    }
}
