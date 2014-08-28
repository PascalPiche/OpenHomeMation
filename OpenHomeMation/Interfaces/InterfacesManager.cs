
using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using System;

namespace OHM.Interfaces
{
    public class InterfacesManager : IInterfacesManager
    {

        private ILoggerManager _loggerMng;
        private IPluginsManager _pluginsMng;
        private IDataStore _data;
        private IDataDictionary _dataRegisteredInterfaces;


        public InterfacesManager(ILoggerManager loggerMng, IPluginsManager pluginsMng)
        {
            _loggerMng = loggerMng;
            _pluginsMng = pluginsMng;
        }

        public bool Init(IDataStore data)
        {
            _data = data;
            _dataRegisteredInterfaces = _data.GetDataDictionary("RegisteredInterfaces");
            if (_dataRegisteredInterfaces == null)
            {
                _dataRegisteredInterfaces = new DataDictionary();
                _data.StoreDataDictionary("RegisteredInterfaces", _dataRegisteredInterfaces);
                _data.Save();
            }
            loadRegisteredInterfaces();
            return true;
        }

        public bool RegisterInterface(string key, IPlugin plugin)
        {
            //Detect if already created
            IDataDictionary _interfaceData = _dataRegisteredInterfaces.GetDataDictionary(key);
            if (_interfaceData == null)
            {
                _interfaceData = new DataDictionary();
                _interfaceData.StoreString("PluginId", plugin.Id.ToString());
                _dataRegisteredInterfaces.StoreDataDictionary(key, _interfaceData);

                IInterface newInterface = CreateAndLaunchInterface(key);


                
                return _data.Save();
            }
            else
            {
                //Interface already registered ???

            }
            return false;
        }

        private void loadRegisteredInterfaces()
        {
            foreach (var key in _dataRegisteredInterfaces.GetKeys())
            {
                CreateAndLaunchInterface(key);
            }
       }

        private IInterface CreateAndLaunchInterface(string key)
        {
            IDataDictionary _dataPlugin = _dataRegisteredInterfaces.GetDataDictionary(key);
            String id = _dataPlugin.GetString("PluginId");
            if (id == "")
            {
                //Interface not found, abording
                return null;
            }
           
            IPlugin plugin = _pluginsMng.GetPlugin(new Guid(id));
            IInterface interf = plugin.CreateInterface(key, _loggerMng.GetLogger(plugin.Name + '.' + key));

            interf.Init();
            

            return interf;
        }
    }
}
