using OHM.Nodes.Properties;
using System.Collections.Generic;

namespace OHM.Nodes
{
    public interface IPropertiesNode : INode
    {
        IReadOnlyList<INodeProperty> Properties { get; }
    }
}
