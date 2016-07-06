using OHM.Data;
using OHM.SYS;
using System;
using System.Collections.Generic;

namespace OHM.Plugins
{
    public interface IPluginsManager
    {
        bool Init(IDataStore data);

        IList<IPlugin> InstalledPlugins { get; }

        IList<IPlugin> AvailablesPlugins { get; }

        bool InstallPlugin(Guid id, IOhmSystemInternal system);

        IPlugin GetPlugin(Guid id);

        bool UnInstallPlugin(Guid guid, IOhmSystem ohmSystem);
    }
}
