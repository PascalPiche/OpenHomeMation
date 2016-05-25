using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommand
    {

        ICommandDefinition Definition { get; }

        bool Execute(Dictionary<string, object> arguments);

        bool CanExecute();
    }

    public interface IInterfaceCommand : ICommand
    {
        string InterfaceKey { get; }

        string NodeKey { get; }
    }
}
