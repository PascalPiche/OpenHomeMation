using OHM.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {

        string Key { get; }

        string Name { get; }

        IList<ICommand> Commands { get; }

        bool CanExecuteCommand(string key);

        bool ExecuteCommand(string key, Dictionary<string, object> arguments);

        IList<INode> Children { get; }

        INode Parent { get; }

        bool AddChild(INode node);

        bool RemoveChild(INode node);

        INode GetChild(string key);

    }
}
