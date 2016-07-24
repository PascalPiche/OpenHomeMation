using OHM.Logger;
using OHM.Nodes;

namespace OHM.RAL
{
    public abstract class RalNodeAbstract : NodeAbstract
    {

        #region Public Ctor

        protected RalNodeAbstract(string key, string name) 
            : base(key, name)
        {}

        #endregion

        protected override NodeAbstract CreateChildNode(string model, string key, string name, System.Collections.Generic.IDictionary<string, object> options = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
