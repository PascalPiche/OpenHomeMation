using OHM.Data;
using OHM.Logger;
using OHM.SYS;
using System.Collections.Generic;

namespace OHM.Nodes.ALR
{
    public abstract class ALRInterfaceAbstractNode : ALRAbstractTreeNode, IALRInterface
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

        public ALRInterfaceStates InterfaceState { get { return _interfaceState; }
            protected set
            {
                if (this.State != NodeStates.initializing)
                {
                    _interfaceState = value;
                    NotifyPropertyChanged("InterfaceState");
                    NotifyPropertyChanged("IsRunning");
                }
            }
        }

        public bool IsRunning { get { return InterfaceState == ALRInterfaceStates.Enabled; } }

        public bool StartOnLaunch { get { return _startOnLaunch; }
            internal set
            {
                if ((this.State == NodeStates.fatal && value == true) || this.State == NodeStates.initializing)
                {
                    return;
                }

                _startOnLaunch = value;

                //TODO : MOVE IT INSIDE A SUB META DICTIONARY FOR INTERFACE
                DataStore.StoreBool("StartOnLaunch", value);
                DataStore.Save();
                NotifyPropertyChanged("StartOnLaunch");
            }
        }

        #endregion

        #region Public Api

        public bool Starting()
        {
            bool result = false;
            if (this.State != NodeStates.initializing && this.State != NodeStates.fatal && !this.IsRunning)
            {
                Logger.Debug("Starting interface");
                Start();
                InterfaceState = ALRInterfaceStates.Enabled;
                result = true;
                Logger.Info("Starting interface was successfully");
            }
            return result;
        }

        public bool Shutdowning()
        {
            bool result = true;
            if (this.IsRunning)
            {
                result = false;
                Logger.Debug("Interface Shutdowning");
                Shutdown();
                InterfaceState = ALRInterfaceStates.Disabled;
                result = true;
                Logger.Info("Interface Shutdowning was successfully");
                
            }
            return result;
        }
        
        public void Init(IDataStore data, ILogger logger, IOhmSystemInterfaceGateway system)
        {
            if (base.Init(data, logger, this))
            {
                _startOnLaunch = data.GetBool("StartOnLaunch");
                _system = system;

                State = NodeStates.normal;
                NotifyPropertyChanged("StartOnLaunch");

                this.Initing();
            }
        }

        public new bool ExecuteCommand(string nodeKey, string commandKey, IDictionary<string, string> arguments)
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

        protected abstract bool Shutdown();
        
        #endregion

        #region internal Protected Functions

        internal protected abstract AbstractPowerNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options);

        #endregion
    }
}
