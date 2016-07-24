using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    public abstract class CommandAbstract : ICommand
    {
        #region Private Members

        private ICommandDefinition _definition;
        private INode _node;

        #endregion

        #region Protected Ctor

        protected CommandAbstract(string key, string name)
            : this(key, name, string.Empty) { }

        protected CommandAbstract(string key, string name, string description)
            : this(key, name, description, null) { }

        protected CommandAbstract(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : this(new CommandDefinition(key, name, description, argumentsDefinition)) { }

        protected CommandAbstract(ICommandDefinition definition)
        {
            _definition = definition;
        }

        #endregion

        #region Public Properties

        public string Key { get { return _definition.Key; } }

        public string Name { get { return _definition.Name; } }

        public string NodeFullKey { get { return Node.FullKey; } }

        public ICommandDefinition Definition { get { return _definition; } }

        #endregion

        #region Public Api

        public virtual bool CanExecute()
        {
            bool result = false;

            if (_node != null) {
                result = true;
            }

            return result;
        }

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

        #region Properties

        protected Nodes.INode Node
        {
            get { return _node; }
        }

        #endregion

        #region Methods

        protected abstract bool RunImplementation(IDictionary<string, string> arguments);

        #endregion

        #endregion    
    
        #region Internal Methods

        internal bool Init(INode node)
        {
            _node = node;
            return true;
        }

        #endregion
    }
}
