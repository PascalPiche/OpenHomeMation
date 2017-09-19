using OHM.Data;
using OHM.Logger;
using OHM.Nodes.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{

    /// <summary>
    /// Minimal implementation of the INode interface.
    /// Must be used for all node present in the system.
    /// </summary>
    public abstract class AbstractBasicNode : INode
    {
        #region Private Members

        /// <summary>
        /// For storing prefix system const
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">SystemKey to identify the node in the system</param>
        /// <param name="name">Text show for basic report and debug information</param>
        /// <param name="initialState">SystemState of the node after the constructor execution</param>
        internal AbstractBasicNode(string key, string name, SystemNodeStates initialState = SystemNodeStates.created)
        {            
            _properties = new ObservableCollection<INodeProperty>();
            _propertiesDic = new Dictionary<string, INodeProperty>();

            //Register SystemKey Property
            _keyProperty = new NodeProperty(PREFIX_SYSTEM + "key", "System node key", typeof(string), true, "System node key", key);
            this.RegisterProperty(_keyProperty);

            //Register SystemName Property
            _nameProperty = new NodeProperty(PREFIX_SYSTEM + "name", "System node name", typeof(string), false, "System node name", name);
            this.RegisterProperty(_nameProperty);

            //Register Node SystemState Property
            _nodeStateProperty = new NodeProperty(PREFIX_SYSTEM + "node-state", "System node state", typeof(SystemNodeStates), false, "System node state", initialState);
            this.RegisterProperty(_nodeStateProperty);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public string SystemKey { get { return _keyProperty.Value as string; } }

        public string SystemName { get { return _nameProperty.Value as string; } }

        public IReadOnlyList<INodeProperty> Properties { get { return _properties; } }

        public SystemNodeStates SystemState
        {
            get { return (SystemNodeStates)_nodeStateProperty.Value; }
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

            //CRITICAL ZONE
            if (!_propertiesDic.ContainsKey(nodeProperty.Key))
            {
                _propertiesDic.Add(nodeProperty.Key, nodeProperty);
                _properties.Add(nodeProperty);
                result = true;
            }
            //END CRITICAL ZONE

            if (result)
            {
                //TODO Notify property list changed
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

            if (result)
            {
                //TODO Notify property list changed
            }
            return result;
        }
        
        protected bool ContainProperty(string key)
        {
            return _propertiesDic.ContainsKey(key);
        }

        protected bool TryGetProperty(string key, out INodeProperty result)
        {
            return _propertiesDic.TryGetValue(key, out result);
        }

        protected bool UpdateProperty(string key, object value)
        {
            INodeProperty property;
            bool result = false;
            if (TryGetProperty(key, out property))
            {
                result = property.SetValue(value);
            }

            if (result)
            {
                //Notify node property changed
            }
            return result;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !String.IsNullOrWhiteSpace(propertyName))
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
