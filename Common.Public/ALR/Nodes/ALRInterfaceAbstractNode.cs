using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.SYS;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class ALRInterfaceAbstractNode : ALRAbstractNode, IALRInterface
    {
        #region Private Members

        private ALRInterfaceStates _interfaceState = ALRInterfaceStates.Disabled;
        private bool _startOnLaunch = false;
        private IOhmSystemInterfaceGateway _system;

        #endregion

        #region Protected Ctor

        protected ALRInterfaceAbstractNode(string key, string name)
            : base(key, name)
        { }

        #endregion

        #region public Properties

        public ALRInterfaceStates InterfaceState
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
            get { return InterfaceState == ALRInterfaceStates.Enabled; }
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
            Logger.Debug("Starting interface : " + this.Name);
            Start();
            InterfaceState = ALRInterfaceStates.Enabled;
            NotifyPropertyChanged("State");
            Logger.Info("Starting interface : " + this.Name + " was successfull");
        }

        public void Shutdowning()
        {
            Logger.Debug(this.Name + " Interface Shutdowning");
            Shutdown();
            InterfaceState = ALRInterfaceStates.Disabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Shutdowned");
        }
        
        public void Init(IDataStore data, ILogger logger, IOhmSystemInterfaceGateway system)
        {
            base.Init(data, logger, this);
            _startOnLaunch = data.GetBool("StartOnLaunch");
            this.State = NodeStates.normal;
            NotifyPropertyChanged("StartOnLaunch");
            _system = system;
            this.Initing();
        }

        public new bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, string> arguments)
        {
            return base.ExecuteCommand(nodeKey, commandKey, arguments);
        }

        public new bool CanExecuteCommand(string nodeKey, string key)
        {
            return base.CanExecuteCommand(nodeKey, key);
        }

        #endregion
        
        #region Protected abstract functions

        protected abstract void Start();

        protected abstract void Shutdown();

        #endregion

        #region internal Protected Functions

        internal protected abstract AbstractNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options);

        #endregion
    }
}
