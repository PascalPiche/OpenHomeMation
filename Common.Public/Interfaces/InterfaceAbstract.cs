using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Sys;
using System.Collections.Generic;

namespace OHM.Interfaces
{
    public abstract class InterfaceAbstract : NodeAbstract, IInterface
    {
        #region Private Members

        private InterfaceStates _state = InterfaceStates.Disabled;
        private bool _startOnLaunch = false;
        private IOhmSystemInterfaceGateway _system;

        #endregion

        #region Public Ctor

        public InterfaceAbstract(string key, string name, ILogger logger) 
            : base(key, name, logger)
        {}

        #endregion

        #region public Properties

        public new InterfaceStates State
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
            get { return State == Interfaces.InterfaceStates.Enabled; }
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
            Logger.Info( this.Name + " Interface initing");
            Start();
            State = Interfaces.InterfaceStates.Enabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Inited");
        }

        public void Shutdowning()
        {
            Logger.Info(this.Name + " Interface Shutdowning");
            Shutdown();
            State = Interfaces.InterfaceStates.Disabled;
            NotifyPropertyChanged("State");
            Logger.Info(this.Name + " Interface Shutdowned");
        }

        public bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, object> arguments)
        {
            if (this.Key == nodeKey)
            {
                return this.ExecuteCommand(commandKey, arguments);
            }
            else
            {
                //Lookup the node list
                INode node = this.GetChild(nodeKey);
                if (node != null)
                {
                    return node.ExecuteCommand(commandKey, arguments);
                }
            }
            
            return false;
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

        #region Private functions

        private new bool ExecuteCommand(string commandKey, Dictionary<string, object> arguments)
        {
            //We need to found the right node
            if (_commandsDic.ContainsKey(commandKey))
            {
                return _commandsDic[commandKey].Execute(arguments);
            }
            return false;
        }

        #endregion

    }
}
