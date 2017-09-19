using OHM.Nodes.Properties;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Nodes
{
    /// <summary>
    /// Node interface with minimal properties
    /// </summary>
    public interface INode : INotifyPropertyChanged
    {
        /// <summary>
        /// System Key property getter.
        /// </summary>
        /// <remarks>
        /// Must be unique in the possible list where it can be.
        /// </remarks>
        string SystemKey { get; }

        /// <summary>
        /// System name property getter.
        /// </summary>
        string SystemName { get; }

        /// <summary>
        /// System state property gettyer.
        /// </summary>
        /// <see cref="SystemNodeStates"/>
        SystemNodeStates SystemState { get; }

        /// <summary>
        /// List all properties of the system node
        /// </summary>
        /// <see cref="INodeProperty"/>
        IReadOnlyList<INodeProperty> Properties { get; }
    }
}
