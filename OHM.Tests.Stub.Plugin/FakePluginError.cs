using OHM.Logger;
using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using System;

namespace OHM.Tests.Stub.Plugin
{
    public class FakePluginError : PluginBase
    {

        public FakePluginError()
        {
            //throw new NotImplementedException();
        }

        public override Guid Id
        {
            get { return new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e318"); }
        }

        public override string Name
        {
            get { return "Fake Plugin Error"; }
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

        public override bool Update()
        {
            throw new NotImplementedException();
        }

        public override RalInterfaceNodeAbstract CreateInterface(string key)
        {
            throw new NotImplementedException();
        }
    }
}
