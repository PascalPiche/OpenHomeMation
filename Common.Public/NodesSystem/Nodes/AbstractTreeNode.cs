using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Nodes
{
    public abstract class AbstractTreeNode : AbstractNode, ITreeNode
    {
        private AbstractTreeNode _parent;

        private ObservableCollection<AbstractTreeNode> _children;
        private Dictionary<string, AbstractTreeNode> _childrenDic;

        internal AbstractTreeNode(string key, string name, NodeStates initialState = NodeStates.initializing)
            : base(key, name, initialState)
        {
            _children = new ObservableCollection<AbstractTreeNode>();
            _childrenDic = new Dictionary<string, AbstractTreeNode>();
        }

        public string TreeKey
        {
            get
            {
                string result = null;
                if (State != NodeStates.initializing)
                {
                    if (Parent != null)
                    {
                        result = Parent.TreeKey + "." + Key;
                    }
                    else
                    {
                        result = Key;
                    }
                }
                return result;
            }
        }

        public IReadOnlyList<ITreeNode> Children { get { return _children; } }

        protected AbstractTreeNode Parent { get { return _parent; } }

        protected bool RemoveChild(AbstractTreeNode node)
        {
            if (node != null)
            {
                _children.Remove(node);
                _childrenDic.Remove(node.Key);
                return true;
            }
            return false;
        }

        protected bool RemoveChild(string key)
        {
            return RemoveChild(FindChild(key));
        }

        protected AbstractTreeNode FindChild(string key)
        {
            AbstractTreeNode result;

            if (_childrenDic.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                //Check child
                foreach (AbstractTreeNode item in Children)
                {
                    result = item.FindChild(key);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        protected new bool CanExecuteCommand(string nodeFullKey, string commandKey)
        {
            if (this.Key == nodeFullKey)
            {
                return base.CanExecuteCommand(commandKey);
            }
            else
            {
                //Remove Extra checked key
                if (nodeFullKey.Contains("."))
                {
                    nodeFullKey = nodeFullKey.Substring(nodeFullKey.IndexOf('.') + 1);
                }
                string nextNode = nodeFullKey;
                if (nextNode.Contains("."))
                {
                    nextNode = nextNode.Split('.')[0];
                }

                //Lookup ALL LEVEL the node list
                AbstractTreeNode node = this.FindChild(nextNode);
                if (node != null)
                {
                    return node.CanExecuteCommand(nodeFullKey, commandKey);
                }
            }

            return false;
        }

        protected new bool ExecuteCommand(string nodeFullKey, string commandKey, Dictionary<string, string> arguments)
        {
            bool result = false;
            if (this.Key == nodeFullKey)
            {
                result = base.ExecuteCommand(commandKey, arguments);
            }
            else
            {
                //Remove Extra checked key
                if (nodeFullKey.Contains("."))
                {
                    nodeFullKey = nodeFullKey.Substring(nodeFullKey.IndexOf('.') + 1);
                }

                string nextNode = nodeFullKey;
                if (nextNode.Contains("."))
                {
                    nextNode = nextNode.Split('.')[0];
                }

                AbstractTreeNode node = this.FindChild(nextNode);
                if (node != null)
                {
                    result = node.ExecuteCommand(nodeFullKey, commandKey, arguments);
                }
            }
            return result;
        }

        internal void SetParent(AbstractTreeNode node)
        {
            _parent = node;
            this.State = NodeStates.normal;
        }

        internal bool AddChild(AbstractTreeNode node)
        {
            if (!_childrenDic.ContainsKey(node.Key))
            {
                _childrenDic.Add(node.Key, node);
                _children.Add(node);
                node.SetParent(this);
                return true;
            }
            return false;
        }
    }
}
