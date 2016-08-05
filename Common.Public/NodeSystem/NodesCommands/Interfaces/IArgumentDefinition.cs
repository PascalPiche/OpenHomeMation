using System;

namespace OHM.Nodes.Commands
{
    public interface IArgumentDefinition
    {
        string Key { get; }

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

        bool ValidateValue(object value);

        bool TryGetBool(object value, out bool result);

        bool TryGetUInt16(object value, out ushort result);

        bool TryGetInt16(object value, out short result);

        bool TryGetUInt32(object value, out uint result);

        bool TryGetInt32(object value, out int result);

        bool TryGetUInt64(object value, out ulong result);

        bool TryGetInt64(object value, out long result);
         
        bool TryGetDouble(object value, out double result);

        bool TryGetString(object value, out string result);
    }
}
