using OHM.Data;
using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;

namespace OHM.Managers.Plugins
{
    public interface IPluginsManager
    {
        bool Init(IDataStore data);

        IList<IPlugin> InstalledPlugins { get; }

        IList<IPlugin> AvailablesPlugins { get; }

        bool InstallPlugin(Guid id, IOhmSystemPlugins system);

        IPlugin GetPlugin(Guid id);

        bool UnInstallPlugin(Guid guid, IOhmSystemPlugins ohmSystem);
    }
}
