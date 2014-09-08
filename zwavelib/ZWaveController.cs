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
        
        public ZWaveController(string key, string name, ILogger logger, INode parent) : base(key, name, logger, parent)
        {

        }
    }
}
