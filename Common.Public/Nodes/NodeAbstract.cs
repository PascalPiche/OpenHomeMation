using OHM.Data;
using OHM.Logger;
using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{
    public abstract class NodeAbstract : INode
    {
        #region Private Members

        private string _key;
        private string _name;
        private ObservableCollection<ICommand> _commands;
        private Dictionary<string, ICommand> _commandsDic;

        private ObservableCollection<NodeAbstract> _children;
        private Dictionary<string, NodeAbstract> _childrenDic;

        private ObservableCollection<INodeProperty> _properties;
        private Dictionary<string, INodeProperty> _propertiesDic;

        private INode _parent;
        private ILogger _logger;
        private IDataStore _data;
        private NodeStates _state;

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Internal ctor

        internal NodeAbstract(string key, string name, NodeStates initialState = NodeStates.initializing)
        {
            _key = key;
            _name = name;
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

        public string FullKey { 
            get {
                string result = null;
                if (_state != NodeStates.initializing)
                {
                    if (Parent != null)
                    {
                        result = Parent.FullKey + "." + Key;
                    }
                    else
                    {
                        result = Key;
                    }
                }
                return result;
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

        #region Protected Properties

        protected ILogger Logger { get { return _logger; } }

        protected IDataStore DataStore { get { return _data; } }

        #endregion

        #region Protected Functions

        protected NodeAbstract FindChild(string key)
        {
            NodeAbstract result;

            if (_childrenDic.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                //Check child
                foreach (NodeAbstract item in Children)
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

        protected bool RemoveChild(NodeAbstract node)
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
            if (_propertiesDic.ContainsKey(key))
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

        protected bool RegisterCommand(CommandAbstract command)
        {
            bool result = false;
            if (!_commandsDic.ContainsKey(command.Key))
            {
                if (command.Init(this))
                {
                    _commandsDic.Add(command.Key, command);
                    _commands.Add(command);
                    result = true;
                }
            }

            return result;
        }

        protected bool UnRegisterCommand(ICommand command)
        {
            bool result = false;
            if (_commandsDic.ContainsKey(command.Key)) {
                _commandsDic.Remove(command.Key);
                _commands.Remove(command);
                result = true;
            }
            return result; ;
        }
        
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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

        protected bool ContainProperty(string key)
        {
            return _propertiesDic.ContainsKey(key);
        }

        protected bool CanExecuteCommand(string nodeFullKey, string commandKey)
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
                NodeAbstract node = this.FindChild(nextNode);
                if (node != null)
                {
                    return node.CanExecuteCommand(nodeFullKey, commandKey);
                }
            }

            return false;
        }

        protected bool ExecuteCommand(string nodeFullKey, string commandKey, Dictionary<string, string> arguments)
        {
            bool result = false;
            if (this.Key == nodeFullKey)
            {
                result = this.ExecuteCommand(commandKey, arguments);
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

                NodeAbstract node = this.FindChild(nextNode);
                if (node != null)
                {
                    result = node.ExecuteCommand(nodeFullKey, commandKey, arguments);
                }
            }
            return result;
        }

        #endregion

        #region Internal Functions

        internal void SetParent(INode node)
        {
            _parent = node;
        }

        internal bool AddChild(NodeAbstract node)
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

        protected bool Init(IDataStore data, ILogger logger)
        {
            _data = data;
            _logger = logger;
            this.State = NodeStates.normal;

            return Initing();
        }

        protected virtual bool Initing()
        {
            return true;
        }

        #endregion

        #region Private Functions

        private bool CanExecuteCommand(string key)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].CanExecute();
            }
            return false;
        }

        private bool ExecuteCommand(string commandKey, IDictionary<string, string> arguments)
        {
            bool result = false;
            if (_commandsDic.ContainsKey(commandKey))
            {
                result = _commandsDic[commandKey].Execute(arguments);
            }
            return result;
        }

        #endregion

    }
}
