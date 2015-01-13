using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib
{
    public class SoftResetCommand : CommandAbstract
    {
        private ZWaveInterface _interface;
        private ZWaveNode _node;

        public SoftResetCommand(INode node, ZWaveInterface interf)
            : base(node, "SoftReset", "Soft Reset the Z Wave Controller")
        {
            _interface = interf;
            _node = node as ZWaveNode;
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            _interface.SoftReset(_node.HomeId);
            return true;
        }
    }

}
