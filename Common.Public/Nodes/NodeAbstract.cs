using OHM.Commands;
using OHM.Data;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace OHM.Nodes
{
    public abstract class NodeAbstract : INode
    {
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
        private Dispatcher _dispatcher;
        private IDataStore _data;
        public event PropertyChangedEventHandler PropertyChanged;

        public NodeAbstract(string key, string name, INode parent)
        {
            _key = key;
            _name = name;
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _children = new ObservableCollection<NodeAbstract>();
            _childrenDic = new Dictionary<string, NodeAbstract>();
            _properties = new ObservableCollection<INodeProperty>();
            _propertiesDic = new Dictionary<string, INodeProperty>();
            _parent = parent;
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        #region "Public"

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
            protected set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public IList<ICommand> Commands { get { return new ReadOnlyObservableCollection<ICommand>(_commands); } }

        public bool CanExecuteCommand(string key)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].CanExecute();
            }
            return false;
        }

        public bool ExecuteCommand(string key, Dictionary<string, object> arguments)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].Execute(arguments);
            }
            return false;
        }

        public IList<INode> Children
        {
            get { return _children; }
        }

        public INode Parent
        {
            get { return _parent; }
        }


        public INode GetChild(string key)
        {
            NodeAbstract result;

            if (_childrenDic.TryGetValue(key, out result))
            {
                return result;
            }
            return null;
        }

        #endregion

        #region "Protected"

        protected bool AddChild(NodeAbstract node)
        {
            if (GetChild(node.Key) == null)
            {
                _childrenDic.Add(node.Key, node);
                _dispatcher.Invoke((Action)(() =>
                {
                    _children.Add(node);
                }));

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

        protected bool RegisterProperty(NodeProperty nodeProperty)
        {
            if (_propertiesDic.ContainsKey(nodeProperty.Key))
            {
                return false;
            }
            _propertiesDic.Add(nodeProperty.Key, nodeProperty);
            _properties.Add(nodeProperty);
            return true;
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

        protected ILogger Logger { get { return _logger; } }

        protected IDataStore DataStore { get { return _data; } }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool Init(ILogger logger, IDataStore data)
        {
            _logger = logger;
            _data = data;
            return true;
        }

        #endregion
    }
}
