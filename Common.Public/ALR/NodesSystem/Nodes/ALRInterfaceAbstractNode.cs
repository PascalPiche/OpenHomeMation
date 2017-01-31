using OHM.Data;
using OHM.Logger;
using OHM.Nodes.Properties;
using OHM.SYS;
using System.Collections.Generic;

namespace OHM.Nodes.ALR
{
    public abstract class ALRInterfaceAbstractNode : ALRAbstractTreeNode, IALRInterface
    {
        #region Private Members

        private INodeProperty _startOnLaunchProperty;
        private ALRInterfaceStates _interfaceState = ALRInterfaceStates.Disabled;
        private IOhmSystemInterfaceGateway _system;

        #endregion

        #region Protected Ctor

        protected ALRInterfaceAbstractNode(string key, string name)
            : base(key, name)
        {
            _startOnLaunchProperty = new NodeProperty(PREFIX_SYSTEM + "startOnLaunch", "StartOnLaunch", typeof(bool), false, "System start on launch interface", false);
            this.RegisterProperty(_startOnLaunchProperty);
        }

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

        public bool StartOnLaunch { 
            get { 
                return (bool)this._startOnLaunchProperty.Value;
            }
            internal set
            {
                if ((this.State != NodeStates.fatal || value == false) && this._startOnLaunchProperty.SetValue(value))
                {
                    DataStore.StoreBool("StartOnLaunch", value);
                    DataStore.Save();
                    NotifyPropertyChanged("StartOnLaunch");
                }
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
                Logger.Info("Interface Shutdowning was successfully executed");
                
            }
            return result;
        }
        
        public void Init(IDataStore data, ILogger logger, IOhmSystemInterfaceGateway system)
        {
            if (system != null && base.Init(data, logger, this))
            {
                _system = system;
                
                if (this.Initing() && State == NodeStates.initializing)
                {
                    State = NodeStates.normal;
                }
                else if (State != NodeStates.fatal)
                {
                    State = NodeStates.error;
                }

                if (data.ContainKey("StartOnLaunch"))
                {
                    this.StartOnLaunch = data.GetBool("StartOnLaunch");
                }
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
