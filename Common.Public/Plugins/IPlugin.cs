using OHM.Interfaces;
using OHM.Logger;
using OHM.System;
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

        IInterface CreateInterface(string key, ILogger logger);

    }
}
