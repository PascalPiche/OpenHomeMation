using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Nodes
{
    public abstract class TreeNodeAbstract : NodeAbstract, ITreeNode
    {
        private TreeNodeAbstract _parent;

        private ObservableCollection<TreeNodeAbstract> _children;
        private Dictionary<string, TreeNodeAbstract> _childrenDic;

        internal TreeNodeAbstract(string key, string name, NodeStates initialState = NodeStates.initializing)
            : base(key, name, initialState)
        {
            _children = new ObservableCollection<TreeNodeAbstract>();
            _childrenDic = new Dictionary<string, TreeNodeAbstract>();
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

        protected TreeNodeAbstract Parent { get { return _parent; } }

        protected bool RemoveChild(TreeNodeAbstract node)
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

        protected TreeNodeAbstract FindChild(string key)
        {
            TreeNodeAbstract result;

            if (_childrenDic.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                //Check child
                foreach (TreeNodeAbstract item in Children)
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
                TreeNodeAbstract node = this.FindChild(nextNode);
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

                TreeNodeAbstract node = this.FindChild(nextNode);
                if (node != null)
                {
                    result = node.ExecuteCommand(nodeFullKey, commandKey, arguments);
                }
            }
            return result;
        }

        internal void SetParent(TreeNodeAbstract node)
        {
            _parent = node;
            this.State = NodeStates.normal;
        }

        internal bool AddChild(TreeNodeAbstract node)
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
