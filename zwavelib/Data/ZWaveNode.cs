using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib
{
    class ZWaveNode : NodeAbstract, IZWaveNode
    {

        private uint _homeId;
        private byte _nodeId;

        public uint HomeId
        {
            get { return _homeId; }
        }

        public byte NodeId
        {
            get { return _nodeId; }
        }

        public ZWaveNode(string key, string name, INode parent, uint homeId, byte nodeId)
            : base(key, name, parent)
        {
            _homeId = homeId;
            _nodeId = nodeId;
        }

        internal void UpdateName(string name) 
        {
            this.Name = name;
        }

       
    }
}
