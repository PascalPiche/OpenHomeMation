using System.Collections.Generic;

namespace OHM.Commands
{
    public sealed class CommandDefinition : ICommandDefinition
    {

        #region Private Members

        private string _key;
        private string _name;
        private string _description;
        private Dictionary<string, IArgumentDefinition> _argumentsDefinition;

        #endregion

        #region Public Ctor

        public CommandDefinition(string key, string name)
            : this(key, name, string.Empty) { }

        public CommandDefinition(string key, string name, string description)
            : this(key, name, description, null) { }

        public CommandDefinition(
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
                _argumentsDefinition = new Dictionary<string, IArgumentDefinition>();
            }

        }

        #endregion

        #region Public Properties

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

        #endregion

        #region Public Api

        public bool ValidateArguments(Dictionary<string, object> arguments)
        {
            bool result = true;

            //Validate required
            foreach (var item in _argumentsDefinition.Values)
            {
                if (item.Required)
                {
                    if (arguments != null && !arguments.ContainsKey(item.Key))
                    {
                        result = false;
                    }
                }
            }

            if (arguments != null && result != false)
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

        public Dictionary<string, IArgumentDefinition> ArgumentsDefinition
        {
            get { return _argumentsDefinition; }
        }

        #endregion

    }
}
