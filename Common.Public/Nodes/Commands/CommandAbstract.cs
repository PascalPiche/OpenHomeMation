using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Nodes.Commands
{

    public abstract class CommandAbstract : ICommand
    {
        #region Private Members

        private ICommandDefinition _definition;
        private NodeAbstract _node;

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

        #region Public

        public string Key { get { return _definition.Key; } }

        public string Name { get { return _definition.Name; } }

        public ICommandDefinition Definition { get { return _definition; } }

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

        protected NodeAbstract Node { get { return _node; } }

        protected abstract bool RunImplementation(IDictionary<string, string> arguments);

        #endregion

        #region Internal Methods

        internal virtual bool Init(NodeAbstract node)
        {
            _node = node;
            return true;
        }

        #endregion
    }

    public abstract class TreeCommandAbstract : CommandAbstract, ITreeCommand
    {
        protected TreeCommandAbstract(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : this(new CommandDefinition(key, name, description, argumentsDefinition)) { }

        protected TreeCommandAbstract(ICommandDefinition definition)
            :base(definition) { }

        public string NodeTreeKey { get { return ((TreeNodeAbstract)Node).TreeKey; } }

        internal bool Init(TreeNodeAbstract node)
        {
            return base.Init(node);
        }
    }
}
