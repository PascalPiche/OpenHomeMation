using OHM.Nodes.Properties;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {
        string Key { get; }

        string Name { get; }

        NodeStates State { get; }

        IReadOnlyList<INodeProperty> Properties { get; }
    }
}
