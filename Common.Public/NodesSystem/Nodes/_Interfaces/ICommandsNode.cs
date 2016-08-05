using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.Nodes
{
    public interface ICommandsNode : INode
    {
        IReadOnlyList<ICommand> Commands { get; }
    }
}
