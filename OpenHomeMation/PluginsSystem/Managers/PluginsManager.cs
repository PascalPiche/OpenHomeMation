using log4net;
using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace OHM.Managers.Plugins
{

    /// <summary>
    /// Core Plugins Manager
    /// </summary>
    public sealed class PluginsManager : IPluginsManager
    {
        #region Private members

        /// <summary>
        /// Member instance of the Logger manager
        /// </summary>
        private ILoggerManager _loggerMng;

        /// <summary>
        /// Member instance of the logger
        /// </summary>
        private ILog _logger;

        /// <summary>
        /// Member instance of inner the data store for the manager
        /// </summary>
        private IDataStore _data;

        /// <summary>
        /// Dictionnary of installed and active plugins by key
        /// </summary>
        private IDataDictionary _dataInstalledPlugins;

        /// <summary>
        /// Local filepath
        /// </summary>
        private string _filePath;

        /// <summary>
        /// List of availables plugins not installed
        /// </summary>
        private IList<IPlugin> _availablesPlugins = new ObservableCollection<IPlugin>();

        /// <summary>
        /// List of installed plugins
        /// </summary>
        private IList<IPlugin> _installedPluginsInstance = new ObservableCollection<IPlugin>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Type _pluginBaseType = typeof(PluginBase);

        #endregion

        #region Public Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerMng"></param>
        /// <param name="filePath"></param>
        public PluginsManager(ILoggerManager loggerMng, string filePath)
        {
            //Store logger
            _loggerMng = loggerMng;

            //Store file path
            _filePath = filePath;

            //Attach assembly resolve event
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public IList<IPlugin> AvailablesPlugins { get { return _availablesPlugins; } }

        /// <summary>
        /// 
        /// </summary>
        public IList<IPlugin> InstalledPlugins { get { return _installedPluginsInstance; } }

        #endregion

        #region Public Api

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Init(IDataStore data)
        {
            //Spawn internal logger
            this._logger = _loggerMng.GetLogger("PluginsManager");
            _logger.Debug("Initing");
            //Store internal reference for futur uses
            _data = data;

            //Create or get Dictionnary for installed plugins
            _dataInstalledPlugins = _data.GetOrCreateDataDictionary("InstalledPlugins");
            //_data.StoreDataDictionary("InstalledPlugins", _dataInstalledPlugins);
            _data.Save();

            InitPluginsList();

            LoadRegisteredPlugins();

            _logger.Info("Inited");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public bool InstallPlugin(Guid id, IOhmSystemPlugins system)
        {
            IPlugin plugin = FindPluginIn(id, _availablesPlugins);
            bool result = false;
            if (plugin != null)
            {
                result = InstallPlugin(plugin, system.GetInstallGateway(plugin));
            }
            else
            {
                //Plugin not found 
                _logger.Warn("Cannot install Plugin " + id + ": Plugin not found");
            }

            return result;
        }

        /// <summary>
        /// Uninstall the plugin by Guid provided
        /// </summary>
        /// <param name="id">Guid of the plugin to uninstall</param>
        /// <param name="system"></param>
        /// <returns>True if the Uninstall was succeed; False otherwise</returns>
        public bool UnInstallPlugin(Guid id, IOhmSystemPlugins system)
        {
            IPlugin plugin = FindPluginIn(id, _installedPluginsInstance);
            bool result = false;
            if (plugin != null)
            {
                result = UnInstallPlugin(plugin, system.GetUnInstallGateway(plugin));
            }
            else
            {
                _logger.Warn("Cannot uninstal Plugin " + id + ": Plugin not found");
            }

            return result;
        }

        /// <summary>
        /// Return the plugin instance by Guid if found
        /// </summary>
        /// <param name="id">Guid of the plugin requested</param>
        /// <returns>nstance of the IPlugin find by the Guid requested or Null</returns>
        public IPlugin GetPlugin(Guid id)
        {
            return FindPluginIn(id, _installedPluginsInstance);
        }

        #endregion 

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        private bool InstallPlugin(IPlugin plugin, IOhmSystemInstallGateway system)
        {
            bool result = false;
            try
            {
                result = plugin.Install(system);
            }
            catch (Exception ex)
            {
                _logger.Error("Install failed for plugin : " + plugin.Name, ex);
                return false;
            }
            
            _installedPluginsInstance.Add(plugin);

            //Save to persistent storage
            //Create object to store Version and name
            IDataDictionary newInstalledPluginRegisteringDict = _dataInstalledPlugins.GetOrCreateDataDictionary(plugin.Id.ToString());
            newInstalledPluginRegisteringDict.StoreString("name", plugin.Name);
            newInstalledPluginRegisteringDict.StoreString("version", plugin.GetType().Assembly.GetName().Version.ToString());
            _data.Save();
            _availablesPlugins.Remove(plugin);
            
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        private bool UnInstallPlugin(IPlugin plugin, IOhmSystemUnInstallGateway system)
        {
            bool result = false;
            try
            {
                result = plugin.Uninstall(system);
            }
            catch (Exception ex)
            {
                _logger.Error("UnInstall failed for plugin : " + plugin.Name, ex);
                return false;
            }

            _installedPluginsInstance.Remove(plugin);
            //Save to persistent storage
            _dataInstalledPlugins.RemoveKey(plugin.Id.ToString());
            _data.Save();
            _availablesPlugins.Add(plugin);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private IPlugin FindPluginIn(Guid id, IList<IPlugin> source) {

            IPlugin result = null;
            
            foreach (var item in source)
            {
                if (item.Id == id)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitPluginsList()
        {
            if (!Directory.Exists(_filePath))
            {
                this._logger.Warn("Plugins directory not found at : " + _filePath);
                return;
            }

            foreach (var file in Directory.GetFiles(_filePath, "*.dll", SearchOption.AllDirectories))
            {
                this._logger.Debug("DLL Found while loading Plugins:" + file);

                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    
                    foreach (var type in assembly.GetExportedTypes())
                    {
                        try
                        {
                            if (IsPlugin(type))
                            {
                                this._logger.Debug("Plugin Found:" + type.FullName);
                                IPlugin plugin = (PluginBase)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(assembly.FullName, type.FullName);
                                _availablesPlugins.Add(new PluginObservableAdapter(plugin));
                                this._logger.Info("Plugin " + type.FullName + " added to available plugins");
                            }
                        }
                        catch (Exception ex)
                        {
                            this._logger.Error("Cannot instantiate plugin type : " + type.ToString() + " : In file : " + file, ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._logger.Error("Cannot load Assembly file : " + file, ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly result = null;

            foreach (var file in Directory.GetFiles(_filePath, "*.dll", SearchOption.AllDirectories))
            {
                var assembly = Assembly.LoadFrom(file);
                if (assembly.FullName == args.Name)
                {
                    result = assembly;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Check if base type is a PluginBaseType
        /// </summary>
        /// <param name="type">The type to valid</param>
        /// <returns>True if the type is a PluginBaseType; False otherwise</returns>
        private bool IsPlugin(Type type)
        {
            return _pluginBaseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Load all registered plugins
        /// </summary>
        private void LoadRegisteredPlugins() {
            foreach (string item in _dataInstalledPlugins.Keys)
            {
                var plugin = FindPluginIn(new Guid(item), _availablesPlugins);
                if (plugin != null)
                {
                    _logger.Info("Registered plugin found : " + plugin.Name);
                    _installedPluginsInstance.Add(plugin);
                    _availablesPlugins.Remove(plugin);
                }
                else
                {
                    //Get Info from the bd to track missing plugin
                    IDataDictionary pluginData = _dataInstalledPlugins.GetOrCreateDataDictionary(item);
                    _logger.Warn("Registered plugin " + item + "not found");
                    plugin = new NotFoundPlugin(item, pluginData.GetString("name"));
                    _installedPluginsInstance.Add(plugin);
                }
            }
        }

        #endregion
    }
}
