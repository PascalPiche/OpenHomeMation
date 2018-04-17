using log4net;
using OHM.Data;
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

            if (_interface != null)
            {
                ALRAbstractTreeNode newNode = _interface.CreateNodeInstance(model, key, name, options) as ALRAbstractTreeNode;
                if (newNode != null)
                {
                    newNode.Init(DataStore, Logger, Interface);
                    if (this.AddChild(newNode))
                    {
                        newNode.Initing();
                        result = newNode;
                    }
                    else
                    {
                        //LOG: CANT ADD NEW NODE TO THE TREE
                        this.Logger.Error("Can't add new node to the tree");
                    }
                }
            }
            else
            {
                //LOG
                if (this.Logger != null)
                {
                    this.Logger.Error("Trying to create a child node with no interface bound to the creator node: " + this.ToString() + " with model: " + model);
                }
            }
           
            return result;
        }

        #endregion

        #region Internal Methods

        internal protected ALRInterfaceAbstractNode Interface { get { return _interface; } }

        internal bool Init(IDataStore data, ILog logger, ALRInterfaceAbstractNode inter)
        {
            bool result = false;
            if (inter != null)
            {
                _interface = inter;
                result = base.Init(data, logger);
            }
            return result;
        }

        #endregion
    }
}
