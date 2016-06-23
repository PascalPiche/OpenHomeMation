using OHM.Interfaces;
using OHM.Logger;
using OHM.Sys;
using System;

namespace OHM.Plugins
{
    [Serializable]
    public enum PluginStatesEnum
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
        private PluginStatesEnum _state = PluginStatesEnum.Normal;

        public PluginBase() {}

        public abstract Guid Id { get; }

        public abstract string Name { get; }

        public PluginStatesEnum State { get { return _state; } }

        public abstract bool Install(IOhmSystemInstallGateway system);

        public abstract bool Uninstall(IOhmSystemUnInstallGateway system);

        public abstract bool Update();

        public abstract InterfaceAbstract CreateInterface(string key, ILogger logger);

        protected void SetState(PluginStatesEnum newState)
        {
            if (newState == PluginStatesEnum.NotFound)
            {
                throw new ArgumentException("NotFound is a reserved status for internal operations only.", "newState");
            }
            _state = newState;
        }

    }
}
