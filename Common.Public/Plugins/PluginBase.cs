using OHM.Interfaces;
using OHM.Sys;
using System;

namespace OHM.Plugins
{
    [Serializable]
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {

        public abstract Guid Id { get; }

        public abstract string Name { get; }

        public abstract bool Install(IOhmSystemInstallGateway system);

        public abstract bool Uninstall();

        public abstract bool Update();

        public abstract InterfaceAbstract CreateInterface(string key);

    }
}
