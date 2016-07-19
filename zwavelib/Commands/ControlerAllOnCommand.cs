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
        public ControlerAllOnCommand()
            : base("allOn", "Switch all on", "")
        {

        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            ZWaveInterface.Manager.SwitchAllOn(((IZWaveNode)Node).HomeId.Value);
            return true;
        }
    }
}
