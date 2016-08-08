using System;

namespace OHM.Plugins
{
    [Serializable]
    public enum PluginStates
    {
        NotFound = -2,
        FatalError = -1,
        Error = 0,
        Warning = 1,
        Ready = 2,
        Normal = 3
    }
}
