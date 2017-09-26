
using System.Collections.Generic;
namespace OHM.Nodes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractTreeCommand : AbstractCommand, ITreeCommand
    {
        #region Protected Ctor

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
