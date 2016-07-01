using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib.Commands
{
    public class ControlerAddNodeCommand : ZWaveCommandAbstract
    {
        public ControlerAddNodeCommand(INode node)
            : base(node, "AddNode", "Add node", "")
        {

        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            return Interface.Manager.AddNode(Node.HomeId.Value, false);
        }
    }
}
