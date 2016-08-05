using OHM.RAL;
using OHM.SYS;
using OHM.VAL;
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

        RalInterfaceNodeAbstract CreateInterface(string key);

        IVrType CreateVrNode(string key);

    }
}
