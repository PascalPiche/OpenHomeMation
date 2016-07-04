using OHM.Commands;

namespace OHM.RAL.Commands
{

    public interface IInterfaceCommand : ICommand
    {
        string InterfaceKey { get; }

        string NodeKey { get; }
    }

}
