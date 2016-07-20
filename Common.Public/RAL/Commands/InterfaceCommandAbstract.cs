using OHM.Nodes;
using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.RAL.Commands
{
    public abstract class InterfaceCommandAbstract : CommandAbstract, IInterfaceCommand
    {
        #region Private Members

        private IInterface _interface;

        #endregion

        #region Protected Ctor

        protected InterfaceCommandAbstract(string key, string name, string description) 
            : this (key, name, description, null) { }

        protected InterfaceCommandAbstract(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(new CommandDefinition(key, name, description, argumentsDefinition)) { }

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
            return IsInterfaceRunning();
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

        private bool IsInterfaceRunning()
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
