using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommand
    {

        String Key { get; }

        String Name { get; }

        String Description { get; }

        bool ValidateArguments(Dictionary<string, object> arguments);

        Dictionary<String, IArgumentDefinition> ArgumentsDefinition { get; }

        bool Run(Dictionary<string, object> arguments);
    }
}
