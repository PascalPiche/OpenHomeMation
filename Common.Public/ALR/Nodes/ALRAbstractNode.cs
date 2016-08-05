using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class ALRAbstractNode : AbstractTreeNode
    {
        #region Private members

        private ALRInterfaceAbstractNode _interface;

        #endregion

        #region Protected Ctor

        protected ALRAbstractNode(string key, string name) 
            : base(key, name)
        {}

        #endregion

        #region Protected Methods

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

        #endregion

        #region Internal Methods

        internal protected ALRInterfaceAbstractNode Interface { get { return _interface; } }

        internal bool Init(IDataStore data, ILogger logger, ALRInterfaceAbstractNode inter)
        {
            _interface = inter;
            return this.Init(data, logger);
        }

        #endregion
    }
}
