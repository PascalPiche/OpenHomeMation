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
    public class FakePlugin : PluginBase
    {

        public override Guid Id
        {
            get { return new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e312"); }
        }

        public override string Name
        {
            get { return "Fake Plugin"; }
        }

        public override bool Install(Sys.IOhmSystemInstallGateway system)
        {
            return true;
        }

        public override bool Uninstall(Sys.IOhmSystemUnInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }


        public override InterfaceAbstract CreateInterface(string key, ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}
