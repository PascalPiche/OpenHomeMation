using OHM.Nodes;
using OHM.RAL;
using System.Collections.Generic;

namespace OHM.Commands
{
    public abstract class InterfaceCommandAbstract : CommandAbstract, IInterfaceCommand
    {

        #region Private Members

        private IInterface _interface;

        #endregion

        #region Protected Ctor

        protected InterfaceCommandAbstract(INode node, string key, string name, string description) 
            : this (node, key, name, description, null) { }

        protected InterfaceCommandAbstract(INode node, string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(node, new CommandDefinition(key, name, description, argumentsDefinition)) { }

        #endregion

        #region Public Properties

        public string InterfaceKey
        {
            get { return Interface.Key; }
        }

        #endregion

        #region Public override methods

        public override bool CanExecute()
        {
            return IsStateRunning();
        }

        #endregion

        #region Protected Properties

        protected IInterface Interface
        {
            get
            {
                if (_interface == null)
                {
                    LookupAndStoreInterface(this.Node);
                }

                return _interface;
            }
        }

        #endregion

        #region Private methods

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
