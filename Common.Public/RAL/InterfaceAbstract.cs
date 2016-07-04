using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.SYS;
using System.Collections.Generic;

namespace OHM.RAL
{
    public abstract class InterfaceAbstract : NodeInterfaceAbstract
    {

        #region Public Ctor

        public InterfaceAbstract(string key, string name, ILogger logger) 
            : base(key, name, logger)
        {}

        #endregion
    }
}
