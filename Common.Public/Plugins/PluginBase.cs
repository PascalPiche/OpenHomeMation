using OHM.System;
using System;

namespace OHM.Plugins
{
    [Serializable]
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {

        public abstract Guid Id { get; }

        public abstract string Name { get; }

        public abstract Version Version { get; }

        public abstract bool Install(IOhmSystemInstallGateway system);

        public abstract bool Uninstall();

        public abstract bool Update();

    }
}
