using OHM.Interfaces;
using OHM.Logger;
using System;

namespace OHM.Sys
{
    public class OhmSystem : IOhmSystem
    {

        private ILoggerManager _loggerMng;

        private IInterfacesManager _interfacesMng;

        internal OhmSystem(IInterfacesManager interfacesMng, ILoggerManager loggerMng)
        {
            _loggerMng = loggerMng;
            _interfacesMng = interfacesMng;
        }

        internal ILoggerManager LoggerMng
        {
            get { return _loggerMng; }
        }

        internal IInterfacesManager InterfacesMng
        {
            get { return _interfacesMng; }
        }

        public IOhmSystemInstallGateway GetInstallGateway(Plugins.IPlugin plugin)
        {
            return new OhmSystemInstallGateway(this, plugin);
        }


        public IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf)
        {
            return new OhmSystemInterfaceGateway(this, interf);
        }
    }
}
