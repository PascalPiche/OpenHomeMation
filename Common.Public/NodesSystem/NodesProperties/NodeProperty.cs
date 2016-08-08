using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes.Properties
{
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

        public NodeProperty(string key, string name, Type type) 
            : this(key, name, type, true) {}

        public NodeProperty(string key, string name, Type type, bool readOnly) 
            : this(key,name,type, readOnly, "") {}

        public NodeProperty(string key, string name, Type type, bool readOnly, string description)
            : this(key, name, type, readOnly, description, null) {}

        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value) 
            : this(key, name, type, readOnly, description, value, new ObservableCollection<INodeProperty>()) {}

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

        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public string Description { get { return _description; } }

        public bool ReadOnly { get { return _readOnly; } }

        public object Value { get { return _value; } }

        #endregion

        #region Public API

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

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Functions

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
