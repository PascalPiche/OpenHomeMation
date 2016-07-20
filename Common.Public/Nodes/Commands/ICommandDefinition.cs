using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    public interface ICommandDefinition
    {
        string Key { get; }

        string Name { get; }

        string Description { get; }

        Dictionary<string, IArgumentDefinition> ArgumentsDefinition { get; }

        bool ValidateArguments(IDictionary<string, string> arguments);
    }
}
