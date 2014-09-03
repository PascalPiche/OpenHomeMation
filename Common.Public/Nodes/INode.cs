using OHM.Commands;
using System.Collections.Generic;

namespace OHM.Nodes
{
    public interface INode
    {

        string Key { get; }

        string Name { get; }

        IList<ICommand> Commands { get; }

    }
}
