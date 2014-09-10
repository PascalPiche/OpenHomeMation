using System;

namespace OHM.Commands
{
    public class ArgumentDefinition : IArgumentDefinition
    {
        private string _key;
        private string _name;
        private Type _type;
        private bool _required;

        public ArgumentDefinition(string key, string name, Type t) : this(key, name, t, false) { }

        public ArgumentDefinition(string key, string name, Type t, bool required)
        {
            _key = key;
            _name = name;
            _type = t;
            _required = required;
        }

        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public bool Required { get { return _required; } }

        public bool ValidateValue(object value)
        {

            if (_type == typeof(int))
            {
                int result;
                return TryGetInt(value, out result);
            } 
            else if (_type == typeof(string))
            {
                string result;
                return TryGetString(value, out result);
            }
            return false;
        }

        public bool TryGetInt(object value, out int result)
        {
            if (value is int)
            {
                result = (int)value;
                return true;
            }

            if (value is string)
            {
                return int.TryParse((string)value, out result);
            }

            result = 0;
            return false;
        }

        public bool TryGetString(object value, out string result)
        {
            if (value is string)
            {
                result = (string)value;
                return true;
            }

            result = string.Empty;
            return false;
        }

    }
}
