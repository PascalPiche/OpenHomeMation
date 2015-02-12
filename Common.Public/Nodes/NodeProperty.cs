using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{
    public class NodeProperty : INodeProperty, INotifyPropertyChanged
    {

        private string _key;
        private string _name;
        private string _description;
        private object _value;
        private bool _readOnly;
        private Type _type;
        private ObservableCollection<INodeProperty> _extraInfo = new ObservableCollection<INodeProperty>();
        private Dictionary<String, INodeProperty> _extraInfoDict = new Dictionary<String, INodeProperty>();

        public NodeProperty(string key, string name, Type type)
        {
            _key = key;
            _name = name;
            _type = type;
            _readOnly = true;
        }

        public NodeProperty(string key, string name, Type type, bool readOnly) : this(key,name,type)
        {
            _readOnly = readOnly;
        }

        public NodeProperty(string key, string name, Type type, bool readOnly, string description) : this(key, name, type, readOnly)
        {
            _description = description;
        }

        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value) : this(key, name, type, readOnly, description)
        {
            SetValue(value);
        }

        public NodeProperty(string key, string name, Type type, bool readOnly, string description, object value, ObservableCollection<INodeProperty> extraInfo)
            : this(key, name, type, readOnly, description, value)
        {
            _extraInfo = extraInfo;
            foreach (INodeProperty nodeProp in _extraInfo)
            {
                _extraInfoDict.Add(nodeProp.Key, nodeProp);
            }
        }

        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public string Description
        {
            get { return _description; }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
        }

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

        public object Value
        {
            get { return _value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        
    }
}
