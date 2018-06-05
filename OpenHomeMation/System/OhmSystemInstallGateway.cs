using log4net;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Plugins;

namespace OHM.SYS
{
    public sealed class OhmSystemInstallGateway : IOhmSystemInstallGateway
    {
        #region Private Members

        private IPlugin _plugin;
        private ILog _logger;
        private IInterfacesManager _interfacesMng;
        private IVrManager _vrMng;

        #endregion

        #region Internal Ctor

        internal OhmSystemInstallGateway(IPlugin plugin, ILog logger, IInterfacesManager interfacesMng, IVrManager vrMng)
        {
            _logger = logger;
            _interfacesMng = interfacesMng;
            _plugin = plugin;
            _vrMng = vrMng;
        }

        #endregion

        #region Public Properties

        public ILog Logger
        {
            get { return _logger;}
        }

        #endregion

        #region Public Methods

        public bool RegisterInterface(string key)
        {
            return _interfacesMng.RegisterInterface(key, _plugin);
        }

        public bool RegisterVrType(string key)
        {
            return _vrMng.RegisterVrType(key, _plugin);
        }

        #endregion
    }
}
