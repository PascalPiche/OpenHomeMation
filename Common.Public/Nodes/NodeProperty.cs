using System;
using System.ComponentModel;

namespace OHM.Nodes
{
    public class NodeProperty : INodeProperty, INotifyPropertyChanged
    {

        private string _key;
        private string _name;
        private object _value;
        private Type _type;

        public NodeProperty(string key, string name, Type type)
        {
            _key = key;
            _name = name;
            _type = type;
        }

        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
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
