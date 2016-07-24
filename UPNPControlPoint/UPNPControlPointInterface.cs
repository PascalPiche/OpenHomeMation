using OHM.Nodes;
using OHM.RAL;
using System;
using System.Collections.Generic;

namespace UPNPControlPoint
{
    public class UPNPControlPointInterface : RalInterfaceNodeAbstract
    {
        public UPNPControlPointInterface()
            : base("UPNPCtlPointInterface", "UPNP Control Point")
        {
            
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

        protected override void RegisterCommands()
        {
            //Create Commands
            //this.RegisterCommand(new CreateControllerCommand(this));
        }

        protected override void RegisterProperties()
        {
            
        }
    }
}
