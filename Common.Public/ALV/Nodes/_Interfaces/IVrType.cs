using System.Collections.Generic;

namespace OHM.Nodes.ALV
{
    public interface IVrType : INode, ICommandsNode
    {
        IList<string> GetAllowedSubVrType();
    }
}
