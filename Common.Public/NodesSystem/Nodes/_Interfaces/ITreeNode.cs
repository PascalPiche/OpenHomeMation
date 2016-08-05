using System.Collections.Generic;

namespace OHM.Nodes
{
    public interface ITreeNode : INode
    {
        string TreeKey { get; }        

        IReadOnlyList<ITreeNode> Children { get; }
    }
}
