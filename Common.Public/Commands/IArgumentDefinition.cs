using System;

namespace OHM.Commands
{
    public interface IArgumentDefinition
    {

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

    }
}
