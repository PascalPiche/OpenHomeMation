using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes.Properties
{
    /// <summary>
    /// Core node property class
    /// Implements: INodeProperty
    /// Implements: INotifyPropertyChanged
    /// </summary>
    public class NodeProperty : INodeProperty, INotifyPropertyChanged
    {
        #region Private Members

        private string _key;
        private string _name;
        private string _description;
        private object _value;
        private bool _readOnly;
        private Type _type;
        private ObservableCollection<INodeProperty> _extraInfo = new ObservableCollection<INodeProperty>();
        private Dictionary<String, INodeProperty> _extraInfoDict;

        #endregion

        #region Public Ctor

        /// <summary>
        /// Minimal Constructor
        /// </summary>
        /// <param name="key">Local Unique key for the property</param>
        /// <param name="name">Name of the property</param>
        /// <param name="type">Type of the property</param>
        /// <param name="readOnly">True if the property is read-only</param>
        /// <param name="description">Short description of the property</param>
        public NodeProperty(string key, string name, Type type, bool readOnly, string description)
            : this(key, name, type, readOnly, description, null) {}

        /// <summary>
        /// Constructor with initial value
        /// </summary>
        /// <param name="key">Local Unique key for the property</param>
        /// <param name="name">Name of the property</param>
        /// <param name="type">Type of the property</param>
        /// <param name="readOnly">True if the property is read-only</param>
        /// <param name="description">Short description of the property</param>
        /// <param name="value">Initial value set in the property</param>
        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value) 
            : this(key, name, type, readOnly, description, value, new ObservableCollection<INodeProperty>()) {}

        /// <summary>
        /// Complete constructor
        /// </summary>
        /// <param name="key">Local Unique key for the property</param>
        /// <param name="name">Name of the property</param>
        /// <param name="type">Type of the property</param>
        /// <param name="readOnly">True if the property is read-only</param>
        /// <param name="description">Short description of the property</param>
        /// <param name="value">Initial value set in the property</param>
        /// <param name="extraInfo">Observable collection of other INodeProperty</param>
        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value, ObservableCollection<INodeProperty> extraInfo)
        {
            _key = key;
            _name = name;
            _type = type;
            _readOnly = readOnly;
            _description = description;
            InitializeExtraInfo(extraInfo);

            if (!SetValue(value))
            {
                //TODO THROW EXCEPTION
            }
        }

        #endregion

        #region Public Properties

        #region INodeProperty Implementation

        /// <summary>
        /// Unique Key of the property
        /// Implements: INodeProperty.Key
        /// </summary>
        public string Key { get { return _key; } }

        /// <summary>
        /// Name of the property
        /// Implements: INodeProperty.Name
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Type of the property value
        /// Implements: INodeProperty.Type
        /// </summary>
        public Type Type { get { return _type; } }

        /// <summary>
        /// Short Description of the property
        /// Implements: INodeProperty.Description
        /// </summary>
        /// <returns>String or String.empty if not available</returns>
        public string Description { get { return _description; } }

        /// <summary>
        /// Readonly flag of the property
        /// Implements: INodeProperty.ReadOnly
        /// </summary>
        /// <return>True if the property is read-only otherwise false</return>
        public bool ReadOnly { get { return _readOnly; } }

        /// <summary>
        /// Value object actual in the property
        /// Implements: INodeProperty.Value
        /// </summary>
        public object Value { get { return _value; } }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the property value with the argument passed
        /// Implements: INodeProperty.SetValue
        /// </summary>
        /// <param name="val">Value to set in the property value</param>
        /// <returns>True if succeed otherwise false</returns>
        public bool SetValue(object val)
        {
            bool result = false;
            if (val == null || val.GetType() == _type)
            {
                _value = val;
                NotifyPropertyChanged("Value");
                result = true;
            }
            return result;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Property changed event handler declaration
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Functions

        /// <summary>
        /// Raise a property changed event
        /// </summary>
        /// <param name="propertyName">The property name passed to the property changed event</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Private functions

        private void InitializeExtraInfo(ObservableCollection<INodeProperty> extraInfo)
        {
            _extraInfo = extraInfo;
            _extraInfoDict = new Dictionary<string, INodeProperty>();
            foreach (INodeProperty nodeProp in _extraInfo)
            {
                _extraInfoDict.Add(nodeProp.Key, nodeProp);
            }
        }

        #endregion
    }
}
