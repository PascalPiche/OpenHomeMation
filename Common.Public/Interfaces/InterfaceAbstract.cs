using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public abstract class InterfaceAbstract : NodeAbstract, IInterface
    {

        private InterfaceState _state = InterfaceState.Disabled;

        public InterfaceAbstract(string key, string name) 
            : base(key, name)
        {}

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
    }
}
