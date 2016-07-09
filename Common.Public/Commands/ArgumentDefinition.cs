using System;
using System.Collections;
using System.Collections.Generic;

namespace OHM.Commands
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
                result = TryGetInt32(value, out resultTemp);
            }
            else if (Type == typeof(string))
            {
                String resultTemp;
                result = TryGetString(value, out resultTemp);
            }

            return result;
        }

        public Boolean TryGetInt32(Object value, out Int32 result)
        {
            bool fctResult = false;
            value = ExtractValueFromDictionary(value);

            if (value is Int32)
            {
                result = (Int32)value;
                return true;
            }

            if (value is String)
            {
                return Int32.TryParse((String)value, out result);
            }

            result = -1;
            return fctResult;
        }

        public Boolean TryGetString(Object value, out String result)
        {
            bool fctResult = false;
            value = ExtractValueFromDictionary(value);

            if (value is String)
            {
                result = (String)value;
                return true;
            }

            result = null;
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
