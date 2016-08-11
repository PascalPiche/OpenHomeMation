using OHM.Data;
using OHM.Logger;
using System.Collections.Generic;

namespace OHM.Nodes.ALR
{
    public abstract class ALRAbstractTreeNode : AbstractPowerTreeNode
    {
        #region Private members

        private ALRInterfaceAbstractNode _interface;

        #endregion

        #region Protected Ctor

        protected ALRAbstractTreeNode(string key, string name) 
            : base(key, name)
        {}

        #endregion

        #region Protected Methods

        protected AbstractPowerTreeNode CreateChildNode(string model, string key, string name, IDictionary<string, object> options = null)
        {
            AbstractPowerTreeNode result = null;
            ALRAbstractTreeNode newNode = Interface.CreateNodeInstance(model, key, name, options) as ALRAbstractTreeNode;
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
