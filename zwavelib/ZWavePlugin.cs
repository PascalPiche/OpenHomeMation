using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using System;

namespace ZWaveLib
{
    public class ZWavePlugin : PluginBase
    {
        private Guid _id = new Guid("8d8a6e6b-4ddb-450a-8f3c-7b361a9081b4");
        
        private string _name = "Zwave Plugin";

        private const string _interfaceKey = "ZWaveInterface";

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

        public override RalInterfaceNodeAbstract CreateInterface(string key)
        {
            switch (key)
	        {
                case _interfaceKey:
                    return new ZWaveInterface();
	        }
            return null;
        }

        public override OHM.VAL.IVrType CreateVrNode(string key)
        {
            throw new NotImplementedException();
        }
    }
}
