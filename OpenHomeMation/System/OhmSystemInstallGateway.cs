﻿using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;

namespace OHM.Sys
{
    
    public sealed class OhmSystemInstallGateway : IOhmSystemInstallGateway
    {
        private IPlugin _plugin;
        private ILogger _logger;
        private IInterfacesManager _interfacesMng;

        internal OhmSystemInstallGateway(IPlugin plugin, ILogger logger, IInterfacesManager interfacesMng)
        {
            _logger = logger;
            _interfacesMng = interfacesMng;
            _plugin = plugin;
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
            return false;
        }
    }
}
