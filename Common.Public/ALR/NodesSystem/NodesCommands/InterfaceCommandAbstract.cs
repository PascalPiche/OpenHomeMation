using OHM.Nodes.ALR;
using System.Collections.Generic;

namespace OHM.Nodes.Commands.ALR
{
    public abstract class InterfaceCommandAbstract : AbstractCommand, IInterfaceCommand
    {
        #region Protected Ctor

        protected InterfaceCommandAbstract(string key, string name, string description) 
            : this(key, name, description, null) { }

        protected InterfaceCommandAbstract(string key, string name, string description, IDictionary<string, IArgumentDefinition> argumentsDefinition)
            : this(new CommandDefinition(key, name, description, argumentsDefinition)) { }
        
        protected InterfaceCommandAbstract(ICommandDefinition definition)
            : base(definition) { }

        #endregion

        #region Public Methods

        public override bool CanExecute()
        {
            return IsInterfaceRunning();
        }

        #endregion

        #region Protected Methods

        protected new ALRAbstractTreeNode Node { get { return base.Node as ALRAbstractTreeNode; } }

        protected ALRInterfaceAbstractNode Interface 
        { 
            get 
            {
                ALRInterfaceAbstractNode result = null;
                if (Node != null)
                {
                    result = Node.Interface; 
                }
                return result;
            }
        }

        protected abstract override bool RunImplementation(IDictionary<string, string> arguments);

        protected bool IsInterfaceRunning()
        {
            bool result = false;
            if (Node != null && Node.Interface != null)
            {
                result = Node.Interface.IsRunning;
            }

            return result;
        }

        #endregion
    }
}
