using OHM.Logger;
using OHM.RAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPNPControlPoint
{
    public class UPNPControlPointInterface : InterfaceAbstract
    {
        public UPNPControlPointInterface(ILogger logger)
            : base("UPNPCtlPointInterface", "UPNP Control Point", logger)
        {
            //Create Commands
            //this.RegisterCommand(new CreateControllerCommand(this));
        }

        protected override void Start()
        {
            var t = new UPNPLib.UPnPDeviceFinder();
            t.StartAsyncFind(t.CreateAsyncFind(".", 0, null));
        }

        protected override void Shutdown()
        {
            
        }

    }
}
