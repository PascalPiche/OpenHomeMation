using OHM.Nodes;
using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.RAL.Commands
{
    public abstract class InterfaceCommandAbstract : TreeCommandAbstract, IInterfaceCommand
    {

        #region Protected Ctor

        protected InterfaceCommandAbstract(string key, string name, string description) 
            : this (key, name, description, null) { }

        protected InterfaceCommandAbstract(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(new CommandDefinition(key, name, description, argumentsDefinition)) { }

        #endregion

        protected new ALRAbstractNode Node { get { return base.Node as ALRAbstractNode; } }

        protected ALRInterfaceAbstractNode Interface { get { return ((ALRAbstractNode)base.Node).Interface; } }

        #region Public override methods

        public override bool CanExecute()
        {
            return IsInterfaceRunning();
        }

        #endregion

        #region Private methods

        private bool IsInterfaceRunning()
        {
            return this.Node.Interface.IsRunning;
        }

        #endregion

        protected abstract override bool RunImplementation(IDictionary<string, string> arguments);
    }
}
