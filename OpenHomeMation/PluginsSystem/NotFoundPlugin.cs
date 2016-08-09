using OHM.RAL;
using OHM.SYS;
using System;

namespace OHM.Plugins
{
    public sealed class NotFoundPlugin : IPlugin
    {
        #region Private Members

        private Guid _guid;
        private string _name;

        #endregion

        #region Public CTOR

        internal NotFoundPlugin(string guid, string name)
        {
            _guid = new Guid(guid);
            _name = name;
        }

        #endregion

        #region Public Properties

        public Guid Id      { get { return _guid; } }

        public string Name  { get { return _name; } }

        #endregion

        #region Public Methods

        public bool Install(IOhmSystemInstallGateway system)
        {
            return false;
        }

        public bool Update(IOhmSystemInstallGateway system)
        {
            return false;
        }

        public PluginStates State
        {
            get { return PluginStates.NotFound; }
        }

        public bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public ALRInterfaceAbstractNode CreateInterface(string key)
        {
            throw new NotImplementedException();
        }

        public VAL.IVrType CreateVrNode(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
