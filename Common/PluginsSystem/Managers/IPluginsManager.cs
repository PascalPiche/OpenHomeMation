using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;

namespace OHM.Managers.Plugins
{
    /// <summary>
    /// Core Interface for a Plugins Manager
    /// </summary>
    public interface IPluginsManager
    {

        /// <summary>
        /// Init the plugins manager
        /// </summary>
        /// <param name="loggerMng">Logger Manager</param>
        /// <param name="data">DataStore to save info</param>
        /// <returns>True when the initialization was a success</returns>
        bool Init(ILoggerManager loggerMng, IDataStore data);

        /// <summary>
        /// List of installed plugins
        /// </summary>
        IList<IPlugin> InstalledPlugins { get; }

        /// <summary>
        /// List of available plugins for installation
        /// </summary>
        IList<IPlugin> AvailablesPlugins { get; }

        /// <summary>
        /// Install a plugins
        /// </summary>
        /// <param name="id">Guid of the plugins to install</param>
        /// <param name="system">System interface for the plugin</param>
        /// <returns>True when the plugin install was a success</returns>
        bool InstallPlugin(Guid id, IOhmSystemPlugins system);

        /// <summary>
        /// Get the plugin object by Guid
        /// </summary>
        /// <param name="id">The Guid of the plugin to get</param>
        /// <returns>The instance or null of the requested Guid plugin</returns>
        IPlugin GetPlugin(Guid id);

        /// <summary>
        /// Uninstall the plugin by Guid
        /// </summary>
        /// <param name="id">The guid of the plugin to uninstall</param>
        /// <param name="ohmSystem">System interface for the plugins</param>
        /// <returns>True when the uninstall was a success, otherwise false </returns>
        bool UnInstallPlugin(Guid id, IOhmSystemPlugins ohmSystem);
    }
}
