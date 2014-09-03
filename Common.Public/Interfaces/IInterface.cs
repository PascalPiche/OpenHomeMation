using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public interface IInterface : INode, INotifyPropertyChanged
    {

        InterfaceState State { get; }

        bool IsRunning { get; }

        void Start();

        void Shutdown();

        
        
    }

    public enum InterfaceState
    {
        Enabled,
        Disabled,
        Error
    }

    public abstract class InterfaceAbstract : IInterface
    {

        private InterfaceState _state = InterfaceState.Disabled;
        private string _key;
        private string _name;
        private IList<ICommand> _commands;
        public event PropertyChangedEventHandler PropertyChanged;

        public InterfaceAbstract(string key, string name)
        {
            _key = key;
            _name = name;
            _commands = new ObservableCollection<ICommand>();
        }

        public InterfaceState State
        {
            get { return _state; }

            protected set 
            {
                _state = value;
                NotifyPropertyChanged("State");
                NotifyPropertyChanged("IsRunning");
            }
        }

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsRunning
        {
            get { return State == Interfaces.InterfaceState.Enabled; }
        }

        public virtual void Start()
        {
            State = Interfaces.InterfaceState.Enabled;
        }

        public virtual void Shutdown()
        {
            State = Interfaces.InterfaceState.Disabled;
        }

        public IList<ICommand> Commands { get { return _commands; } }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
