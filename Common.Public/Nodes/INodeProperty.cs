using System;

namespace OHM.Nodes
{
    public interface INodeProperty
    {

        string Key { get; }

        string Name { get; }

        Type Type { get; }

        object Value { get; set; }

    }
}
