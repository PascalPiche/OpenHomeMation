﻿using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Data;

namespace ZWaveLib.Commands
{

    public class ControlerAllOffCommand : ZWaveCommandAbstract
    {

        public ControlerAllOffCommand(IZWaveNode node)
            : base(node, "allOff", "Switch all off", "")
        {
            
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            ZWaveInterface.Manager.SwitchAllOff(((IZWaveNode)Node).HomeId.Value);
            return true;
        }
    }
}