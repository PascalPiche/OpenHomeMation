using System.Collections.Generic;

namespace OHM.Nodes
{
    /// <summary>
    /// System Tree Node interface 
    /// Extend INode
    /// </summary>
    public interface ITreeNode : INode
    {
        /// <summary>
        /// Getter of the unique hierarchical tree key of the node
        /// </summary>
        string TreeKey { get; }        

        /// <summary>
        /// Get all children of the node
        /// </summary>
        /// <value>Readonly list of children TreeNode</value>
        IReadOnlyList<ITreeNode> Children { get; }
    }
}
