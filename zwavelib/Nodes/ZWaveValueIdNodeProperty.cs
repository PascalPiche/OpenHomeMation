using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib.Data
{
    public class ZWaveValueIdNodeProperty : NodeProperty
    {
        public ZWaveValueIdNodeProperty(string key, string name) : base(key, name, typeof(OpenZWaveDotNet.ZWValueID))
        {

        }

        internal bool InternalSetValue()
        {
            
            return false;
        }

    }
}
