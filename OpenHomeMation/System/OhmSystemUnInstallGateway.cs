using log4net;
using log4net.Core;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Plugins;

namespace OHM.SYS
{
    
    public sealed class OhmSystemUnInstallGateway : IOhmSystemUnInstallGateway
    {
        private IPlugin _plugin;
        private ILog _logger;
        private IInterfacesManager _interfacesMng;

        internal OhmSystemUnInstallGateway(IPlugin plugin, ILog logger, IInterfacesManager interfacesMng)
        {
            _logger = logger;
            _plugin = plugin;
            _interfacesMng = interfacesMng;
        }

        public ILog Logger
        {
            get { return _logger; }
        }

        public bool UnRegisterInterface(string key)
        {
            return _interfacesMng.UnRegisterInterface(key, _plugin);
        }
    }
}
