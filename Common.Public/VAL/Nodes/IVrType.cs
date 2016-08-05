using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.VAL
{
    public interface IVrType : INode, IPropertiesNode, ICommandsNode
    {
        IList<string> GetAllowedSubVrType();
    }
}
