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
    public class RefreshNodeCommand : ZWaveCommandAbstract
    {

        public RefreshNodeCommand()
            : base("RefreshNodeInfo", "Refresh all Node Info", "")
        {

        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return ZWaveInterface.Manager.RefreshNodeInfo(((IZWaveNode)Node).HomeId.Value, ((IZWaveNode)Node).NodeId.Value);
        }
    }
}
