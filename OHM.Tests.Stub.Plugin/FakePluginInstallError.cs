using OHM.Plugins;
using OHM.SYS;
using System;

namespace OHM.Tests.Stub.Plugin
{
    public class FakePluginInstallError : PluginBase
    {

        public override Guid Id
        {
            get { return new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e314"); }
        }

        public override string Name
        {
            get { return "Fake Plugin Install Error"; }
        }

        public override bool Install(IOhmSystemInstallGateway system)
        {
            throw new NotImplementedException();
            //return true;
        }

        public override bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public override bool Update(IOhmSystemInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public override Nodes.ALR.ALRInterfaceAbstractNode CreateInterface(string key)
        {
            throw new NotImplementedException();
        }

        public override Nodes.ALV.IVrType CreateVrNode(string key)
        {
            throw new NotImplementedException();
        }
    }
}
