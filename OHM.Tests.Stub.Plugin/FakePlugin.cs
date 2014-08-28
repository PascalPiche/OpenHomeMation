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
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Install(System.IOhmSystemInstallGateway system)
        {
            throw new NotImplementedException();
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
