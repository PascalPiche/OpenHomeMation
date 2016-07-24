using OHM.Logger;
using OHM.Nodes;
using OHM.RAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPNPControlPoint
{
    public class UPNPControlPointInterface : RalInterfaceNodeAbstract
    {
        public UPNPControlPointInterface()
            : base("UPNPCtlPointInterface", "UPNP Control Point")
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

        protected override NodeAbstract CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options)
        {
            throw new NotImplementedException();
        }
    }
}
