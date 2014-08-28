using OHM.Data;
using OHM.System;
using System;
using System.Collections.Generic;


namespace OHM.Plugins
{
    public interface IPluginsManager
    {

        bool Init(IDataStore data);

        IList<IPlugin> InstalledPlugins { get; }

        IList<IPlugin> AvailablesPlugins { get; }

        bool InstallPlugin(Guid id, IOhmSystem system);

        IPlugin GetPlugin(Guid id);
    }
}
