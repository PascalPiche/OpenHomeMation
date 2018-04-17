using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.Nodes.ALR;
using OHM.Plugins;
using OHM.SYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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

        public IList<IALRInterface> Interfaces
        {
            get
            {
                return (IList<IALRInterface>)ohm.API.ExecuteCommand("ral/list/interfaces/").Result;
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
                NotifyPropertyChanged("IsHomeViewVisible");
                NotifyPropertyChanged("IsSystemViewVisible");
                NotifyPropertyChanged("IsDatasManagerViewVisible");
                NotifyPropertyChanged("IsPluginsManagerViewVisible");
                NotifyPropertyChanged("IsInterfacesManagerViewVisible");
                NotifyPropertyChanged("IsInterfaceViewVisible");
                NotifyPropertyChanged("IsNodeViewVisible");
                NotifyPropertyChanged("SelectedNode");
            }
        }

        private Visibility IsVisibleWhenSelectedNodeTagMatch(string value)
        {
            if (selectedNode is TreeViewItem && 
                ((TreeViewItem)selectedNode).Tag != null &&
                ((TreeViewItem)selectedNode).Tag.ToString() == value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public Visibility IsHomeViewVisible
        {
            get
            {
                return IsVisibleWhenSelectedNodeTagMatch("home");
            }
        }

        public Visibility IsSystemViewVisible
        {
            get
            {
                return IsVisibleWhenSelectedNodeTagMatch("system");
            }
        }

        public Visibility IsDatasManagerViewVisible
        {
            get
            {
                return IsVisibleWhenSelectedNodeTagMatch("datasManager");
            }
        }

        public Visibility IsPluginsManagerViewVisible
        {
            get
            {
                return IsVisibleWhenSelectedNodeTagMatch("pluginsManager");
            }
        }

        public Visibility IsInterfacesManagerViewVisible
        {
            get
            {
                return IsVisibleWhenSelectedNodeTagMatch("interfacesManager");
            }
        }

        public Visibility IsInterfaceViewVisible
        {
            get
            {
                if (selectedNode is IALRInterface)
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
                if (!(selectedNode is IALRInterface) && selectedNode is ALRAbstractTreeNode)
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

        #region ALR

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
            ILoggerManager loggerMng = new LoggerManager();
            DataManagerAbstract dataMng = new FileDataManager(AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
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
