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

    public class ControlerAllOffCommand : ZWaveCommandAbstract
    {

        public ControlerAllOffCommand(INode node)
            : base(node, "allOff", "Switch all off", "")
        {
            
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            Interface.Manager.SwitchAllOff(Node.HomeId.Value);
            return true;
        }
    }
}
