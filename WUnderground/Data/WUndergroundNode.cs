using OHM.Logger;
using OHM.Nodes;
using OHM.RAL;
using System;
using System.Collections.Generic;

namespace WUnderground.Data
{
    public abstract class WUndergroundNodeAbstract : ALRAbstractNode
    {
        public WUndergroundNodeAbstract(string keyId, string name)
            : base(keyId, name)
        {

        }
    }
}
