
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
            :base(definition) { }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string NodeTreeKey { get { return ((ICommandsTreeNode)Node).TreeKey; } }

        #endregion

        #region Internal Methods

        internal override bool Init(ICommandsNode node)
        {
            if (node is ICommandsTreeNode)
            {
                return base.Init(node);
            }
            return false;
        }

        #endregion
    }
}
