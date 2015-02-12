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
    public class RefreshNodeValueCommand : CommandAbstract
    {

        private ZWaveInterface _interface;
        private ZWaveNode _node;

        public RefreshNodeValueCommand(INode node, ZWaveInterface interf)
            : base(node, "RefreshNodeValue", "Refresh all node value")
        {
            _interface = interf;
            _node = node as ZWaveNode;
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            return false;
            //return _interface.Manager.RefreshValue()
            //return _interface.Manager.RefreshValue(_node.HomeId, _node.NodeId);
        }
    }
}
