using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {
        string Key { get; }

        string Name { get; }

        NodeStates State { get; }
    }

    public interface IBasicNode : INode, IPropertiesNode
    {

    }
}
