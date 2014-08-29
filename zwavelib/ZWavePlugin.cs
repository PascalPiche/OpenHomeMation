using System.AddIn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OHM.Logger;
using OHM.Commands;
using OHM.Plugins;
using OHM.System;
using OHM.Interfaces;

namespace ZWaveLib
{
    
    public class ZWavePlugin : PluginBase
    {
        private Guid _id = new Guid("8d8a6e6b-4ddb-450a-8f3c-7b361a9081b4");
        
        private string _name = "Zwave Plugin";

        public override Guid Id { get { return _id; } }

        public override string Name { get { return _name; } }

        public override bool Install(IOhmSystemInstallGateway system)
        {
            return system.RegisterInterface("ZWaveInterface");
        }

        public override bool Uninstall()
        {
            return true;
        }

        public override bool Update()
        {
            return true;
        }

        public override IInterface CreateInterface(string key, ILogger logger)
        {
            switch (key)
	        {
                case "ZWaveInterface":
                    return new ZWaveInterface(logger);
	        }
            return null;
        }

    }
}
