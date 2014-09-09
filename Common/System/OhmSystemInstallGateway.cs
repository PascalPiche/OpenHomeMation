using OHM.Interfaces;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Sys
{

    
    public class OhmSystemInstallGateway : IOhmSystemInstallGateway
    {

        private OhmSystem _system;
        private IPlugin _plugin;

        
        public OhmSystemInstallGateway(OhmSystem system, IPlugin plugin)
        {
            _system = system;
            _plugin = plugin;
        }

        public Logger.ILogger Logger
        {
            get { return _system.LoggerMng.GetLogger(_plugin.Name); }
        }

        public bool RegisterInterface(string key)
        {
            return _system.InterfacesMng.RegisterInterface(key, _plugin, _system);
        }

        /*public void RegisterNodeType(string key, string type)
        {
            throw new NotImplementedException();
        }*/
    }

    public class OhmSystemInterfaceGateway : IOhmSystemInterfaceGateway
    {

        private OhmSystem _system;
        private IInterface _interface;


        public OhmSystemInterfaceGateway(OhmSystem system, IInterface interf)
        {
            _system = system;
            _interface = interf;
        }

        public bool CreateNode(Nodes.INode node)
        {
            throw new NotImplementedException();
        }

        public bool RemoveNode(Nodes.INode node)
        {
            throw new NotImplementedException();
        }
    }
}
