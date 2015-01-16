using OHM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPNPControlPoint
{
    public class UPNPControlPointInterface : InterfaceAbstract
    {

        public UPNPControlPointInterface()
            : base("UPNPCtlPointInterface", "UPNP Control Point")
        {
            //Create Commands
            //this.RegisterCommand(new CreateControllerCommand(this));
        }

        public override void Start()
        {
            var t = new UPNPLib.UPnPDeviceFinder();
            t.StartAsyncFind(t.CreateAsyncFind(".", 0, null));
            base.Start();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

    }
}
