using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandDefinition : ICommandDefinition
    {
        #region Private Members

        private string _key;
        private string _name;
        private string _description;
        private IDictionary<string, IArgumentDefinition> _argumentsDefinition;

        #endregion

        #region Public Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        public CommandDefinition(string key, string name)
            : this(key, name, string.Empty) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public CommandDefinition(string key, string name, string description)
            : this(key, name, description, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="argumentsDefinition"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public string Key { get { return _key; } }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get { return _description; } }

        #endregion

        #region Public Api

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, IArgumentDefinition> ArgumentsDefinition
        {
            get { return _argumentsDefinition; }
        }

        #endregion
    }
}
