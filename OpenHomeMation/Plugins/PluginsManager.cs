using OHM.Data;
using OHM.Logger;
using OHM.System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Policy;

namespace OHM.Plugins
{
    public class PluginsManager : IPluginsManager
    {

        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private IDataStore _data;
        private IDataDictionary _dataInstalledPlugins;
        private string _filePath;

        private IList<IPlugin> _availablesPlugins = new ObservableCollection<IPlugin>();
        private IList<IPlugin> _installedPluginsInstance = new ObservableCollection<IPlugin>();
        private readonly Type _pluginBaseType = typeof(PluginBase);

        public PluginsManager(ILoggerManager loggerMng, string filePath)
        {
            _loggerMng = loggerMng;
            _filePath = filePath;
        }

        public IList<IPlugin> AvailablesPlugins
        {
            get {
                return _availablesPlugins;
            }
        }

        public IList<IPlugin> InstalledPlugins
        {
            get
            {
                return _installedPluginsInstance;
            }
        }

        public bool Init(IDataStore data)
        {
            this._logger = _loggerMng.GetLogger("PluginsManager");
            _data = data;
            _dataInstalledPlugins = _data.GetDataDictionary("InstalledPlugins");
            if (_dataInstalledPlugins == null)
            {
                _dataInstalledPlugins = new DataDictionary();
                _data.StoreDataDictionary("InstalledPlugins", _dataInstalledPlugins);
            }

            InitPluginsList();
            LoadRegisteredPlugins();
            return true;
        }

        public bool InstallPlugin(Guid id, IOhmSystem system)
        {
            IPlugin plugin = FindPluginIn(id, _availablesPlugins);
            
            if (plugin != null)
            {

                return InstallPlugin(plugin, system.GetInstallGateway(plugin));
            }
            else
            {
                //todo log
                //Plugin not found 
            }
            return false;
        }

        public IPlugin GetPlugin(Guid id)
        {
            return FindPluginIn(id, _installedPluginsInstance);
        }

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
            _dataInstalledPlugins.StoreString(plugin.Id.ToString(), plugin.GetType().Assembly.GetName().Version.ToString());
            _data.Save();

            try
            {
                _availablesPlugins.Remove(plugin);
            }
            catch (Exception ex)
            {
                //Weird error related to WPF 
                //Do nothing for the moment.. need investigation later
                //but work if we skip the error
                _logger.Debug("Weird error on removing plugin from availables plugins list", ex);
            }
            
            return result;
        }

        private IPlugin FindPluginIn(Guid id, IList<IPlugin> source) {
            //Find plugins
            foreach (var item in AvailablesPlugins)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

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

                        if (IsPlugin(type))
                        {
                            this._logger.Info("Plugin Found:" + type.FullName);

                            var domain = CreateSandboxDomain("Sandbox Domain For " + type.FullName, Path.GetDirectoryName(assembly.Location), SecurityZone.Internet, typeof(PluginsManager));
                            IPlugin plugin = (PluginBase)domain.CreateInstanceAndUnwrap(assembly.FullName, type.FullName);

                            _availablesPlugins.Add(plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._logger.Error("Cannot instantiate plugin : " + file, ex);
                }
            }
        }

        private bool IsPlugin(Type type)
        {
            return _pluginBaseType.IsAssignableFrom(type) && !type.Equals(_pluginBaseType);
        }

        private void LoadRegisteredPlugins() {
            foreach (string item in _dataInstalledPlugins.GetKeys())
            {
                var plugin = FindPluginIn(new Guid(item), _availablesPlugins);
                if (plugin != null)
                {
                    _installedPluginsInstance.Add(plugin);
                    try
                    {
                        _availablesPlugins.Remove(plugin);
                    }
                    catch (Exception ex)
                    {
                        //Weird error related to WPF 
                        //Do nothing for the moment.. need investigation later
                        //but work if we skip the error
                        _logger.Debug("Weird error on removing plugin from availables plugins list", ex);
                    }
                }
                else
                {
                    //Track plugin status?, code base not found for registered plugins

                }
            }
        }

        private AppDomain CreateSandboxDomain(string name, string path, SecurityZone zone, Type item)
        {
            var setup = new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory, PrivateBinPath = Path.GetFullPath(path) };

            var evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(zone));
            var permissions = SecurityManager.GetStandardSandbox(evidence);

            StrongName strongName = item.Assembly.Evidence.GetHostEvidence<StrongName>();

            return AppDomain.CreateDomain(name, null, setup);
        }

    }
}
