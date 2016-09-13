using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes.Properties
{
    /// <summary>
    /// 
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
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public NodeProperty(string key, string name, Type type) 
            : this(key, name, type, true) {}

        public NodeProperty(string key, string name, Type type, bool readOnly) 
            : this(key,name,type, readOnly, "") {}

        public NodeProperty(string key, string name, Type type, bool readOnly, string description)
            : this(key, name, type, readOnly, description, null) {}

        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value) 
            : this(key, name, type, readOnly, description, value, new ObservableCollection<INodeProperty>()) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="readOnly"></param>
        /// <param name="description"></param>
        /// <param name="value"></param>
        /// <param name="extraInfo"></param>
        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value, ObservableCollection<INodeProperty> extraInfo)
        {
            _key = key;
            _name = name;
            _type = type;
            _readOnly = readOnly;
            _description = description;
            SetValue(value);
            InitializeExtraInfo(extraInfo);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Key { get { return _key; } }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get { return _type; } }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get { return _description; } }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadOnly { get { return _readOnly; } }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get { return _value; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetValue(object val)
        {
            
            if (val == null || val.GetType() == _type)
            {
                _value = val;
                NotifyPropertyChanged("Value");
                return true;
            }
            return false;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
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
