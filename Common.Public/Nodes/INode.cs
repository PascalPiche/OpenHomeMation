using OHM.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {

        string Key { get; }

        string Name { get; }

        IReadOnlyList<ICommand> Commands { get; }

        bool CanExecuteCommand(string key);

        bool ExecuteCommand(string commandKey, Dictionary<string, object> arguments);

        IReadOnlyList<INode> Children { get; }

        INode Parent { get; }

        INode GetChild(string key);

    }
}
