using OHM.Data;
using OHM.Logger;
using OHM.Nodes.ALV;
using OHM.Plugins;
using OHM.SYS;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.VAL
{
    public class VrManager : IVrManager
    {
        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private IDataStore _data;
        private IPluginsManager _pluginsMng;
        private IDictionary<string, IPlugin> _registeredType = new Dictionary<string, IPlugin>();
        private ICollection<IVrType> _rootNodes = new ObservableCollection<IVrType>();

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
            //Launch Main Vr Node
            

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

        public bool RegisterVrType(string key, IPlugin plugin)
        {
            bool result = false;
            if (!_registeredType.ContainsKey(key))
            {
                _registeredType.Add(key, plugin);
                result = true;
            }
            return result;
        }

        public IList<string> GetAllowedSubVrType(IVrType parentType)
        {
            if (parentType == null)
            {
                return (IList<string>)_registeredType.Keys;
            }
            else
            {
                return parentType.GetAllowedSubVrType();
            }
        }

        public bool CreateRootNode(string vrType, string key, string name)
        {
            bool result = false;

            //Find type
            if (_registeredType.ContainsKey(vrType))
            {
                //Create Node
                IVrType newNode = _registeredType[vrType].CreateVrNode(vrType);

                //Init type

                //_rootNodes 

                //Register node

            }
            
            return result;
        }

        #endregion
    }
}
