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
        private IInterfacesManager _interfacesMng;

        internal ILoggerManager LoggerMng
        {
            get { return _loggerMng; }
            set { _loggerMng = value; }
        }

        internal IInterfacesManager InterfacesMng
        {
            get { return _interfacesMng; }
            set { _interfacesMng = value; }
        }

        /*
        public void RegisterInterface(IInterface newInterface)
        {
            //var t = log4net.Appender.;

            //TODO throw new NotImplementedException();
        }

        public void RegisterObjectType(IAbstractNode obj)
        {
            //TODO throw new NotImplementedException();
        }*/

        public IOhmSystemInstallGateway getInstallGateway(Plugins.IPlugin plugin)
        {
            throw new NotImplementedException();
        }
    }
}
