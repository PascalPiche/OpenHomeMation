using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUnderground.Data
{
    public abstract class WUndergroundNodeAbstract : NodeAbstract
    {
        public WUndergroundNodeAbstract(string keyId, string name, ILogger logger)
            : base(keyId, name, logger)
        {

        }
    }
}
