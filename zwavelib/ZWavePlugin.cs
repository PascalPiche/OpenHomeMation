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

namespace ZWaveLib
{
    [Serializable()]
    public class ZWavePlugin : PluginBase
    {
        private Guid _id = new Guid("8d8a6e6b-4ddb-450a-8f3c-7b361a9081b4");
        
        private string _name = "Zwave Plugin";
        
        private Version _version = new Version(0, 1);
        
        //private IList<ICommandDefinition> _commands;

        public ZWavePlugin()
        {
            
        }

        public override Guid Id { get { return _id; } }

        public override string Name { get { return _name; } }

        public override Version Version { get { return _version; } }

        public override bool Install(IOhmSystemInstallGateway system)
        {
            system.RegisterInterface(new ZWaveInterface(system.Logger));
            return true;
        }

        public override bool Uninstall()
        {
            return true;
        }

        public override bool Update()
        {
            return true;
        }

    }
}
