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
        public ControlerAddNodeCommand()
            : base("AddNode", "Add node")
        {

        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return ZWaveInterface.Manager.AddNode(((IZWaveHomeNode)Node).HomeId.Value, false);
        }
    }
}
