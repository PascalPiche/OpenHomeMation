using OHM.Plugins;

namespace OHM.Sys
{
    
    public class OhmSystemUnInstallGateway : IOhmSystemUnInstallGateway
    {

        private IOhmSystemInternal _system;
        private IPlugin _plugin;

        public OhmSystemUnInstallGateway(IOhmSystemInternal system, IPlugin plugin)
        {
            _system = system;
            _plugin = plugin;
        }

        public Logger.ILogger Logger
        {
            get { return _system.LoggerMng.GetLogger(_plugin.Name); }
        }

        public bool UnRegisterInterface(string key)
        {
            return _system.InterfacesMng.UnRegisterInterface(key, _plugin, _system);
        }
    }
}
