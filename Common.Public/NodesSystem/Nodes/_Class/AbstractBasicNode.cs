using log4net;
using OHM.Data;
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
        /// For storing prefix system constant
        /// </summary>
        internal const string PREFIX_SYSTEM = "system-";

        private ILog _logger;
        private IDataStore _data;

        private INodeProperty _keyProperty;
        private INodeProperty _nameProperty;
        private INodeProperty _nodeStateProperty;

        private ObservableCollection<INodeProperty> _properties;
        private IDictionary<string, INodeProperty> _propertiesDic;

        #endregion

        #region Internal Ctor

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="key">SystemKey to identify the node in the system</param>
        /// <param name="name">Text show for basic report and debug information</param>
        internal AbstractBasicNode(string key, string name)
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
            _nodeStateProperty = new NodeProperty(PREFIX_SYSTEM + "node-state", "System node state", typeof(SystemNodeStates), false, "System node state", SystemNodeStates.creating);
            this.RegisterProperty(_nodeStateProperty);

            //Set the node as created
            this.UpdateProperty(_nodeStateProperty.Key, SystemNodeStates.created);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// System key of the node
        /// </summary>
        public string SystemKey { get { return _keyProperty.Value as string; } }

        /// <summary>
        /// System name of the node
        /// </summary>
        public string SystemName { get { return _nameProperty.Value as string; } }

        /// <summary>
        /// Properties list of the node
        /// </summary>
        public IReadOnlyList<INodeProperty> Properties { get { return _properties; } }

        /// <summary>
        /// Get the actual state of the node
        /// </summary>
        public SystemNodeStates SystemState
        {
            get { return (SystemNodeStates)_nodeStateProperty.Value; }
            protected set
            {
                if (UpdateProperty(PREFIX_SYSTEM + "node-state", value))
                {
                    NotifyPropertyChanged("State");
                }
                else
                {
                    //TODO: MANAGE THE CASE
                    //Throw exception?
                }
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Get the logger
        /// </summary>
        protected ILog Logger { get { return _logger; } }

        /// <summary>
        /// Get the data store
        /// </summary>
        protected IDataStore DataStore { get { return _data; } }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Properties to register when the node is created
        /// </summary>
        /// <returns></returns>
        protected abstract bool RegisterProperties();

        /// <summary>
        /// Register a property
        /// </summary>
        /// <param name="nodeProperty"></param>
        /// <returns></returns>
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
                NotifyPropertyChanged("Properties");
            }
            return result;
        }

        /// <summary>
        /// Remove a property from the node
        /// </summary>
        /// <param name="key"></param>
        /// <returns>True when the property was succesfuly removed from the node</returns>
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
                NotifyPropertyChanged("Properties");
            }
            return result;
        }
        
        /// <summary>
        /// Allow to check if a property exist on the node with a property key string
        /// </summary>
        /// <param name="key">The property key to lookup</param>
        /// <returns>True if the key exist in the property list of the node</returns>
        protected bool ContainProperty(string key)
        {
            return _propertiesDic.ContainsKey(key);
        }

        /// <summary>
        /// Try get property without error if not found
        /// </summary>
        /// <param name="key">The property key to get</param>
        /// <param name="result">The property if found</param>
        /// <returns>True if the property was found</returns>
        protected bool TryGetProperty(string key, out INodeProperty result)
        {
            return _propertiesDic.TryGetValue(key, out result);
        }

        /// <summary>
        /// Update the value of a property
        /// </summary>
        /// <param name="key">The property key to update</param>
        /// <param name="value">The new value to set</param>
        /// <returns>True if the change is accepted</returns>
        protected bool UpdateProperty(string key, object value)
        {
            INodeProperty property;
            bool result = false;
            if (TryGetProperty(key, out property))
            {
                //TODO: Custom check for system-key and system-state PROPERTY NEEDED
                //TODO IMPORTANT FOR SECURITY AND INTEGRITY
                result = property.SetValue(value);
            }
            if (result)
            {
                NotifyPropertyChanged("Properties");
            }
            return result;
        }

        /// <summary>
        /// Raise a property changed event for the property name passed
        /// </summary>
        /// <param name="propertyName">The property name with the changed value</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !String.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Internal Methods

        internal bool Init(IDataStore data, ILog logger)
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
