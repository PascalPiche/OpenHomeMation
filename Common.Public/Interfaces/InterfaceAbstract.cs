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

        public InterfaceAbstract(string key, string name, ILogger logger) 
            : base(key, name, logger)
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

        public void Starting()
        {
            Logger.Info( this.Name + " Interface initing");
            Start();
            State = Interfaces.InterfaceState.Enabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Inited");
        }

        protected abstract void Start();

        public void Shutdowning()
        {
            Logger.Info(this.Name + " Interface Shutdowning");
            Shutdown();
            State = Interfaces.InterfaceState.Disabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Shutdowned");
        }

        protected abstract void Shutdown();

        public void Init(IDataStore data, IOhmSystemInterfaceGateway system)
        {
            base.Init(data);
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
