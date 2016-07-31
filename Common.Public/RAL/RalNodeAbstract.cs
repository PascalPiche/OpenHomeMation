using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class RalNodeAbstract : NodeAbstract
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

        protected NodeAbstract CreateChildNode(string model, string key, string name, IDictionary<string, object> options = null)
        {
            NodeAbstract result = null;
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
