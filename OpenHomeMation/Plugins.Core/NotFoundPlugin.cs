﻿using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    public sealed class NotFoundPlugin : IPlugin
    {
        private Guid _guid;
        private String _name;

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

        public bool Install(Sys.IOhmSystemInstallGateway system)
        {
            return false;
        }

        public bool Uninstall(Sys.IOhmSystemUnInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            return false;
        }

        public Interfaces.InterfaceAbstract CreateInterface(string key, ILogger logger)
        {
            throw new NotImplementedException();
        }

        public PluginStates State
        {
            get { return PluginStates.NotFound; }
        }
    }
}