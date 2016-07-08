using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Data;

namespace ZWaveLib.Commands
{
    public class ControlerAddNodeCommand : ZWaveCommandAbstract
    {
        public ControlerAddNodeCommand(IZWaveNode node)
            : base(node, "AddNode", "Add node", "")
        {

        }

        protected override bool RunImplementation(Dictionary<string, string> arguments)
        {
            return ZWaveInterface.Manager.AddNode(((IZWaveNode)Node).HomeId.Value, false);
        }
    }
}
