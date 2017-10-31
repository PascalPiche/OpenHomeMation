using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes.Properties
{
    /// <summary>
    /// Core node property class
    /// </summary>
    /// <seealso cref="OHM.Nodes.Properties.INodeProperty"/>
    /// <remarks>
    /// Implements: INodeProperty
    /// Tested by :
    ///     - NodePropertyUnitTest
    /// </remarks>
    public class NodeProperty : INodeProperty
    {
        #region Private Members

        private string _key;
        private string _name;
        private string _description;
        private object _value;
        private bool _readOnly;
        private Type _type;
        private ObservableCollection<INodeProperty> _properties;
        private Dictionary<String, INodeProperty> propertiesDict;

        #endregion

        #region Public Ctor

        /// <summary>
        ///  Minimal Constructor for a node property
        /// </summary>
        /// <param name="key">Local Unique key for the property</param>
        /// <param name="name">Name of the property</param>
        /// <param name="type">Type of the property</param>
        /// <param name="readOnly">True if the property is read-only</param>
        /// <param name="description">Short description of the property</param>
        /// <param name="value">Initial value set in the property</param>
        /// <remarks>
        /// Tested  by :
        ///    - TestNodeProperty_Ctor_Minimal_UT001 (Normal case)
        ///    - TestNodeProperty_Ctor_Minimal_UT002 (Key validation error)
        ///    - TestNodeProperty_Ctor_Minimal_UT003 (Name validation error)
        ///    - TestNodeProperty_Ctor_Minimal_UT004 (Value validation error);
        /// </remarks>
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
        /// <param name="properties">Observable collection of other INodeProperty</param>
        /// <remarks>
        /// Tested  by :
        ///     - TestNodeProperty_Ctor_Minimal_UT005
        /// </remarks>
        /// <exception cref="ArgumentNullException" />
        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value, IList<INodeProperty> properties)
        {
            //Validate key
            //Tested by NodePropertyUnitTest.TestNodeProperty_Ctor_Minimal_UT002
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key", "Key cannot be Null, empty string or white space only");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "Name cannot be Null, empty string or white space only");
            }

            //Assign value
            _key = key;
            _name = name;
            _type = type;
            _readOnly = readOnly;
            _description = description;

            //Initialize sub list
            InitializeSubProperties(properties);

            if (!SetValue(value))
            {
                //Throw exception
                throw new ArgumentOutOfRangeException("value", value, "Can't set the new value in the property value");
            }
        }
        #endregion

        #region Public Properties

        #region INodeProperty Implementation

        /// <summary>
        /// Unique Key of the node property.
        /// Can not be null.
        /// </summary>
        /// <remarks>
        /// Implements: INodeProperty.Key
        /// </remarks>
        public string Key { get { return _key; } }

        /// <summary>
        /// Name of the node property.
        /// Can not be null.
        /// </summary>
        /// <remarks>
        /// Implements: INodeProperty.Name
        /// </remarks>
        public string Name { get { return _name; } }

        /// <summary>
        /// Type of the property value
        /// </summary>
        /// <remarks>
        /// Implements: INodeProperty.Type
        /// </remarks>
        public Type Type { get { return _type; } }

        /// <summary>
        /// Readonly flag of the property
        /// </summary>
        /// <return>True if the property is read-only otherwise false</return>
        /// <remarks>
        /// Implements: INodeProperty.ReadOnly
        /// </remarks>
        public bool ReadOnly { get { return _readOnly; } }

        /// <summary>
        /// Short Description of the property
        /// </summary>
        /// <returns>String or String.empty if not available</returns>
        /// <remarks>
        /// Implements: INodeProperty.Description
        /// </remarks>
        public string Description { get { return _description; } }

        /// <summary>
        /// Value object actual in the property
        /// </summary>
        /// <remarks>
        /// Implements: INodeProperty.Value
        /// </remarks>
        public object Value { get { return _value; } }

        /// <summary>
        /// Get the read only properties collection
        /// </summary>
        /// <remarks>
        /// Implements: INodeProperty.Value
        /// </remarks>
        public ReadOnlyCollection<INodeProperty> Properties
        {
            get {
                return new ReadOnlyCollection<INodeProperty>(_properties);
            }
        }

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
            //TODO MUST CHECK IF NULLABLE IS ALLOWED : 
            //Dont know how to do it at the moment.
            if (val == null || isValidType(val))
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
        /// Property changed event handler
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

        /// <summary>
        /// Check if the object is the same type as the property
        /// </summary>
        /// <param name="val">Object to validate the type</param>
        /// <returns>True when the type are the same</returns>
        private bool isValidType(object val)
        {
            return val.GetType() == _type;
        }

        /// <summary>
        /// Initialize the Extra dictionnary of sub property
        /// </summary>
        /// <param name="properties">List of sub properties to initialize</param>
        private void InitializeSubProperties(IList<INodeProperty> properties)
        {
            _properties = new ObservableCollection<INodeProperty>(properties);
            propertiesDict = new Dictionary<string, INodeProperty>();
            foreach (INodeProperty nodeProp in _properties)
            {
                propertiesDict.Add(nodeProp.Key, nodeProp);
            }
        }

        #endregion
    }
}
