using System.Collections.Generic;

namespace ZWaveLib.Commands
{
    public class RefreshNodeValueCommand : ZWaveCommandAbstract
    {

        public RefreshNodeValueCommand()
            : base("RefreshNodeValue", "Refresh all node value", "")
        {

        }

        public override bool CanExecute()
        {
            return false;
        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return ZWaveInterface.Manager.RefreshNodeInfo(uint.Parse(arguments["homeId"]),byte.Parse(arguments["nodeId"]));
            //return ZWaveInterface.Manager.RefreshValue(_node.HomeId, _node.NodeId);
        }
    }
}
