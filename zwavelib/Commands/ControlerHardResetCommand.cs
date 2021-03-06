﻿using System.Collections.Generic;
using ZWaveLib.Nodes;

namespace ZWaveLib.Commands
{
    public class ControlerHardResetCommand : ZWaveCommandAbstract
    {
        public ControlerHardResetCommand()
            : base("HardReset", "Hard Reset the Z Wave Controller (Warning: Will erase all data in the controller, pairing will need to be done again after the hard reset)", "")
        { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            ZWaveInterface.Manager.ResetController(((IZWaveHomeNode)Node).HomeId.Value);
            return true;
        }
    }
}
