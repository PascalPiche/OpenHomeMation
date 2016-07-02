using OHM.Interfaces;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Commands
{
    
    public abstract class CommandAbstract : ICommand
    {
        #region Private Members

        private ICommandDefinition _definition;
        private INode _node;

        #endregion

        #region Protected Ctor

        protected CommandAbstract(INode node, string key, string name)
            : this(node, key, name, string.Empty) { }

        protected CommandAbstract(INode node, string key, string name, string description) 
            : this (node, key, name, description, null) { }
        
        protected CommandAbstract(INode node, string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : this(node, new CommandDefinition(key, name, description, argumentsDefinition)) { }

        protected CommandAbstract(INode node, ICommandDefinition definition)
        {
            _node = node;
            _definition = definition;
        }

        #endregion

        #region Public Properties

        public ICommandDefinition Definition
        {
            get { return _definition; }
        }
        
        public string NodeKey
        {
            get { return Node.Key; }
        }

        #endregion

        #region Public Api

        public virtual bool CanExecute()
        {
            return true;
        }

        public bool Execute(Dictionary<string, object> arguments)
        {
            bool result = false;
            if (_definition.ValidateArguments(arguments))
            {
                result = RunImplementation(arguments);
            }
            return result;
        }

        #endregion

        #region Protected

        protected Nodes.INode Node
        {
            get { return _node; }
        }

        protected abstract bool RunImplementation(Dictionary<string, object> arguments);

        #endregion

    }
}
