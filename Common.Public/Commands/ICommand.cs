using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommand
    {
        string Key { get; }

        string Name { get; }

        string NodeFullKey { get; }

        ICommandDefinition Definition { get; }

        bool Execute(IDictionary<string, string> arguments);

        bool CanExecute();
    }

}
