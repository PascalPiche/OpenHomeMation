using System;

namespace OHM.Nodes
{
    public interface INodeProperty
    {
        #region Properties

        string Key { get; }

        string Name { get; }

        string Description { get; }

        bool ReadOnly { get; }

        Type Type { get; }

        object Value { get; }

        #endregion

        #region API

        bool SetValue(object val);

        #endregion

    }
}
