using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib.Data
{
    public interface IZWaveDriverControlerNode : ITreeNode
    {
        uint? HomeId { get; }
    }

    public interface IZWaveNode : IZWaveDriverControlerNode
    {
        byte? NodeId { get; }
    }
}
