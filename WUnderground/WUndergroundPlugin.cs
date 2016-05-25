﻿using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Data;

namespace WUnderground
{
    public class WUndergroundPlugin : PluginBase
    {
        private Guid _id = new Guid("6e1ab586-e584-4eb3-ab3e-b46bd2a2d2c0");

        private string _name = "Weather Underground Plugin";

        private const string _interfaceKey = "WUndergroundInterface";

        public override Guid Id { get { return _id; } }

        public override string Name { get { return _name; } }

        public override bool Install(IOhmSystemInstallGateway system)
        {
            return system.RegisterInterface(_interfaceKey);
        }

        public override bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            return system.UnRegisterInterface(_interfaceKey);
        }

        public override bool Update()
        {
            return true;
        }

        public override InterfaceAbstract CreateInterface(string key, ILogger logger)
        {
            switch (key)
            {
                case _interfaceKey:
                    return new WUndergroundInterface(logger);
            }
            return null;
        }

    }
}