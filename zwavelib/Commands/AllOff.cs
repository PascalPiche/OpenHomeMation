using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib
{
    
    public class AllOffCommand : CommandAbstract
    {

        private ZWaveInterface _interface;
        private ZWaveNode _node;

        public AllOffCommand(INode node, ZWaveInterface interf)
            : base(node, "allOff", "Switch all off")
        {
            _interface = interf;
            _node = node as ZWaveNode;
        }


        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            _interface.Manager.SwitchAllOff(_node.HomeId);
            return true;
        }
    }
}
