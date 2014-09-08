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
        public ZWaveNode(string key, string name, ILogger logger, INode parent)
            : base(key, name, logger, parent)
        {

        }

        internal void UpdateName(string name) 
        {
            this.Name = name;
        }
    }
}
