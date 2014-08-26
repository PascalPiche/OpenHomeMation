using OHM.System;
using System;

namespace OHM.Plugins
{
    public interface IPlugin
    {

        Guid Id { get; }

        String Name { get; }

        Version Version { get; }

        bool Install(IOhmSystemInstallGateway system);

        bool Uninstall();

        bool Update();

    }
}
