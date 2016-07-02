using OHM.Logger;
using OHM.RAL;
using OHM.SYS;
using System;

namespace OHM.Plugins
{
    public sealed class NotFoundPlugin : IPlugin
    {
        private Guid _guid;
        private string _name;

        public NotFoundPlugin(string guid, string name)
        {
            _guid = new Guid(guid);
            _name = name;
        }

        public Guid Id
        {
            get { return _guid; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool Install(IOhmSystemInstallGateway system)
        {
            return false;
        }

        public bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            return false;
        }

        public InterfaceAbstract CreateInterface(string key, ILogger logger)
        {
            throw new NotImplementedException();
        }

        public PluginStates State
        {
            get { return PluginStates.NotFound; }
        }
    }
}
