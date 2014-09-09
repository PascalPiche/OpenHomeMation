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
}
