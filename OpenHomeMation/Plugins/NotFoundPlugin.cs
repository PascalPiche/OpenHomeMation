using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    public sealed class NotFoundPlugin : IPlugin
    {
        private Guid _guid;

        public NotFoundPlugin(string guid)
        {
            _guid = new Guid(guid);
        }

        public Guid Id
        {
            get { return _guid; }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool Install(Sys.IOhmSystemInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public bool Uninstall(Sys.IOhmSystemUnInstallGateway system)
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public Interfaces.InterfaceAbstract CreateInterface(string key, ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}
