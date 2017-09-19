using System.Collections.Generic;

namespace OHM.Nodes
{
    /// <summary>
    /// System Tree Node interface 
    /// Extend INode
    /// </summary>
    public interface ITreeNode : INode
    {
        string TreeKey { get; }        

        IReadOnlyList<ITreeNode> Children { get; }
    }
}
