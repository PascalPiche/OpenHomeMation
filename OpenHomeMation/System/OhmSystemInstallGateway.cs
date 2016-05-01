using OHM.Plugins;

namespace OHM.Sys
{
    
    public class OhmSystemInstallGateway : IOhmSystemInstallGateway
    {

        private IOhmSystemInternal _system;
        private IPlugin _plugin;

        public OhmSystemInstallGateway(IOhmSystemInternal system, IPlugin plugin)
        {
            _system = system;
            _plugin = plugin;
        }

        public Logger.ILogger Logger
        {
            get { return _system.LoggerMng.GetLogger(_plugin.Name); }
        }

        public bool RegisterInterface(string key)
        {
            return _system.InterfacesMng.RegisterInterface(key, _plugin, _system);
        }

        public bool RegisterVrType(string key)
        {
            return _system.InterfacesMng.RegisterInterface(key, _plugin, _system);
        }
    }
}
