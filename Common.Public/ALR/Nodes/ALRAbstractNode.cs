using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class ALRAbstractNode : AbstractTreeNode
    {

        private ALRInterfaceAbstractNode _interface;

        #region Public Ctor

        protected ALRAbstractNode(string key, string name) 
            : base(key, name)
        {}

        #endregion

        internal protected ALRInterfaceAbstractNode Interface { get { return _interface; } }

        internal bool Init(IDataStore data, ILogger logger, ALRInterfaceAbstractNode inter)
        {
            _interface = inter;
            return this.Init(data, logger);
        }

        protected AbstractTreeNode CreateChildNode(string model, string key, string name, IDictionary<string, object> options = null)
        {
            AbstractTreeNode result = null;
            ALRAbstractNode newNode = Interface.CreateNodeInstance(model, key, name, options) as ALRAbstractNode;
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
