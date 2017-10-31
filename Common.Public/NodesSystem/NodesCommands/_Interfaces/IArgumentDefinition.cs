using System;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// Core interface for a command argument
    /// </summary>
    public interface IArgumentDefinition
    {
        /// <summary>
        /// Unique key of the argument command
        /// </summary>
        /// <remarks>
        /// Must be unique in the node commands list
        /// </remarks>
        string Key { get; }

        /// <summary>
        /// Name of the argument command
        /// </summary>
        string Name { get;  }

        /// <summary>
        /// Type of the argument command
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// True when the argument is required for executing the command
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetBool(object value, out bool result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetUInt16(object value, out ushort result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetInt16(object value, out short result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetUInt32(object value, out uint result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetInt32(object value, out int result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetUInt64(object value, out ulong result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetInt64(object value, out long result);
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetDouble(object value, out double result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Obsolete()]
        bool TryGetString(object value, out string result);
    }
}
