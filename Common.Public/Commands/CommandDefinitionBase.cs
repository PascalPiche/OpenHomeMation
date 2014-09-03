using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    
    public abstract class CommandAbstract : ICommand
    {
        private string _key;
        private string _name;
        private string _description;
        private Dictionary<string, IArgumentDefinition> _argumentsDefinition;

        public CommandAbstract(string key, string name) 
            : this(key, name, string.Empty, null) { }

        public CommandAbstract(string key, string name, string description) 
            : this (key, name, description, null) { }

        public CommandAbstract(
            string key, 
            string name,
            string description, 
            Dictionary<string, IArgumentDefinition> argumentsDefinition
        )
        {
            _key = key;
            _name = name;
            _description = description;
            if (argumentsDefinition != null)
            {
                _argumentsDefinition = argumentsDefinition;
            }
            else
            {
                _argumentsDefinition = new Dictionary<string,IArgumentDefinition>();
            }
            
        }

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public bool ValidateArguments(Dictionary<string, object> arguments)
        {
            bool result = true;
            foreach (var item in ArgumentsDefinition.Values)
            {
                if (item.Required)
                {
                    if (!arguments.ContainsKey(item.Name))
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public Dictionary<string, IArgumentDefinition> ArgumentsDefinition
        {
            get { return _argumentsDefinition; }
        }

        public bool Run(Dictionary<string, object> arguments)
        {
            bool result = false;
            if (ValidateArguments(arguments))
            {
                result = RunImplementation(arguments);
            }
            return result;
        }

        protected abstract bool RunImplementation(Dictionary<string, object> arguments);
    }
}
