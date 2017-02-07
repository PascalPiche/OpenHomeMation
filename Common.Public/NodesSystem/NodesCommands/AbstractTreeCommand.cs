
using System.Collections.Generic;
namespace OHM.Nodes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractTreeCommand : AbstractCommand, ITreeCommand
    {
        #region Protected Ctor

        protected AbstractTreeCommand(string key, string name)
            : this(key, name, string.Empty) {}

        protected AbstractTreeCommand(string key, string name, string description)
            : this(key, name, description, null) {}

        protected AbstractTreeCommand(string key, string name, string description, IDictionary<string, IArgumentDefinition> argumentsDefinition)
            : this(new CommandDefinition(key, name, description, argumentsDefinition)) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definition"></param>
        protected AbstractTreeCommand(ICommandDefinition definition)
            :base(definition) {}

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string NodeTreeKey { 
            get {
                string result = string.Empty;
                if (Node != null)
                {
                    result = ((ITreeNode)Node).TreeKey; 
                }
                return result;
            }
        }

        #endregion

        #region Internal Methods

        internal override bool Init(ICommandsNode node)
        {
            if (node is ITreeNode)
            {
                return base.Init(node);
            }
            return false;
        }

        #endregion
    }
}
