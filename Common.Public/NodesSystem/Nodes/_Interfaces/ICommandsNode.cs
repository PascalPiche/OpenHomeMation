using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.Nodes
{
    /// <summary>
    /// System node interface with commands
    /// Extend INode
    /// </summary>
    public interface ICommandsNode : INode
    {
        /// <summary>
        /// List all commands of the node
        /// </summary>
        IReadOnlyList<ICommand> Commands { get; }
    }
}
