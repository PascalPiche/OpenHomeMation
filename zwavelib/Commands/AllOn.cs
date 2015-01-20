using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib
{
    public class AllOnCommand : CommandAbstract
    {

        private ZWaveInterface _interface;
        private ZWaveNode _node;

        public AllOnCommand(INode node, ZWaveInterface interf)
            : base(node, "allOn", "Switch all on")
        {
            _interface = interf;
            _node = node as ZWaveNode;
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            _interface.Manager.SwitchAllOn(_node.HomeId);
            return true;
        }
    }
}
