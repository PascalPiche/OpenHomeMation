using OHM.Interfaces;
using OHM.Logger;
using OHM.Sys;
using System;

namespace OHM.Plugins
{
    [Serializable]
    public enum PluginStates
    {
        NotFound = -2,
        FatalError = -1,
        Error = 0,
        Warning = 1,
        Normal = 2
    }

    [Serializable]
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {
        private PluginStates _state = PluginStates.Normal;

        public PluginBase() {}

        public abstract Guid Id { get; }

        public abstract string Name { get; }

        public PluginStates State { 
            get { return _state; }
            protected set
            {
                if (value == PluginStates.NotFound)
                {
                    throw new ArgumentException("NotFound is a reserved status for internal operations only.", "newState");
                }
                _state = value;
                //NotifyPropertyChanged("State");
                //NotifyPropertyChanged("IsRunning");
            }
        }

        public abstract bool Install(IOhmSystemInstallGateway system);

        public abstract bool Uninstall(IOhmSystemUnInstallGateway system);

        public abstract bool Update();

        public abstract InterfaceAbstract CreateInterface(string key, ILogger logger);

    }
}
