using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    public interface ITreeCommand : ICommand
    {
        string NodeTreeKey { get; }
    }
}
