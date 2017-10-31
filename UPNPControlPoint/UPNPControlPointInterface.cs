using OHM.Nodes;
using OHM.Nodes.ALR;
using System;
using System.Collections.Generic;

namespace UPNPControlPoint
{
    public class UPNPControlPointInterface : ALRInterfaceAbstractNode
    {
        public UPNPControlPointInterface()
            : base("UPNPCtlPointInterface", "UPNP Control Point")
        { }

        protected override bool Start()
        {
            var t = new UPNPLib.UPnPDeviceFinder();
            t.StartAsyncFind(t.CreateAsyncFind(".", 0, null));
            return true;
        }

        protected override bool Shutdown()
        {
            return true;
        }

        protected override AbstractPowerNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options)
        {
            throw new NotImplementedException();
        }

        protected override void RegisterCommands()
        {
            //Create Commands
            //this.RegisterCommand(new CreateControllerCommand(this));
        }

        protected override bool RegisterProperties()
        {
            return true;
        }
    }
}
