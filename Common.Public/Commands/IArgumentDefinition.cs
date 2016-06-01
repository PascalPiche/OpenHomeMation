using System;

namespace OHM.Commands
{
    public interface IArgumentDefinition
    {

        string Key { get; }

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

        //IArgumentConverter ArgumentConverter { get; }

        bool ValidateValue(object value);

        bool TryGetInt32(object value, out int result);

        bool TryGetString(object value, out string result);
    }

    
}
