using OHM.Nodes;
using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommand
    {

        INode Node { get; }

        ICommandDefinition Definition { get; }

        bool Execute(Dictionary<string, object> arguments);

        bool CanExecute();
    }

    public interface ICommandDefinition
    {
        String Key { get; }

        String Name { get; }

        String Description { get; }

        Dictionary<String, IArgumentDefinition> ArgumentsDefinition { get; }

        bool ValidateArguments(Dictionary<string, object> arguments);
    }
}
