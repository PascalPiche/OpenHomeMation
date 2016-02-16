﻿using OHM.Commands;
using OHM.Data;
using OHM.Interfaces;
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
        private ObservableCollection<INode> _children;
        private Dictionary<string, INode> _childrenDic;
        private ObservableCollection<INodeProperty> _properties;
        private Dictionary<string, INodeProperty> _propertiesDic;
        private INode _parent;
        private ILogger _logger;
        private IDataStore _data;
        public event PropertyChangedEventHandler PropertyChanged;

        public NodeAbstract(string key, string name, ILogger logger)
        {
            _key = key;
            _name = name;
            _logger = logger;
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _children = new ObservableCollection<INode>();
            _childrenDic = new Dictionary<string, INode>();
            _properties = new ObservableCollection<INodeProperty>();
            _propertiesDic = new Dictionary<string, INodeProperty>();
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

        public IReadOnlyList<ICommand> Commands { get { return new ReadOnlyObservableCollection<ICommand>(_commands); } }

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

        public IReadOnlyList<INode> Children
        {
            get { return _children; }
        }

        public INode Parent
        {
            get { return _parent; }
        }

        public INode GetChild(string key)
        {
            INode result;

            if (_childrenDic.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                //Check child
                foreach (INode item in _children)
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

        public IReadOnlyList<INodeProperty> Properties
        {
            get
            {
                return _properties;
            }
        }

        #endregion

        #region "Protected"

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

        protected bool UnRegisterProperty(String key)
        {
            if (_propertiesDic.ContainsKey(Key))
            {
                var property = _propertiesDic[key];
                if (_properties.Remove(property))
                {
                    if (_propertiesDic.Remove(key))
                    {
                        return true;
                    }
                    else
                    {
                        _properties.Add(property);
                    }
                }
                
                return false;
            }
            return false;
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

        protected bool Init(IDataStore data)
        {
            _data = data;
            return true;
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

        #region internal

        void SetParent(INode node)
        {
            _parent = node;
        }

        #endregion
    }
}
