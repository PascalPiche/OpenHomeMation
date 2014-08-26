using OHM.Commands;
using System.Collections.Generic;

namespace OHM.Nodes
{
    public interface INode
    {

        string Key { get; }

        IList<ICommandDefinition> Commands { get; }

        bool RunCommand(ICommandDefinition command, Dictionary<string, object> arguments);

    }
}
