using OHM.Commands;
using System;
using System.ComponentModel;

namespace WpfApplication1.Designer
{
    public sealed class ArgumentsDefinitionDesigner : INotifyPropertyChanged
    {
        private IArgumentDefinition _argumentDef;
        private string _value;

        public ArgumentsDefinitionDesigner(IArgumentDefinition argumentDef)
        {
            _argumentDef = argumentDef;
        }

        public IArgumentDefinition ArgumentDefinition { get { return _argumentDef; } }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
