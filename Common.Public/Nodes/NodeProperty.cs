﻿using System;
using System.ComponentModel;

namespace OHM.Nodes
{
    public class NodeProperty : INodeProperty, INotifyPropertyChanged
    {

        private string _key;
        private string _name;
        private string _description;
        private object _value;
        private Type _type;

        public NodeProperty(string key, string name, Type type)
        {
            _key = key;
            _name = name;
            _type = type;
        }
        public NodeProperty(string key, string name, Type type, string description) : this(key, name, type)
        {
            _description = description;
        }

        public NodeProperty(string key, string name, Type type, string description, object value) : this(key, name, type, description)
        {
            SetValue(value);
        }


        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public string Description
        {
            get { return _description; }
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
