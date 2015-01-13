using OHM.Logger;
using OHM.Nodes;
using System;

namespace ZWaveLib
{
    [Flags]
    public enum ZWaveControllerState {
        initializing,
        ready,
        error
    }
    class ZWaveController : ZWaveNode, IZWaveController
    {

        //private ZWaveControllerState state = ZWaveControllerState.initializing;
        
        public ZWaveController(string key, string name, ZWaveInterface parent, uint homeId, byte nodeId)
            : base(key, name, parent, homeId, nodeId)
        {

            this.RegisterCommand(new AllOnCommand(this, parent));
            this.RegisterCommand(new AllOffCommand(this, parent));
            this.RegisterCommand(new SoftResetCommand(this, parent));
            this.RegisterCommand(new HardResetCommand(this, parent));
        }
    }
}
