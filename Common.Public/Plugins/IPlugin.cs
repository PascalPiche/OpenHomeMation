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

        bool Install(IOhmSystemInstallGateway system);

        bool Uninstall();

        bool Update();

        InterfaceAbstract CreateInterface(string key, ILogger logger);

    }
}
