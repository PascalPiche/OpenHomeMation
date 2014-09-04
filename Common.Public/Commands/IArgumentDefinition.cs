using System;

namespace OHM.Commands
{
    public interface IArgumentDefinition
    {

        string Key { get; }

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

        IArgumentConverter ArgumentConverter { get; }

        bool ValidateValue(object value);
    }

    
}
