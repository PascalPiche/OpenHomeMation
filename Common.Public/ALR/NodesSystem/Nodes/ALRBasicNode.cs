
namespace OHM.Nodes.ALR
{
    public class ALRBasicNode : ALRAbstractTreeNode
    {
        #region Public Ctor

        public ALRBasicNode(string key, string name) : base(key, name) {}

        #endregion

        #region Protected Methods

        protected override void RegisterCommands() {}

        protected override bool RegisterProperties()
        {
            return true;
        }

        #endregion
    }
}
