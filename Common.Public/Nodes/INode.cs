using OHM.Nodes.Commands;
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

        IReadOnlyList<INodeProperty> Properties { get; }

        IReadOnlyList<ICommand> Commands { get; }

        IReadOnlyList<INode> Children { get; }

        //INode Parent { get; }
        
        #endregion

    }
}
