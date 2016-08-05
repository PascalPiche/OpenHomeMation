using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    public sealed class CommandDefinition : ICommandDefinition
    {
        #region Private Members

        private string _key;
        private string _name;
        private string _description;
        private IDictionary<string, IArgumentDefinition> _argumentsDefinition;

        #endregion

        #region Internal Ctor

        public CommandDefinition(string key, string name)
            : this(key, name, string.Empty) { }

        public CommandDefinition(string key, string name, string description)
            : this(key, name, description, null) { }

        public CommandDefinition(string key, string name, string description, IDictionary<string, IArgumentDefinition> argumentsDefinition)
        {
            _key = key;
            _name = name;
            _description = description;
            _argumentsDefinition = argumentsDefinition;

            if (argumentsDefinition == null)
            {
                _argumentsDefinition = new Dictionary<string, IArgumentDefinition>();
            }
        }

        #endregion

        #region Public Properties

        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public string Description { get { return _description; } }

        #endregion

        #region Public Api

        public bool ValidateArguments(IDictionary<string, string> arguments)
        {
            bool result = true;

            //Validate required
            foreach (var item in _argumentsDefinition.Values)
            {
                if (item.Required)
                {
                    if (arguments == null || !arguments.ContainsKey(item.Key) || arguments[item.Key] == null)
                    {
                        result = false;
                    }
                }
            }

            //Validate type value
            if (result != false && arguments != null)
            {
                foreach (var item in arguments)
                {
                    var argDef = _argumentsDefinition[item.Key];
                    if (!argDef.ValidateValue(item.Value))
                    {
                        result = false;
                    }
                }
            }
            
            return result;
        }

        public IDictionary<string, IArgumentDefinition> ArgumentsDefinition
        {
            get { return _argumentsDefinition; }
        }

        #endregion
    }
}
