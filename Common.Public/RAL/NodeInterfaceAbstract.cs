using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.SYS;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class NodeInterfaceAbstract : NodeAbstract, IInterface
    {
        #region Private Members

        private InterfaceStates _interfaceState = InterfaceStates.Disabled;
        private bool _startOnLaunch = false;
        private IOhmSystemInterfaceGateway _system;

        #endregion

        #region Protected Ctor

        protected NodeInterfaceAbstract(string key, string name, ILogger logger)
            : base(key, name, logger)
        { }

        #endregion

        #region public Properties

        public InterfaceStates InterfaceState
        {
            get { return _interfaceState; }

            protected set
            {
                _interfaceState = value;
                NotifyPropertyChanged("InterfaceState");
                NotifyPropertyChanged("IsRunning");
            }
        }

        public bool IsRunning
        {
            get { return InterfaceState == InterfaceStates.Enabled; }
        }

        public bool StartOnLaunch
        {
            get { return _startOnLaunch; }
            set
            {
                _startOnLaunch = value;
                DataStore.StoreBool("StartOnLaunch", value);
                DataStore.Save();
                NotifyPropertyChanged("StartOnLaunch");
            }
        }

        #endregion

        #region Public Api

        public void Starting()
        {
            Logger.Info(this.Name + " Interface initing");
            Start();
            InterfaceState = InterfaceStates.Enabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Inited");
        }

        public void Shutdowning()
        {
            Logger.Info(this.Name + " Interface Shutdowning");
            Shutdown();
            InterfaceState = InterfaceStates.Disabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Shutdowned");
        }
        
        public void Init(IDataStore data, IOhmSystemInterfaceGateway system)
        {
            base.Init(data);
            _startOnLaunch = data.GetBool("StartOnLaunch");
            NotifyPropertyChanged("StartOnLaunch");
            _system = system;
        }

        #endregion
        
        #region Protected abstract functions

        protected abstract void Start();

        protected abstract void Shutdown();

        #endregion
    }
}
