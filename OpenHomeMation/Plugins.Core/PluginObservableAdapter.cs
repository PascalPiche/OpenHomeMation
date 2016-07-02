﻿using OHM.RAL;
using OHM.SYS;
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

        public InterfaceAbstract CreateInterface(string key, Logger.ILogger logger)
        {
            return _plugin.CreateInterface(key, logger);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
