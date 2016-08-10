
namespace OHM.Nodes.Commands
{
    public interface ITreeCommand : ICommand
    {
        string NodeTreeKey { get; }
    }
}
