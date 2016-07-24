using System;
using System.Collections;
using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    public sealed class ArgumentDefinition : IArgumentDefinition
    {
        #region Private Members

        private String _key;
        private String _name;
        private Type _type;
        private Boolean _required;

        #endregion

        #region Public Ctor

        public ArgumentDefinition(String key, String name, Type t) : this(key, name, t, false) { }

        public ArgumentDefinition(String key, String name, Type t, Boolean required)
        {
            _key = key;
            _name = name;
            _type = t;
            _required = required;
        }

        #endregion

        #region Public Properties

        public String Key { get { return _key; } }

        public String Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public Boolean Required { get { return _required; } }

        #endregion

        #region Public API

        public Boolean ValidateValue(Object value)
        {
            Boolean result = false;

            if (Type == typeof(Int32))
            {
                Int32 resultTemp;
                result = this.TryGetInt32(value, out resultTemp);
            }
            else if (Type == typeof(string))
            {
                String resultTemp;
                result = this.TryGetString(value, out resultTemp);
            }

            return result;
        }

        public Boolean TryGetBool(Object value, out bool result)
        {
            bool fctResult = false;
            result = false;

            value = this.ExtractValueFromDictionary(value);
            if (value is bool)
            {
                result = (bool)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = Boolean.TryParse((String)value, out result);
            }

            return fctResult;
        }

        public Boolean TryGetUInt16(Object value, out UInt16 result)
        {
            bool fctResult = false;
            result = 0;

            value = this.ExtractValueFromDictionary(value);
            if (value is UInt16)
            {
                result = (UInt16)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = UInt16.TryParse((String)value, out result);
            }

            return fctResult;
        }

        public Boolean TryGetInt16(Object value, out Int16 result)
        {
            bool fctResult = false;
            result = 0;

            value = this.ExtractValueFromDictionary(value);
            if (value is Int16)
            {
                result = (Int16)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = Int16.TryParse((String)value, out result);
            }
            
            return fctResult;
        }

        public Boolean TryGetUInt32(Object value, out UInt32 result)
        {
            bool fctResult = false;
            result = 0;

            value = this.ExtractValueFromDictionary(value);
            if (value is UInt32)
            {
                result = (UInt32)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = UInt32.TryParse((String)value, out result);
            }

            return fctResult;
        }

        public Boolean TryGetInt32(Object value, out Int32 result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is Int32 /*|| value is Int16*/)
            {
                result = (Int32)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = Int32.TryParse((String)value, out result);
            }
            
            return fctResult;
        }

        public Boolean TryGetUInt64(Object value, out UInt64 result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is UInt64)
            {
                result = (UInt64)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = UInt64.TryParse((String)value, out result);
            }

            return fctResult;
        }

        public Boolean TryGetInt64(Object value, out Int64 result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is Int64)
            {
                result = (Int64)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = Int64.TryParse((String)value, out result);
            }
            
            return fctResult;
        }

        public Boolean TryGetDouble(Object value, out Double result)
        {
            bool fctResult = false;
            result = 0;
            value = this.ExtractValueFromDictionary(value);

            if (value is Double)
            {
                result = (Double)value;
                fctResult = true;
            }
            else if (value is String)
            {
                fctResult = Double.TryParse((String)value, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out result);
            }

            return fctResult;
        }

        public Boolean TryGetString(Object value, out String result)
        {
            bool fctResult = false;
            result = null;

            value = this.ExtractValueFromDictionary(value);
            if (value is String)
            {
                result = (String)value;
                fctResult = true;
            }
            
            return fctResult;
        }

        #endregion

        #region Private methods

        private Object ExtractValueFromDictionary(Object value)
        {
            if (value is IDictionary)
            {
                if (value is Dictionary<string, Object>)
                {
                    Dictionary<string, Object> temp = (Dictionary<string, Object>)value;
                    if (temp.ContainsKey(this.Key))
                    {
                        value = temp[this.Key];
                    }
                }
                else if (value is Dictionary<string, string>)
                {
                    Dictionary<string, string> temp = (Dictionary<string, string>)value;
                    if (temp.ContainsKey(this.Key))
                    {
                        value = temp[this.Key];
                    }
                }
            }

            return value;
        }

        #endregion
    }
}
