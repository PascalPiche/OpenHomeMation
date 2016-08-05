using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class ValNodeAbstract : NodeAbstract
    {

        #region Public Ctor

        protected ValNodeAbstract(string key, string name) 
            : base(key, name)
        {}

        #endregion
    }
}
