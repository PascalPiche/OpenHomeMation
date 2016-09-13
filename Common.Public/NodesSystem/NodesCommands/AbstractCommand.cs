using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        #region Private Members

        private ICommandDefinition _definition;
        private ICommandsNode _node;

        #endregion

        #region Protected Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        protected AbstractCommand(string key, string name)
            : this(key, name, string.Empty) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        protected AbstractCommand(string key, string name, string description)
            : this(key, name, description, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="argumentsDefinition"></param>
        protected AbstractCommand(string key, string name, string description, IDictionary<string, IArgumentDefinition> argumentsDefinition)
            : this(new CommandDefinition(key, name, description, argumentsDefinition)) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definition"></param>
        protected AbstractCommand(ICommandDefinition definition)
        {
            _definition = definition;
        }

        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        public string Key { get { return _definition.Key; } }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return _definition.Name; } }

        /// <summary>
        /// 
        /// </summary>
        public ICommandDefinition Definition { get { return _definition; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool CanExecute()
        {
            bool result = false;

            if (_node != null) {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public bool Execute(IDictionary<string, string> arguments)
        {
            bool result = false;
            if (this.CanExecute() && this.Definition.ValidateArguments(arguments))
            {
                result = this.RunImplementation(arguments);
            }
            return result;
        }

        #endregion

        #region Protected

        /// <summary>
        /// 
        /// </summary>
        protected ICommandsNode Node { get { return _node; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected abstract bool RunImplementation(IDictionary<string, string> arguments);

        #endregion

        #region Internal Methods

        internal virtual bool Init(ICommandsNode node)
        {
            _node = node;
            return true;
        }

        #endregion
    }
}
