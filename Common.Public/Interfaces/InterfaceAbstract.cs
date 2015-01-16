using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Sys;

namespace OHM.Interfaces
{
    public abstract class InterfaceAbstract : NodeAbstract, IInterface
    {

        private InterfaceState _state = InterfaceState.Disabled;
        private bool _startOnLaunch = false;
        
        private IOhmSystemInterfaceGateway _system;

        public InterfaceAbstract(string key, string name) 
            : base(key, name, null)
        {
            
            //Register Default commands

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
            NotifyPropertyChanged("State");
        }

        public virtual void Shutdown()
        {
            NotifyPropertyChanged("State");
            State = Interfaces.InterfaceState.Disabled;
        }

        public void Init(ILogger logger, IDataStore data, IOhmSystemInterfaceGateway system)
        {
            base.Init(logger, data);
            _startOnLaunch = data.GetBool("StartOnLaunch");
            NotifyPropertyChanged("StartOnLaunch");
            _system = system;
        }


        public bool StartOnLaunch
        {
            get { return _startOnLaunch; }
            set { 

                _startOnLaunch = value;
                DataStore.StoreBool("StartOnLaunch", value);
                DataStore.Save();
                NotifyPropertyChanged("StartOnLaunch");
            }

        }
    }
}
