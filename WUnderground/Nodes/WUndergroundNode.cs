using OHM.Logger;
using OHM.Nodes;
using OHM.RAL;
using System;
using System.Collections.Generic;

namespace WUnderground.Nodes
{
    public abstract class WUndergroundNodeAbstract : ALRAbstractTreeNode
    {
        public WUndergroundNodeAbstract(string keyId, string name)
            : base(keyId, name)
        {

        }
    }
}
