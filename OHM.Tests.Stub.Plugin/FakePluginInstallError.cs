using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override bool Install(System.IOhmSystemInstallGateway system)
        {
            throw new NotImplementedException();
            //return true;
        }

        public override bool Uninstall()
        {
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }

        public override IInterface CreateInterface(string key, ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}
