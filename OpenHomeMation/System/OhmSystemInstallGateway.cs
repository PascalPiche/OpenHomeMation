using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Plugins;
using OHM.VAL;

namespace OHM.SYS
{
    
    public sealed class OhmSystemInstallGateway : IOhmSystemInstallGateway
    {
        private IPlugin _plugin;
        private ILogger _logger;
        private IInterfacesManager _interfacesMng;
        private IVrManager _vrMng;

        internal OhmSystemInstallGateway(IPlugin plugin, ILogger logger, IInterfacesManager interfacesMng, IVrManager vrMng)
        {
            _logger = logger;
            _interfacesMng = interfacesMng;
            _plugin = plugin;
            _vrMng = vrMng;
        }

        public Logger.ILogger Logger
        {
            get { return _logger;}
        }

        public bool RegisterInterface(string key)
        {
            return _interfacesMng.RegisterInterface(key, _plugin);
        }

        public bool RegisterVrType(string key)
        {
            return _vrMng.RegisterVrType(key, _plugin);
        }
    }
}
