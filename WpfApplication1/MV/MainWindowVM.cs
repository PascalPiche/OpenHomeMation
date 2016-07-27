using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using OHM.VAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApplication1.Logger;

namespace WpfApplication1.MV
{
    public sealed class MainWindowVM : INotifyPropertyChanged
    {
        #region Private members

        private OpenHomeMation ohm;
        private object selectedNode;

        #endregion

        #region Public Ctor

        public MainWindowVM() { }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public IList<IPlugin> AvailablesPlugins
        {
            get
            {
                return (IList<IPlugin>)ohm.API.ExecuteCommand("plugins/list/availables/").Result;
            }
        }

        public IList<IPlugin> InstalledPlugins
        {
            get
            {
                return (IList<IPlugin>)ohm.API.ExecuteCommand("plugins/list/installed/").Result;
            }
        }

        public IList<IInterface> Interfaces
        {
            get
            {
                return (IList<IInterface>)ohm.API.ExecuteCommand("ral/list/interfaces/").Result;
            }
        }

        public Object SelectedNode
        {
            get
            {
                return selectedNode;
            }

            set
            {
                selectedNode = value;
                NotifyPropertyChanged("IsSystemViewVisible");
                NotifyPropertyChanged("IsPluginsManagerViewVisible");
                NotifyPropertyChanged("IsInterfaceViewVisible");
                NotifyPropertyChanged("IsNodeViewVisible");
                NotifyPropertyChanged("SelectedNode");
            }
        }

        public Visibility IsPluginsManagerViewVisible
        {
            get
            {
                if (selectedNode is TreeViewItem && ((TreeViewItem)selectedNode).Tag.ToString() == "pluginsManager")
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility IsInterfaceViewVisible
        {
            get
            {
                if (selectedNode is IInterface)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility IsSystemViewVisible
        {
            get
            {
                if (selectedNode is TreeViewItem && ((TreeViewItem)selectedNode).Tag.ToString() == "system")
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility IsNodeViewVisible
        {
            get
            {
                if (IsInterfaceViewVisible != Visibility.Visible && selectedNode is ITreeNode)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        #endregion

        #region Public Methods

        #region PluginsManager

        public bool InstallPlugin(Guid guid)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("guid", guid.ToString());
            return ohm.API.ExecuteCommand("plugins/execute/install/", args).IsSuccess;
        }

        public bool UnInstallPlugin(Guid guid)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("guid", guid.ToString());
            return ohm.API.ExecuteCommand("plugins/execute/uninstall/", args).IsSuccess;
        }

        #endregion

        #region Hal

        public bool StartInterface(string key)
        {
            return ohm.API.ExecuteCommand("ral/execute/start/" + key + "/").IsSuccess;
        }

        public bool StopInterface(string key)
        {
            return ohm.API.ExecuteCommand("ral/execute/stop/" + key + "/").IsSuccess;
        }

        public bool ExecuteHalCommand(string nodeKey, string commandKey)
        {
            return this.ExecuteHalCommand(nodeKey, commandKey, null);
        }

        public bool ExecuteHalCommand(string nodeKey, string commandKey, Dictionary<string, string> args)
        {
            return ohm.API.ExecuteCommand("ral/execute/" + commandKey + "/" + nodeKey + "/", args).IsSuccess;
        }

        public bool CanExecuteHalCommand(string nodeKey, string commandKey)
        {
            return ohm.API.ExecuteCommand("ral/can-execute/" + commandKey + "/" + nodeKey + "/").IsSuccess;
        }

        #endregion

        #region VR

        public bool ExecuteVrCommand(string nodeKey, string commandKey)
        {
            return this.ExecuteVrCommand(nodeKey, commandKey, null);
        }

        public bool ExecuteVrCommand(string nodeKey, string commandKey, Dictionary<string, string> args)
        {
            return ohm.API.ExecuteCommand("val/execute/" + commandKey + "/" + nodeKey, args).IsSuccess;
        }

        public bool CanExecuteVrCommand(string nodeKey, string commandKey)
        {
            return ohm.API.ExecuteCommand("val/can-execute/" + commandKey + "/" + nodeKey).IsSuccess;
        }

        #endregion

        #endregion

        #region Internal Methods

        internal void start(TextBox txt)
        {
            ILoggerManager loggerMng = new WpfLoggerManager(txt);
            IDataManager dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            IPluginsManager pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            IInterfacesManager interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            IVrManager vrMng = new VrManager(loggerMng, pluginMng);

            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
            ohm.API.PropertyChanged += API_PropertyChanged;
            ohm.Start();
        }

        internal bool Shutdown()
        {
            ohm.Shutdown();
            return true;
        }

        #endregion

        #region Private Methods

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void API_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "plugins/installed/")
            {
                this.NotifyPropertyChanged("InstalledPlugins");
            }
        }

        #endregion
    }
}
