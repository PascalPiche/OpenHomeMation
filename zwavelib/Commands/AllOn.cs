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
    public class AllOnCommand : ZWaveCommandAbstract
    {

        public AllOnCommand(INode node)
            : base(node, "allOn", "Switch all on", "")
        {

        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            Interface.Manager.SwitchAllOn(Node.HomeId);
            return true;
        }
    }
}
