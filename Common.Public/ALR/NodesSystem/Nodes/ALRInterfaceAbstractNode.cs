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
                if (this.SystemState != SystemNodeStates.created)
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
                if ((this.SystemState != SystemNodeStates.fatal || value == false) && this._startOnLaunchProperty.SetValue(value))
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
            if (this.SystemState != SystemNodeStates.created && this.SystemState != SystemNodeStates.fatal && !this.IsRunning)
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
        
        public bool Init(IDataStore data, ILogger logger, IOhmSystemInterfaceGateway system)
        {
            bool result = false;

            //Make sure we can cet a system gateway
            if (system != null && base.Init(data, logger, this))
            {
                _system = system;
                
                if (SystemState == SystemNodeStates.created && this.Initing())
                {
                    SystemState = SystemNodeStates.operational;
                    result = true;
                }
                //Dont set lower value if already in a fatal state
                else if (SystemState != SystemNodeStates.fatal)
                {
                    SystemState = SystemNodeStates.error;
                }

                //Update start on launch from data store
                if (data.ContainKey("StartOnLaunch"))
                {
                    this.StartOnLaunch = data.GetBool("StartOnLaunch");
                }
            }
            return result;
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

        protected abstract bool Start();

        protected abstract bool Shutdown();
        
        #endregion

        #region internal Protected Functions

        internal protected abstract AbstractPowerNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options);

        #endregion
    }
}
