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
    public class RefreshNodeCommand : CommandAbstract
    {

        private ZWaveInterface _interface;
        private ZWaveNode _node;

        public RefreshNodeCommand(INode node, ZWaveInterface interf)
            : base(node, "RefreshNodeInfo", "Refresh all Node Info")
        {
            _interface = interf;
            _node = node as ZWaveNode;
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            return _interface.Manager.RefreshNodeInfo(_node.HomeId, _node.NodeId);
        }
    }
}
