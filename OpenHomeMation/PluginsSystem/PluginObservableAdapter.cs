using OHM.Nodes.ALR;
using OHM.Nodes.ALV;
using OHM.SYS;
using System;
using System.ComponentModel;

namespace OHM.Plugins
{
    internal class PluginObservableAdapter : IPlugin, INotifyPropertyChanged
    {
        #region Private Members

        private IPlugin _plugin;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Ctor

        public PluginObservableAdapter(IPlugin plugin) { _plugin = plugin; }

        #endregion

        #region Public properties

        public Guid Id              { get { return _plugin.Id; } }

        public string Name          { get { return _plugin.Name; } }

        public PluginStates State   { get { return _plugin.State; } }

        #endregion

        #region Public Methods

        public bool Install(IOhmSystemInstallGateway system)            { return _plugin.Install(system); }

        public bool Uninstall(IOhmSystemUnInstallGateway system)        { return _plugin.Uninstall(system); }

        public bool Update(IOhmSystemInstallGateway system)             { return _plugin.Update(system); }

        public ALRInterfaceAbstractNode CreateInterface(string key)     { return _plugin.CreateInterface(key); }

        public IVrType CreateVrNode(string key)                         { return _plugin.CreateVrNode(key); }

        #endregion
    }
}
