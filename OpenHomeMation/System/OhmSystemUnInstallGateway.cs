using log4net;
using OHM.Managers.ALR;
using OHM.Plugins;

namespace OHM.SYS
{
    public sealed class OhmSystemUnInstallGateway : IOhmSystemUnInstallGateway
    {
        #region Private Members

        private IPlugin _plugin;
        private ILog _logger;
        private IInterfacesManager _interfacesMng;

        #endregion

        #region Internal Ctor

        internal OhmSystemUnInstallGateway(IPlugin plugin, ILog logger, IInterfacesManager interfacesMng)
        {
            _logger = logger;
            _plugin = plugin;
            _interfacesMng = interfacesMng;
        }

        #endregion

        #region Public Properties

        public ILog Logger
        {
            get { return _logger; }
        }

        #endregion

        #region Public Methods

        public bool UnRegisterInterface(string key)
        {
            return _interfacesMng.UnRegisterInterface(key, _plugin);
        }

        #endregion
    }
}
