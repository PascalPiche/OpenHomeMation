using OHM.Common.Vr;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;

namespace OHM.Sys
{
    public class OhmSystem : IOhmSystemInternal
    {
        private ILoggerManager _loggerMng;
        private IInterfacesManager _interfacesMng;
        private IDataManager _dataMng;
        private IVrManager _vrMng;

        #region "Ctor"

        public OhmSystem(IInterfacesManager interfacesMng, IVrManager vrMng, ILoggerManager loggerMng, IDataManager dataMng)
        {
            _loggerMng = loggerMng;
            _interfacesMng = interfacesMng;
            _dataMng = dataMng;
            _vrMng = vrMng;
        }

        #endregion

        #region "Public Properties"

        public ILoggerManager LoggerMng { get { return _loggerMng; } }

        public IInterfacesManager InterfacesMng { get { return _interfacesMng; } }

        public IDataManager DataMng { get { return _dataMng; } }

        public Common.Vr.IVrManager vrMng
        {
            get { return _vrMng; }
        }

        #endregion

        #region "Public Api"

        public IOhmSystemInstallGateway GetInstallGateway(Plugins.IPlugin plugin)
        {
            return new OhmSystemInstallGateway(this, plugin);
        }

        public IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf)
        {
            return new OhmSystemInterfaceGateway(this, interf);
        }

        public IOhmSystemUnInstallGateway GetUnInstallGateway(Plugins.IPlugin plugin)
        {
            return new OhmSystemUnInstallGateway(this, plugin);
        }

        #endregion       
    }
}
