using OHM.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {
        #region Properties

        string FullKey { get; }

        string Key { get; }

        string Name { get; }

        NodeStates State { get; }

        IReadOnlyList<ICommand> Commands { get; }

        IReadOnlyList<INode> Children { get; }

        INode Parent { get; }

        #endregion

        #region API

        bool CanExecuteCommand(string key);

        bool ExecuteCommand(string commandKey, Dictionary<string, string> arguments);

        INode GetChild(string key);

        #endregion

    }
}
