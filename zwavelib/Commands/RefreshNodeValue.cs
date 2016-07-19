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
    public class RefreshNodeValueCommand : ZWaveCommandAbstract
    {

        public RefreshNodeValueCommand()
            : base("RefreshNodeValue", "Refresh all node value", "")
        {

        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {

            return false;
            //return ZWaveInterface.Manager.RefreshValue(_node.HomeId, _node.NodeId);
        }
    }
}
