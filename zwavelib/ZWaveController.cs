using System;

namespace ZWaveLib
{
    [Flags]
    public enum ZWaveControllerState {
        initializing,
        ready,
        error
    }
    class ZWaveController : IZWaveController
    {

        //private ZWaveControllerState state = ZWaveControllerState.initializing;
        
        public ZWaveController()
        {

        }
    }
}
