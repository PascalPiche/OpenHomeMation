using OHM.Nodes.ALR;

namespace WUnderground.Nodes
{
    public abstract class WUndergroundNodeAbstract : ALRAbstractTreeNode
    {
        #region Public Ctor

        public WUndergroundNodeAbstract(string keyId, string name)
            : base(keyId, name)
        { }

        #endregion
    }
}
