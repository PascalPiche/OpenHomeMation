using OHM.Nodes.Properties;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    /// <summary>
    /// System node interface with minimal public properties
    /// </summary>
    public interface INode : INotifyPropertyChanged
    {
        /// <summary>
        /// System Key property of the node.
        /// </summary>
        /// <remarks>
        /// Must be unique in the possible list where it can be.
        /// </remarks>
        string Key { get; }

        /// <summary>
        /// System name property of the node.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// System state of the node.
        /// </summary>
        /// <see cref="NodeStates"/>
        NodeStates State { get; }

        /// <summary>
        /// List all properties of the node
        /// </summary>
        IReadOnlyList<INodeProperty> Properties { get; }
    }
}
