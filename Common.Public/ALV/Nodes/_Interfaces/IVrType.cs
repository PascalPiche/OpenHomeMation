using System.Collections.Generic;

namespace OHM.Nodes.ALV
{
    public interface IVrType : INode, IPropertiesNode, ICommandsNode
    {
        IList<string> GetAllowedSubVrType();
    }
}
