using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Nodes
{
    public abstract class NodeInterfaceAbstract : NodeAbstract
    {
        public NodeInterfaceAbstract(string key, string name, ILogger logger)
            : base(key, name, logger)
        { }

    }
}
