using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using System;

namespace OHM.Sys
{
    public class OhmSystem : IOhmSystem
    {

        private ILoggerManager _loggerMng;

        private IInterfacesManager _interfacesMng;

        private IDataManager _dataMng;

        public OhmSystem(IInterfacesManager interfacesMng, ILoggerManager loggerMng, IDataManager dataMng)
        {
            _loggerMng = loggerMng;
            _interfacesMng = interfacesMng;
            _dataMng = dataMng;
        }

        public ILoggerManager LoggerMng
        {
            get { return _loggerMng; }
        }

        public IInterfacesManager InterfacesMng
        {
            get { return _interfacesMng; }
        }

        public IDataManager DataMng
        {
            get { return _dataMng; }
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
