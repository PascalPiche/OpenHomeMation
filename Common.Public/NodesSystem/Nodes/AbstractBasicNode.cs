using OHM.Data;
using OHM.Logger;
using OHM.Nodes.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{

    
    public abstract class AbstractBasicNode : IBasicNode
    {
        #region Private Members

        internal const string PREFIX_SYSTEM = "system-";

        private ILogger _logger;
        private IDataStore _data;

        private INodeProperty _keyProperty;
        private INodeProperty _nameProperty;

        private INodeProperty _nodeStateProperty;

        private ObservableCollection<INodeProperty> _properties;
        private IDictionary<string, INodeProperty> _propertiesDic;

        #endregion

        #region Internal Ctor

        internal AbstractBasicNode(string key, string name, NodeStates initialState = NodeStates.created)
        {            
            _properties = new ObservableCollection<INodeProperty>();
            _propertiesDic = new Dictionary<string, INodeProperty>();

            //Register Key Property
            _keyProperty = new NodeProperty(PREFIX_SYSTEM + "key", "System node key", typeof(string), true, "System node key", key);
            this.RegisterProperty(_keyProperty);

            //Register Name Property
            _nameProperty = new NodeProperty(PREFIX_SYSTEM + "name", "System node name", typeof(string), false, "System node name", name);
            this.RegisterProperty(_nameProperty);

            //Register Node State Property
            _nodeStateProperty = new NodeProperty(PREFIX_SYSTEM + "node-state", "System node state", typeof(NodeStates), false, "System node state", initialState);
            this.RegisterProperty(_nodeStateProperty);
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public string Key { get { return _keyProperty.Value as string; } }

        public string Name { get { return _nameProperty.Value as string; } }

        public IReadOnlyList<INodeProperty> Properties { get { return _properties; } }

        public NodeStates State
        {
            get { return (NodeStates)_nodeStateProperty.Value; }
            protected set
            {
                UpdateProperty(PREFIX_SYSTEM + "node-state", value);
                NotifyPropertyChanged("State");
            }
        }

        #endregion

        #region Protected Properties

        protected ILogger Logger { get { return _logger; } }

        protected IDataStore DataStore { get { return _data; } }

        #endregion

        #region Protected Methods

        protected abstract bool RegisterProperties();

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

            
            if (key != null && !key.StartsWith(PREFIX_SYSTEM)) 
            {
                //CRITICAL ZONE
                if (_propertiesDic.ContainsKey(key) && _properties.Remove(_propertiesDic[key]))
                {
                    _propertiesDic.Remove(key);
                    result = true;
                }
                //END CRITICAL ZONE
            }
            return result;
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

        #endregion

        #region Internal Methods

        internal bool Init(IDataStore data, ILogger logger)
        {
            bool result = false;
            if (data != null && logger != null)
            {
                _data = data;
                _logger = logger;
                result = RegisterProperties();
            }
            return result;
        }

        #endregion
    }
}
