using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class RalNodeAbstract : TreeNodeAbstract
    {

        private RalInterfaceNodeAbstract _interface;

        #region Public Ctor

        protected RalNodeAbstract(string key, string name) 
            : base(key, name)
        {}

        #endregion

        internal protected RalInterfaceNodeAbstract Interface { get { return _interface; } }

        internal bool Init(IDataStore data, ILogger logger, RalInterfaceNodeAbstract inter)
        {
            _interface = inter;
            return this.Init(data, logger);
        }

        protected TreeNodeAbstract CreateChildNode(string model, string key, string name, IDictionary<string, object> options = null)
        {
            TreeNodeAbstract result = null;
            RalNodeAbstract newNode = Interface.CreateNodeInstance(model, key, name, options) as RalNodeAbstract;
            if (newNode != null)
            {
                newNode.Init(DataStore, Logger, Interface);
                if (this.AddChild(newNode))
                {
                    newNode.Initing();
                    result = newNode;
                }
            }
            return result;
        }
    
    }
}
