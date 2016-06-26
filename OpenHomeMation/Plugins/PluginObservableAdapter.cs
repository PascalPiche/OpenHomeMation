using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    internal class PluginObservableAdapter : IPlugin, INotifyPropertyChanged
    {
        private IPlugin _plugin;

        public PluginObservableAdapter(IPlugin plugin)
        {
            _plugin = plugin;
        }

        public Guid Id
        {
            get { return _plugin.Id; }
        }

        public string Name
        {
            get { return _plugin.Name; }
        }

        public PluginStates State
        {
            get { return _plugin.State; }
        }

        public bool Install(Sys.IOhmSystemInstallGateway system)
        {
            return _plugin.Install(system);
        }

        public bool Uninstall(Sys.IOhmSystemUnInstallGateway system)
        {
            return _plugin.Uninstall(system);
        }

        public bool Update()
        {
            return _plugin.Update();
        }

        public Interfaces.InterfaceAbstract CreateInterface(string key, Logger.ILogger logger)
        {
            return _plugin.CreateInterface(key, logger);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
