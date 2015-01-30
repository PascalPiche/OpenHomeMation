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
    public class HardResetCommand : CommandAbstract
    {
        private ZWaveInterface _interface;
        private ZWaveNode _node;

        public HardResetCommand(INode node, ZWaveInterface interf)
            : base(node, "HardReset", "Hard Reset the Z Wave Controller (Warning: Will erase all data in the controller, pairing will need to be done again after the hard reset)")
        {
            _interface = interf;
            _node = node as ZWaveNode;
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            _interface.Manager.ResetController(_node.HomeId);
            return true;
        }
    }

}
