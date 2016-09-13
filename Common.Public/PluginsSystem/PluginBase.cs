using OHM.Nodes.ALR;
using OHM.Nodes.ALV;
using OHM.SYS;
using System;

namespace OHM.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {
        #region Private members

        private PluginStates _state = PluginStates.Ready;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
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

        #region Public Abstract Properties

        /// <summary>
        /// 
        /// </summary>
        public abstract Guid Id { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string Name { get; }

        #endregion

        #region Public Abstract Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public abstract bool Install(IOhmSystemInstallGateway system);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public abstract bool Uninstall(IOhmSystemUnInstallGateway system);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public abstract bool Update(IOhmSystemInstallGateway system);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract ALRInterfaceAbstractNode CreateInterface(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract IVrType CreateVrNode(string key);

        #endregion
    }
}
