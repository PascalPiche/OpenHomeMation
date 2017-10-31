using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Nodes
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractPowerTreeNode : AbstractPowerNode, IPowerTreeNode
    {
        #region Private Members

        private ITreeNode _parent;
        private ObservableCollection<INode> _children;
        private IDictionary<string, INode> _childrenDic;

        #endregion

        #region Internal Ctor

        internal AbstractPowerTreeNode(string key, string name)
            : base(key, name)
        {
            _children = new ObservableCollection<INode>();
            _childrenDic = new Dictionary<string, INode>();
        }

        #endregion

        #region Public Properties

        public string TreeKey
        {
            get
            {
                string result = null;
                if (SystemState != SystemNodeStates.created)
                {
                    if (Parent != null)
                    {
                        result = Parent.TreeKey + "." + SystemKey;
                    }
                    else
                    {
                        result = SystemKey;
                    }
                }
                return result;
            }
        }

        public IReadOnlyList<INode> Children { get { return _children; } }

        #endregion

        #region Protected Properties

        protected ITreeNode Parent { get { return _parent; } }

        #endregion

        #region Protected Methods

        protected virtual bool InitSubChild()
        {
            return true;
        }

        protected bool RemoveChild(string key)
        {
            return RemoveChild(FindDirectChild(key));
        }

        protected INode FindDirectChild(string key)
        {
            INode result;
            _childrenDic.TryGetValue(key, out result);
            return result;
        }

        #endregion

        #region Internal methods

        internal protected override bool Initing()
        {   
            return base.Initing() && InitSubChild();
        }

        internal void SetParent(ITreeNode node)
        {
            _parent = node;
            this.SystemState = SystemNodeStates.operational;
        }

        internal bool AddChild(AbstractPowerTreeNode node)
        {
            if (!_childrenDic.ContainsKey(node.SystemKey))
            {
                _childrenDic.Add(node.SystemKey, node);
                _children.Add(node);
                node.SetParent(this);
                return true;
            }
            return false;
        }

        internal bool CanExecuteCommand(string nodeFullKey, string commandKey)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(nodeFullKey) && !string.IsNullOrWhiteSpace(commandKey))
            {
                if (this.SystemKey == nodeFullKey)
                {
                    result = base.CanExecuteCommand(commandKey);
                }
                else if (nodeFullKey.Contains(".") && this._children.Count != 0)
                {
                    //Lookup ALL LEVEL the node list
                    nodeFullKey = nodeFullKey.Substring(nodeFullKey.IndexOf('.') + 1);

                    AbstractPowerTreeNode node = GetDirectChildNode(nodeFullKey) as AbstractPowerTreeNode;
                    if (node != null)
                    {
                        result = node.CanExecuteCommand(nodeFullKey, commandKey);
                    }
                }
            }

            return result;
        }

        internal bool ExecuteCommand(string nodeFullKey, string commandKey, IDictionary<string, string> arguments)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(nodeFullKey) && !string.IsNullOrWhiteSpace(commandKey))
            {
                if (this.SystemKey == nodeFullKey)
                {
                    result = base.ExecuteCommand(commandKey, arguments);
                }
                else if (nodeFullKey.Contains(".") && this._children.Count != 0)
                {
                    //Remove Extra checked key
                    nodeFullKey = nodeFullKey.Substring(nodeFullKey.IndexOf('.') + 1);

                    INode node = GetDirectChildNode(nodeFullKey);
                    if (node != null && node is AbstractPowerTreeNode)
                    {
                        result = ((AbstractPowerTreeNode)node).ExecuteCommand(nodeFullKey, commandKey, arguments);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Private methods

        private INode GetDirectChildNode(string nodeFullKey)
        {
            string nextNode = nodeFullKey;

            if (nextNode.Contains("."))
            {
                nextNode = nextNode.Split('.')[0];
            }

            return this.FindDirectChild(nextNode);
        }

        private bool RemoveChild(INode node)
        {
            if (node != null && _childrenDic.ContainsKey(node.SystemKey))
            {
                _children.Remove(node);
                _childrenDic.Remove(node.SystemKey);
                return true;
            }
            return false;
        }

        #endregion
    }
}