using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommand
    {

        string InterfaceKey { get;}

        string NodeKey { get; }

        ICommandDefinition Definition { get; }

        bool Execute(Dictionary<string, object> arguments);

        bool CanExecute();
    }
}
