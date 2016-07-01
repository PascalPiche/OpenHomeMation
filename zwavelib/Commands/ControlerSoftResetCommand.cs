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
    public class ControlerSoftResetCommand : ZWaveCommandAbstract
    {
        public ControlerSoftResetCommand(INode node)
            : base(node, "SoftReset", "Soft Reset the Z Wave Controller", "")
        {
            
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            Interface.Manager.SoftReset(Node.HomeId.Value);
            return true;
        }
    }

}
