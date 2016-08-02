using OHM.RAL;
using OHM.SYS;
using OHM.VAL;
using System;
using System.ComponentModel;

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

        public bool Install(IOhmSystemInstallGateway system)
        {
            return _plugin.Install(system);
        }

        public bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            return _plugin.Uninstall(system);
        }

        public bool Update()
        {
            return _plugin.Update();
        }

        public RalInterfaceNodeAbstract CreateInterface(string key)
        {
            return _plugin.CreateInterface(key);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IVrType CreateVrNode(string key)
        {
            return _plugin.CreateVrNode(key);
        }
    }
}
