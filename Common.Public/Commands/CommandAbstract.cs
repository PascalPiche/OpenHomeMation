using OHM.Interfaces;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Commands
{
    
    public abstract class CommandAbstract : ICommand
    {

        private ICommandDefinition _definition;
        private INode _node;
        #region "Ctor"

        protected CommandAbstract(INode node, string key, string name) 
            : this(node, key, name, string.Empty, null) { }

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

        #region "Public"

        public Nodes.INode Node
        {
            get { return _node; }
        }

        public ICommandDefinition Definition
        {
            get { return _definition; }
        }

        public virtual bool CanExecute()
        {
            return IsStateRunning();
        }

        #endregion

        #region "Protected"

        protected abstract bool RunImplementation(Dictionary<string, object> arguments);

        #endregion

        #region "Internal"

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


        private IInterface LookupInterface(INode node)
        {
            if (_node is IInterface)
                return (IInterface)_node;
            else
            {
                return LookupInterface(_node.Parent);
            }
        }

        private IInterface GetInterface()
        {
            if (_node is IInterface)
                return (IInterface)_node;
            else
            {
                return LookupInterface(_node.Parent);
            }
        }

        private bool IsStateRunning()
        {
            IInterface interf = GetInterface();
            if (interf != null)
            {
                return interf.IsRunning;
            } 
            return false;
        }
    }
}
