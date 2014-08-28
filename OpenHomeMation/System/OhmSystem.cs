using OHM.Interfaces;
using OHM.Logger;
using System;

namespace OHM.System
{
    [Serializable]
    public class OhmSystem : IOhmSystem
    {

        [NonSerialized]
        private ILoggerManager _loggerMng;

        [NonSerialized]
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
    }
}
