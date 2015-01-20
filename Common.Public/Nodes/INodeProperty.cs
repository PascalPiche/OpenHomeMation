using System;

namespace OHM.Nodes
{
    public interface INodeProperty
    {

        string Key { get; }

        string Name { get; }

        Type Type { get; }

        bool SetValue(object val);

        object Value { get; }

    }
}
