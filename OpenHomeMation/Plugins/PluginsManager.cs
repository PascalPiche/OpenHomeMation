using OHM.Data;
using OHM.Logger;
using OHM.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    public class PluginsManager : IPluginsManager
    {

        private ILogger _logger;
        private IDataStore _data;
        private IDataStore _dataInstalledPlugins;

        private IList<IPlugin> _availablesPlugins = new ObservableCollection<IPlugin>();
        private IList<IPlugin> _installedPluginsInstance = new ObservableCollection<IPlugin>();
        private readonly Type _pluginBaseType = typeof(PluginBase);

        public PluginsManager(ILoggerManager loggerMng)
        {
            this._logger = loggerMng.GetLogger("PluginsManager");
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
            _data = data;
            _dataInstalledPlugins = _data.GetDataStore("InstalledPlugins");
            if (_dataInstalledPlugins == null)
            {
                _dataInstalledPlugins = new DataStore();
                _data.StoreDataStore("InstalledPlugins", _dataInstalledPlugins);
            }

            InitPluginsList();

            return true;
        }

        public bool InstallPlugin(Guid id, ISystem system)
        {
            IPlugin plugin = FindPluginIn(id, _availablesPlugins);
            
            if (plugin != null)
            {

                return InstallPlugin(plugin, system);
            }
            else
            {
                //todo log
                //Plugin not found 
            }
            return false;
        }

        private bool InstallPlugin(IPlugin plugin, ISystem system) {
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
            _dataInstalledPlugins.StoreString(plugin.Id.ToString(), plugin.Version.ToString());
            _data.Save();
            _availablesPlugins.Remove(plugin);
            return result;
        }

        private bool IsPluginRegistered(Guid id)
        {
            return _dataInstalledPlugins.GetString(id.ToString()) != null;
        }

        private bool IsPluginInstanceInstalled(Guid id)
        {
            IPlugin plugin = FindPluginIn(id, _installedPluginsInstance);
            if (plugin != null) {
                return true;
            }
            return false;
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

            foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\", "*.dll", SearchOption.AllDirectories))
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

                            if (IsPluginRegistered(plugin.Id))
                            {
                                _installedPluginsInstance.Add(plugin);
                            }
                            else
                            {
                                _availablesPlugins.Add(plugin);
                            }
                            
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
            return !type.Equals(_pluginBaseType) && _pluginBaseType.IsAssignableFrom(type);
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
