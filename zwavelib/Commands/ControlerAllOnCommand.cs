using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Data;

namespace ZWaveLib.Commands
{
    public class ControlerAllOnCommand : ZWaveCommandAbstract
    {
        public ControlerAllOnCommand(IZWaveNode node)
            : base(node, "allOn", "Switch all on", "")
        {

        }

        protected override bool RunImplementation(Dictionary<string, string> arguments)
        {
            ZWaveInterface.Manager.SwitchAllOn(((IZWaveNode)Node).HomeId.Value);
            return true;
        }
    }
}
