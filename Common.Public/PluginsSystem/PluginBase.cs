using OHM.RAL;
using OHM.SYS;
using OHM.VAL;
using System;

namespace OHM.Plugins
{
    [Serializable]
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {
        #region Private members

        private PluginStates _state = PluginStates.Ready;

        #endregion

        #region Public Properties

        public abstract Guid Id { get; }

        public abstract string Name { get; }

        public PluginStates State { 
            get { return _state; }
            protected set
            {
                if (value == PluginStates.NotFound)
                {
                    throw new ArgumentException("NotFound is a reserved status for internal operations only.", "State");
                }
                _state = value;
            }
        }

        #endregion

        #region Public Methods

        public abstract bool Install(IOhmSystemInstallGateway system);

        public abstract bool Uninstall(IOhmSystemUnInstallGateway system);

        public abstract bool Update();

        public abstract ALRInterfaceAbstractNode CreateInterface(string key);

        public abstract IVrType CreateVrNode(string key);

        #endregion
    }
}
