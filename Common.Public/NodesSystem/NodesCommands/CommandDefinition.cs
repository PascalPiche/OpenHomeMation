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
        /// <param name="description"></param>
        public CommandDefinition(string key, string name, string description)
            : this(key, name, description, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Unique key of the command</param>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Short description</param>
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
