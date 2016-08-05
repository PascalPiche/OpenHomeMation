using OHM.Data;
using OHM.Logger;
using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{
    public abstract class AbstractNode : INode, ICommandsNode, IPropertiesNode
    {
        #region Private Members

        private INodeProperty _keyProperty;
        private INodeProperty _nameProperty;
        private INodeProperty _nodeStateProperty;

        private ILogger _logger;
        private IDataStore _data;

        private ObservableCollection<ICommand> _commands;
        private IDictionary<string, ICommand> _commandsDic;
        private ReadOnlyObservableCollection<ICommand> _commandsReadOnly;

        private ObservableCollection<INodeProperty> _properties;
        private Dictionary<string, INodeProperty> _propertiesDic;

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Internal ctor

        internal AbstractNode(string key, string name, NodeStates initialState = NodeStates.initializing)
        {
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _commandsReadOnly = new ReadOnlyObservableCollection<ICommand>(_commands); 
            
            _properties = new ObservableCollection<INodeProperty>();
            _propertiesDic = new Dictionary<string, INodeProperty>();

            //Register Key Property
            _keyProperty = new NodeProperty("system-key", "System node key", typeof(string), true, "System node key", key);
            this.RegisterProperty(_keyProperty);

            //Register Name Property
            _nameProperty = new NodeProperty("system-name", "System node name", typeof(string), false, "System node name", name);
            this.RegisterProperty(_nameProperty);

            //Register Node State Property
            _nodeStateProperty = new NodeProperty("system-node-state", "System node state", typeof(NodeStates), false, "System node state", initialState);
            this.RegisterProperty(_nodeStateProperty);
        }

        #endregion

        #region Public Properties

        public string Key { get { return _keyProperty.Value as string; } }

        public string Name { get { return _nameProperty.Value as string; }
            protected set
            {
                UpdateProperty("system-name", value);
                NotifyPropertyChanged("Name");
            }
        }

        public NodeStates State { get { return (NodeStates)_nodeStateProperty.Value; }
            protected set
            {
                UpdateProperty("system-node-state", value);
                NotifyPropertyChanged("State");
            }
        }

        public IReadOnlyList<ICommand> Commands { get { return _commandsReadOnly; } }

        public IReadOnlyList<INodeProperty> Properties { get { return _properties; } }

        #endregion

        #region Protected Properties

        protected ILogger Logger { get { return _logger; } }

        protected IDataStore DataStore { get { return _data; } }

        #endregion

        #region Protected Functions

        protected bool RegisterProperty(INodeProperty nodeProperty)
        {
            bool result = false;
            if (!_propertiesDic.ContainsKey(nodeProperty.Key))
            {
                _propertiesDic.Add(nodeProperty.Key, nodeProperty);
                _properties.Add(nodeProperty);
                result = true;
            }
            return result;
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
       
        protected bool TryGetProperty(string key, out INodeProperty result)
        {
            return _propertiesDic.TryGetValue(key, out result);
        }

        protected bool UpdateProperty(string key, object value)
        {
            INodeProperty property;
            if (TryGetProperty(key, out property))
            {
                return property.SetValue(value);
            }
            return false;
        }

        protected bool ContainProperty(string key)
        {
            return _propertiesDic.ContainsKey(key);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual bool Initing()
        {
            RegisterCommands();
            RegisterProperties();
            return true;
        }

        protected abstract void RegisterCommands();

        protected abstract void RegisterProperties();

        #endregion

        #region Internal Methods

        internal bool Init(IDataStore data, ILogger logger)
        {
            _data = data;
            _logger = logger;
            return true;
        }

        internal bool CanExecuteCommand(string key)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].CanExecute();
            }
            return false;
        }

        internal bool ExecuteCommand(string commandKey, IDictionary<string, string> arguments)
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
