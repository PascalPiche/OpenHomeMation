using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    [Serializable]
    public enum PluginStates
    {
        NotFound = -2,
        FatalError = -1,
        Error = 0,
        Warning = 1,
        Normal = 2
    }
}
