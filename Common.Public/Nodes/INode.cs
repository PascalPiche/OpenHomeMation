using OHM.Commands;
using System.Collections.Generic;

namespace OHM.Nodes
{
    public interface INode
    {

        string Key { get; }

        string Name { get; }

        IList<ICommandDefinition> Commands { get; }

        bool RunCommand(string key, Dictionary<string, object> arguments);

    }
}
