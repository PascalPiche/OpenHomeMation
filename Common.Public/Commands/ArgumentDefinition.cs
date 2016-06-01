﻿using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    public sealed class ArgumentDefinition : IArgumentDefinition
    {
        private String _key;
        private String _name;
        private Type _type;
        private Boolean _required;

        public ArgumentDefinition(String key, String name, Type t) : this(key, name, t, false) { }

        public ArgumentDefinition(String key, String name, Type t, Boolean required)
        {
            _key = key;
            _name = name;
            _type = t;
            _required = required;
        }

        public String Key { get { return _key; } }

        public String Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public Boolean Required { get { return _required; } }

        public Boolean ValidateValue(Object value)
        {
            Boolean result = false;

            if (_type == typeof(Int32))
            {
                Int32 resultTemp;
                result = TryGetInt32(value, out resultTemp);
            }
            else if (_type == typeof(String))
            {
                String resultTemp;
                result = TryGetString(value, out resultTemp);
            }

            return result;
        }

        public Boolean TryGetInt32(Object value, out Int32 result)
        {
            bool fctResult = false;
            if (value is Dictionary<String, Object>)
            {
                Dictionary<String, Object> temp = (Dictionary<String, Object>)value;
                if (temp.ContainsKey(this.Key))
                {
                    value = temp[this.Key];
                }
            }

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
            if (value is Dictionary<String, Object>)
            {
                Dictionary<String, Object> temp = (Dictionary<String, Object>)value;
                if (temp.ContainsKey(this.Key))
                {
                    value = temp[this.Key];
                }
            }

            if (value is String)
            {
                result = (String)value;
                return true;
            }

            result = null;
            return fctResult;
        }
    }
}
