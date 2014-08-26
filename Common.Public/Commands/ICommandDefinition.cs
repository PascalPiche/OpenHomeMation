using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommandDefinition
    {

        String Key { get; }

        String Description { get; }

        bool ValidateArguments(Dictionary<string, object> arguments);

        Dictionary<String, IArgumentDefinition> ArgumentsDefinition { get; }
    }
}
