using OHM.Interfaces;
using OHM.Logger;
using OHM.Sys;
using System;

namespace OHM.Plugins
{
    public interface IPlugin
    {

        Guid Id { get; }

        String Name { get; }

        PluginStatesEnum State { get; }

        bool Install(IOhmSystemInstallGateway system);

        bool Uninstall(IOhmSystemUnInstallGateway system);

        bool Update();

        InterfaceAbstract CreateInterface(string key, ILogger logger);

    }
}
