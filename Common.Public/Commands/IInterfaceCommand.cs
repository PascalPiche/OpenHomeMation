using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Commands
{

    public interface IInterfaceCommand : ICommand
    {
        string InterfaceKey { get; }

        string NodeKey { get; }
    }

}
