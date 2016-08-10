using OHM.Nodes.ALR;
using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.Nodes.ALR.Commands
{
    public abstract class InterfaceCommandAbstract : TreeCommandAbstract, IInterfaceCommand
    {

        #region Protected Ctor

        protected InterfaceCommandAbstract(string key, string name, string description) 
            : this (key, name, description, null) { }

        protected InterfaceCommandAbstract(string key, string name, string description, IDictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(new CommandDefinition(key, name, description, argumentsDefinition)) { }

        #endregion

        #region Public Methods

        public override bool CanExecute()
        {
            return IsInterfaceRunning();
        }

        #endregion

        #region Protected Methods

        protected new ALRAbstractTreeNode Node { get { return base.Node as ALRAbstractTreeNode; } }

        protected ALRInterfaceAbstractNode Interface { get { return ((ALRAbstractTreeNode)base.Node).Interface; } }

        protected abstract override bool RunImplementation(IDictionary<string, string> arguments);

        protected bool IsInterfaceRunning()
        {
            return this.Node.Interface.IsRunning;
        }

        #endregion
    }
}
