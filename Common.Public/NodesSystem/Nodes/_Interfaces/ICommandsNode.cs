using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.Nodes
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandsNode : INode
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<ICommand> Commands { get; }
    }
}
