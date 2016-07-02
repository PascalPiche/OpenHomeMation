using OHM.Logger;
using OHM.RAL;
using OHM.SYS;
using System;

namespace OHM.Plugins
{
    public interface IPlugin
    {

        Guid Id { get; }

        String Name { get; }

        PluginStates State { get; }

        bool Install(IOhmSystemInstallGateway system);

        bool Uninstall(IOhmSystemUnInstallGateway system);

        bool Update();

        InterfaceAbstract CreateInterface(string key, ILogger logger);

    }
}
