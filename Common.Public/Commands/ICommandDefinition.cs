using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommandDefinition
    {
        String Key { get; }

        String Name { get; }

        String Description { get; }

        Dictionary<String, IArgumentDefinition> ArgumentsDefinition { get; }

        bool ValidateArguments(Dictionary<string, object> arguments);
    }
}
