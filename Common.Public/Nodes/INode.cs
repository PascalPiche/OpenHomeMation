using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {
        string Key { get; }

        string Name { get; }

        NodeStates State { get; }
    }

    public interface INodeProperties
    {
        IReadOnlyList<INodeProperty> Properties { get; }
    }

    public interface INodeCommands
    {
        IReadOnlyList<ICommand> Commands { get; }
    }

    public interface IPowerNode :  INode, INodeProperties, INodeCommands
    {
       
    }

    public interface ITreePowerNode : IPowerNode
    {

        #region Properties

        string TreeKey { get; }        

        IReadOnlyList<ITreePowerNode> Children { get; }
        
        #endregion
    }
}
