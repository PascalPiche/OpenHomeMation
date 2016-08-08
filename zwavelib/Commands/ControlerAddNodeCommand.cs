using System.Collections.Generic;
using ZWaveLib.Nodes;

namespace ZWaveLib.Commands
{
    public class ControlerAddNodeCommand : ZWaveCommandAbstract
    {
        public ControlerAddNodeCommand()
            : base("AddNode", "Add node")
        { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return ZWaveInterface.Manager.AddNode(((IZWaveHomeNode)Node).HomeId.Value, false);
        }
    }
}
