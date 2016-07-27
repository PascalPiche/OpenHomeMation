using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{

    public interface INode :  INotifyPropertyChanged
    {
        #region Properties

        string Key { get; }

        string Name { get; }

        IReadOnlyList<INodeProperty> Properties { get; }

        IReadOnlyList<ICommand> Commands { get; }

        #endregion
    }

    public interface ITreeNode : INode
    {
        #region Properties

        string TreeKey { get; }        

        NodeStates State { get; }

        IReadOnlyList<ITreeNode> Children { get; }
        
        #endregion
    }
}
