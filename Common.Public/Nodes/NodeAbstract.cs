using OHM.Commands;
using OHM.Data;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{
    public abstract class NodeAbstract : INode
    {
        private string _key;
        private string _name;
        private ObservableCollection<ICommand> _commands;
        protected Dictionary<string, ICommand> _commandsDic;

        private ObservableCollection<NodeAbstract> _children;
        private Dictionary<string, NodeAbstract> _childrenDic;

        private ObservableCollection<INodeProperty> _properties;
        private Dictionary<string, INodeProperty> _propertiesDic;

        private INode _parent;
        private ILogger _logger;
        private IDataStore _data;
        private NodeStates _state;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Protected ctor

        protected NodeAbstract(string key, string name, ILogger logger, NodeStates initialState = NodeStates.initializing)
        {
            _key = key;
            _name = name;
            _logger = logger;
            _state = initialState;
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _children = new ObservableCollection<NodeAbstract>();
            _childrenDic = new Dictionary<string, NodeAbstract>();
            _properties = new ObservableCollection<INodeProperty>();
            _propertiesDic = new Dictionary<string, INodeProperty>();
        }

        #endregion

        #region Public Properties

        public string FullKey { get {
                if (Parent != null)
                {
                    return Parent.FullKey + "." + Key;
                }
                else
                {
                    return Key;
                }
            } 
        }

        public string Key { get { return _key; } }

        public IReadOnlyList<ICommand> Commands { get { return new ReadOnlyObservableCollection<ICommand>(_commands); } }

        public string Name
        {
            get { return _name; }
            protected set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public NodeStates State
        {
            get
            {
                return _state;
            }
            protected set
            {
                _state = value;
                NotifyPropertyChanged("State");
            }
        }

        public IReadOnlyList<INode> Children { get { return _children; } }

        public INode Parent { get { return _parent; } }

        public IReadOnlyList<INodeProperty> Properties { get { return _properties; } }

        #endregion

        #region Public API

        public bool CanExecuteCommand(string key)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].CanExecute();
            }
            return false;
        }

        public bool CanExecuteCommand(string nodeFullKey, string commandKey)
        {
            if (this.Key == nodeFullKey)
            {
                return this.CanExecuteCommand(commandKey);
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
                NodeAbstract node = this.GetChild(nextNode);
                if (node != null)
                {
                    return node.CanExecuteCommand(nodeFullKey, commandKey);
                }
            }

            return false;
        }

        public bool ExecuteCommand(string key, Dictionary<string, string> arguments)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].Execute(arguments);
            }
            else if (_children.Count != 0)
            {
                foreach(INode node in _children) {
                    if (node.ExecuteCommand(key, arguments))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ExecuteCommand(string nodeFullKey, string commandKey, Dictionary<string, string> arguments)
        {
            if (this.Key == nodeFullKey)
            {
                return this.ExecuteCommand(commandKey, arguments);
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
                INode node = this.GetChild(nextNode);
                if (node != null)
                {
                    return node.ExecuteCommand(commandKey, arguments);
                }
            }
            return false;
        }

        
        #endregion

        #region Protected Properties

        protected ILogger Logger { get { return _logger; } }

        protected IDataStore DataStore { get { return _data; } }

        #endregion

        #region Protected Functions

        protected NodeAbstract GetChild(string key)
        {
            NodeAbstract result;

            if (_childrenDic.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                //Check child
                foreach (NodeAbstract item in _children)
                {
                    result = item.GetChild(key);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        protected bool AddChild(NodeAbstract node)
        {
            if (GetChild(node.Key) == null)
            {
                _childrenDic.Add(node.Key, node);
                _children.Add(node);
                node.SetParent(this);
                return true;
            }
            return false;
        }

        protected bool RemoveChild(INode node)
        {
            var it = GetChild(node.Key);
            if (it != null)
            {
                _children.Remove(it);
                _childrenDic.Remove(node.Key);
                return true;
            }
            return false;
        }

        protected bool RemoveChild(string key)
        {
            var it = GetChild(key);
            if (it != null)
            {
                _children.Remove(it);
                _childrenDic.Remove(Key);
                return true;
            }
            return false;
        }

        protected bool RegisterProperty(INodeProperty nodeProperty)
        {
            if (_propertiesDic.ContainsKey(nodeProperty.Key))
            {
                return false;
            }
            _propertiesDic.Add(nodeProperty.Key, nodeProperty);
            _properties.Add(nodeProperty);
            return true;
        }

        protected bool UnRegisterProperty(string key)
        {
            bool result = false;
            if (_propertiesDic.ContainsKey(Key))
            {
                var property = _propertiesDic[key];
                if (_properties.Remove(property))
                {
                    if (_propertiesDic.Remove(key))
                    {
                        result = true;
                    }
                    else
                    {
                        //Undo first remove to maintain coherence in the system
                        _properties.Add(property);
                    }
                }
                return result;
            }

            return result;
        }
       
        protected bool RegisterCommand(ICommand command)
        {
            if (_commandsDic.ContainsKey(command.Definition.Key))
            {
                return false;
            }

            _commandsDic.Add(command.Definition.Key, command);
            _commands.Add(command);
            return true;
        }

        protected bool UnRegisterCommand(ICommand command)
        {
            return false;
        }
        
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool Init(IDataStore data)
        {
            _data = data;
            return true;
        }

        protected bool UpdateProperty(string key, double value)
        {
            INodeProperty property;
            if (_propertiesDic.TryGetValue(key, out property))
            {
                return property.SetValue(value);
            }
            return false;
        }

        protected bool UpdateProperty(string key, Int64 value)
        {
            INodeProperty property;
            if (_propertiesDic.TryGetValue(key, out property))
            {
                return property.SetValue(value);
            }
            return false;
        }

        protected bool UpdateProperty(string key, object value)
        {
            INodeProperty property;
            if (_propertiesDic.TryGetValue(key, out property))
            {
                return property.SetValue(value);
            }
            return false;
        }

        protected bool ContainsProperty(string key)
        {
            return _propertiesDic.ContainsKey(key);
        }

        #endregion

        #region Internal Functions

        void SetParent(INode node)
        {
            _parent = node;
        }

        #endregion
    }
}
