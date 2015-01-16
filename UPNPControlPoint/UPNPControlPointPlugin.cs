using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPNPControlPoint
{
    public class UPNPControlPointPlugin : PluginBase
    {

        private Guid _id = new Guid("ec3cb3af-23b9-4f45-8e80-66bb869cc0a3");

        private string _name = "UPNP Control Point";

        private OHM.Interfaces.InterfaceAbstract _runningInterface;

        public override Guid Id
        {
            get 
            {
                return _id;
            }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override bool Install(OHM.Sys.IOhmSystemInstallGateway system)
        {

            return system.RegisterInterface("UPNPControlPointInterface");
        }

        public override bool Update()
        {
            return true;
        }

        public override OHM.Interfaces.InterfaceAbstract CreateInterface(string key)
        {
            switch (key)
            {
                case "UPNPControlPointInterface":
                    _runningInterface = new UPNPControlPointInterface();
                    return _runningInterface;
            }
            return null;
        }

        public override bool Uninstall(OHM.Sys.IOhmSystemUnInstallGateway system)
        {
            return system.UnRegisterInterface("UPNPControlPointInterface");
        }
    }
}
