using System;

namespace OHM.Commands
{
    public interface IArgumentDefinition
    {

        string Key { get; }

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

    }

    public class ArgumentDefinition : IArgumentDefinition
    {
        private string _key;
        private string _name;
        private Type _type;
        private bool _required;

        public ArgumentDefinition(string key, string name, Type t) : this(key, name, t, false) {}

        public ArgumentDefinition(string key, string name, Type t, bool required)
        {
            _key = key;
            _name = name;
            _type = t;
            _required = required;
        }

        public string Key    { get { return _key; } }

        public string Name   { get { return _name; } }

        public Type Type     { get { return _type; } }

        public bool Required { get { return _required; } }
    }
}
