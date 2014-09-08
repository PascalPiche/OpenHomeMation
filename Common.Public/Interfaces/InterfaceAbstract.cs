using OHM.Commands;
using OHM.Logger;
using OHM.Nodes;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public abstract class InterfaceAbstract : NodeAbstract, IInterface
    {

        private InterfaceState _state = InterfaceState.Disabled;
        
        private IOhmSystemInterfaceGateway _system;

        public InterfaceAbstract(string key, string name, ILogger logger) 
            : base(key, name, logger, null)
        {
            
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

        public void Init(IOhmSystemInterfaceGateway system)
        {
            base.Init();
            _system = system;
        }
       
        
    }
}
