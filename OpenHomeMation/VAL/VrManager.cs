using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.SYS;
using System;

namespace OHM.VAL
{
    public class VrManager : IVrManager
    {

        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private IDataStore _data;
        private IPluginsManager _pluginsMng;

        #region Public Property

        #endregion

        #region Ctor

        public VrManager(ILoggerManager loggerMng, IPluginsManager pluginsMng)
        {
            _loggerMng = loggerMng;
            _pluginsMng = pluginsMng;
        }

        #endregion

        #region Public API

        public bool Init(IDataStore data, IOhmSystemInternal system)
        {
            _data = data;
            _logger = _loggerMng.GetLogger("InterfacesManager");
           /* _dataRegisteredInterfaces = _data.GetDataDictionary("RegisteredInterfaces");
            if (_dataRegisteredInterfaces == null)
            {
                _dataRegisteredInterfaces = new DataDictionary();
                _data.StoreDataDictionary("RegisteredInterfaces", _dataRegisteredInterfaces);
                _data.Save();
            }
            loadRegisteredInterfaces(system);*/
            return true;
        }

        #endregion

        public bool RegisterVrType(string key, IVrType vrType)
        {
            throw new NotImplementedException();
        }
    }
}
