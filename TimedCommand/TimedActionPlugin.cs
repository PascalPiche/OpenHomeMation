using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using System;

namespace TimedCommand
{
    public class TimedActionPlugin : PluginBase
    {
        private Guid _id = new Guid("5d4a6b0c-3347-4791-a989-e9362d30cad7");
        
        private string _name = "Timed Action Plugin";

        public override Guid Id { get { return _id; } }

        public override string Name { get { return _name; } }

        public override bool Install(IOhmSystemInstallGateway system)
        {
            return system.RegisterVrType("timedAction");
        }

        public override bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            return false; // system.UnRegisterInter(_interfaceKey);
        }

        public override bool Update()
        {
            return true;
        }

        public override ALRInterfaceAbstractNode CreateInterface(string key)
        {
            throw new NotImplementedException();
        }

        public override OHM.VAL.IVrType CreateVrNode(string key)
        {
            throw new NotImplementedException();
        }
    }
}
