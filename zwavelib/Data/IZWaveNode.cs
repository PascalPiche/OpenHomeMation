using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib.Data
{
    public interface IZWaveNode : INode
    {

        uint HomeId { get; }

        byte NodeId { get; }

    }
}
