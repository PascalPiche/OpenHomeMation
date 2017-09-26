using System;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// Core interface for a command argument
    /// </summary>
    public interface IArgumentDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get;  }

        /// <summary>
        /// 
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool ValidateValue(object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetBool(object value, out bool result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetUInt16(object value, out ushort result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetInt16(object value, out short result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetUInt32(object value, out uint result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetInt32(object value, out int result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetUInt64(object value, out ulong result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetInt64(object value, out long result);
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetDouble(object value, out double result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetString(object value, out string result);
    }
}
