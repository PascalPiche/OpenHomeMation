using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Plugins;

namespace OHM.SYS
{
    
    public sealed class OhmSystemUnInstallGateway : IOhmSystemUnInstallGateway
    {
        private IPlugin _plugin;
        private ILogger _logger;
        private IInterfacesManager _interfacesMng;

        internal OhmSystemUnInstallGateway(IPlugin plugin, ILogger logger, IInterfacesManager interfacesMng)
        {
            //_system = system;
            _logger = logger;
            _plugin = plugin;
            _interfacesMng = interfacesMng;
        }

        public Logger.ILogger Logger
        {
            get { return _logger; }
        }

        public bool UnRegisterInterface(string key)
        {
            return _interfacesMng.UnRegisterInterface(key, _plugin);
        }
    }
}
