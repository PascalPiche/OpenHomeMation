using System.Collections.Generic;
using ZWaveLib.Nodes;

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
