using OHM.Nodes.Commands;

namespace OHM.RAL.Commands
{
    public interface IInterfaceCommand : ICommand
    {
        string InterfaceKey { get; }
    }
}
