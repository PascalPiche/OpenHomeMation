using System;

namespace OHM.Commands
{
    public interface IArgumentDefinition
    {
        string Key { get; }

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

        bool ValidateValue(object value);

        bool TryGetBool(object value, out Boolean result);

        bool TryGetUInt16(object value, out UInt16 result);

        bool TryGetInt16(object value, out Int16 result);

        bool TryGetUInt32(object value, out UInt32 result);

        bool TryGetInt32(object value, out Int32 result);

        bool TryGetUInt64(object value, out UInt64 result);

        bool TryGetInt64(object value, out Int64 result);

        bool TryGetDouble(object value, out Double result);

        bool TryGetString(object value, out string result);
    }
}
