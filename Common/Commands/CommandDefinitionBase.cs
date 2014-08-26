using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    [Serializable]
    class CommandDefinitionBase : ICommandDefinition
    {
        private string _key;
        private string _description;
        private Dictionary<string, IArgumentDefinition> _argumentsDefinition;

        public CommandDefinitionBase(string key, string description)
        {
            _key = key;
            _description = description;
        }

        public CommandDefinitionBase(
            string key, 
            string description, 
            Dictionary<string, IArgumentDefinition> argumentsDefinition
        ) : this(key, description)
        {
            _argumentsDefinition = argumentsDefinition;
        }

        public string Key
        {
            get { return _key; }
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
            get { throw new NotImplementedException(); }
        }
    }
}
