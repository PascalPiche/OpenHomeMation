﻿using System;
using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandArgumentDefinition : IArgumentDefinition
    {
        #region Private Members

        private string _key;
        private string _name;
        private Type _type;
        private bool _required;

        #endregion

        #region Public Ctor

        /// <summary>
        /// Create a Command argument Definition
        /// </summary>
        /// <param name="key">Unique key of the command argument for the command</param>
        /// <param name="name">Name of the argument</param>
        /// <param name="type">Type of the argument value expected</param>
        /// <param name="required">Boolean defining the required state of the argument for executing the command</param>
        public CommandArgumentDefinition(string key, string name, Type type, bool required = false)
        {
            _key = key;
            _name = name;
            _type = type;
            _required = required;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Unique key of the argument command
        /// </summary>
        /// <remarks>
        /// Implement :
        ///     IArgumentDefinition.Key
        /// </remarks>
        public string Key { get { return _key; } }

        /// <summary>
        /// Name of the argument command
        /// </summary>
        /// <remarks>
        /// Implement :
        ///     IArgumentDefinition.Name
        /// </remarks>
        public string Name { get { return _name; } }

        /// <summary>
        /// Type of the argument command
        /// </summary>
        /// <remarks>
        /// Implement :
        ///     IArgumentDefinition.Type
        /// </remarks>
        public Type Type { get { return _type; } }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Implement :
        ///     IArgumentDefinition.Required
        /// </remarks>
        public bool Required { get { return _required; } }

        #endregion

        #region Public API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetBool(object value, out bool result)
        {
            bool fctResult = false;
            result = false;

            value = this.ExtractValueFromDictionary(value);
            if (value is bool)
            {
                result = (bool)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = bool.TryParse((string)value, out result);
            }

            return fctResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetUInt16(object value, out ushort result)
        {
            bool fctResult = false;
            result = 0;

            value = this.ExtractValueFromDictionary(value);
            if (value is ushort)
            {
                result = (ushort)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = ushort.TryParse((string)value, out result);
            }

            return fctResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetInt16(object value, out short result)
        {
            bool fctResult = false;
            result = 0;

            value = this.ExtractValueFromDictionary(value);
            if (value is short)
            {
                result = (short)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = short.TryParse((string)value, out result);
            }
            
            return fctResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetUInt32(object value, out uint result)
        {
            bool fctResult = false;
            result = 0;

            value = this.ExtractValueFromDictionary(value);
            if (value is uint)
            {
                result = (uint)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = uint.TryParse((string)value, out result);
            }

            return fctResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetInt32(object value, out int result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is int) // ?Upercast? || value is Int16*/
            {
                result = (int)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = int.TryParse((string)value, out result);
            }
            
            return fctResult;
        }

        public bool TryGetUInt64(object value, out ulong result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is ulong)
            {
                result = (ulong)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = ulong.TryParse((string)value, out result);
            }

            return fctResult;
        }

        public bool TryGetInt64(object value, out long result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is long)
            {
                result = (long)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = long.TryParse((string)value, out result);
            }
            
            return fctResult;
        }

        public bool TryGetDouble(object value, out double result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is double)
            {
                result = (double)value;
                fctResult = true;
            }
            else if (value is string)
            {
                fctResult = double.TryParse(
                    (string)value, 
                    System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, 
                    out result);
            }

            return fctResult;
        }

        public bool TryGetString(object value, out string result)
        {
            bool fctResult = false;
            result = null;
            value = this.ExtractValueFromDictionary(value);

            if (value is string)
            {
                result = (string)value;
                fctResult = true;
            }
            return fctResult;
        }

        #endregion

        #region Private methods

        private object ExtractValueFromDictionary(object value)
        {
            if (value is IDictionary<string, object>)
            {
                IDictionary<string, object> temp = (IDictionary<string, object>)value;
                if (temp.ContainsKey(this.Key))
                {
                    value = temp[this.Key];
                }
            }
            else if (value is IDictionary<string, string>)
            {
                IDictionary<string, string> temp = (IDictionary<string, string>)value;
                if (temp.ContainsKey(this.Key))
                {
                    value = temp[this.Key];
                }
            }
            return value;
        }

        #endregion
    }
}
