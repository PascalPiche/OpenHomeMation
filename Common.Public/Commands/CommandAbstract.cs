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
        private IInterface _interface;

        #endregion

        #region Protected Ctor

        protected CommandAbstract(INode node, string key, string name) 
            : this(node, key, name, string.Empty, null) {
                
        }

        protected CommandAbstract(INode node, string key, string name, string description) 
            : this (node, key, name, description, null) { }

        protected CommandAbstract(
            INode node,
            string key,
            string name,
            string description,
            Dictionary<string, IArgumentDefinition> argumentsDefinition
        )
        {
            _node = node;
            _definition = new CommandDefinition(key, name, description, argumentsDefinition);
        }

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

        public string InterfaceKey
        {
            get { return Interface.Key; }
        }

        #endregion

        #region Public Api

        public virtual bool CanExecute()
        {
            return IsStateRunning();
        }


        #endregion

        #region Protected

        protected Nodes.INode Node
        {
            get { return _node; }
        }

        protected IInterface Interface
        {
            get
            {
                if (_interface == null)
                {
                    LookupAndStoreInterface(this._node);
                }
                
                return _interface; 
            }
        }

        protected abstract bool RunImplementation(Dictionary<string, object> arguments);

        #endregion

        #region Internal

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

        #region Private

        private void LookupAndStoreInterface(INode node)
        {

            if (node == null)
            {
                node = this.Node;
            }

            if (node is IInterface)
            {
                _interface = (IInterface)node;
            }
            else if (node.Parent != null)
            {
                LookupAndStoreInterface(node.Parent);
            }
            else
            {
                //TODO log error
            }
        }

        private bool IsStateRunning()
        {
            if (Interface != null)
            {
                return _interface.IsRunning;
            } 
            return false;
        }
        
        #endregion
    }
}
