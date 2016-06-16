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
    public abstract class ZWaveCommandAbstract : CommandAbstract, IInterfaceCommand
    {

        protected new ZWaveNode Node
        {
            get { return (ZWaveNode)base.Node; }
        }

        protected new ZWaveInterface Interface
        {
            get { return (ZWaveInterface)base.Interface; }
        }

        public ZWaveCommandAbstract(INode node, string key, string name, string description) 
            : base(node, key, name, description, null)
        {
            
        }
    }
}
