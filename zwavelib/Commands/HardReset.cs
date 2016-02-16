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
    public class HardResetCommand : ZWaveCommandAbstract
    {
        
        

        public HardResetCommand(INode node)
            : base(node, "HardReset", "Hard Reset the Z Wave Controller (Warning: Will erase all data in the controller, pairing will need to be done again after the hard reset)", "")
        {

        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            Interface.Manager.ResetController(Node.HomeId);
            return true;
        }
    }

}
