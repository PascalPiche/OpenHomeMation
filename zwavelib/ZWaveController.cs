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
        
        public ZWaveController(string key, string name, INode parent, uint homeId, byte nodeId)
            : base(key, name, parent, homeId, nodeId)
        {

        }
    }
}
