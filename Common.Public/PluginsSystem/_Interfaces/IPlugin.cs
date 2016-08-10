﻿using OHM.Nodes.ALR;
using OHM.Nodes.ALV;
using OHM.SYS;
using System;

namespace OHM.Plugins
{
    public interface IPlugin
    {
        Guid Id { get; }

        string Name { get; }

        PluginStates State { get; }

        bool Install(IOhmSystemInstallGateway system);

        bool Uninstall(IOhmSystemUnInstallGateway system);

        bool Update(IOhmSystemInstallGateway system);

        ALRInterfaceAbstractNode CreateInterface(string key);

        IVrType CreateVrNode(string key);
    }
}
