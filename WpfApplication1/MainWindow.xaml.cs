﻿using OHM.Commands;
using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Plugins;
using OHM.RAL;
using OHM.RAL.Commands;
using OHM.SYS;
using OHM.VAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApplication1.Logger;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM vm = new MainWindowVM();

        #region Public Command

        public static readonly RoutedUICommand InstallPluginCommand = new RoutedUICommand
        (
                "Install Plugin",
                "Install Plugin",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand UnInstallPluginCommand = new RoutedUICommand
        (
                "UnInstall Plugin",
                "UnInstall Plugin",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand StartInterfaceCommand = new RoutedUICommand
        (
                "Start Interface",
                "Start Interface",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand StopInterfaceCommand = new RoutedUICommand
        (
                "Stop Interface",
                "Stop Interface",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand ExecuteInterfaceCommand = new RoutedUICommand
        (
                "Execute interface command",
                "Execute interface command",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand ExecuteNodeCommand = new RoutedUICommand
        (
                "Execute node command",
                "Execute node command",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand ExecuteVrAddNodeBasic = new RoutedUICommand
        (
                "Execute vr add node basic command",
                "Execute vr add node basic command",
                typeof(MainWindow)
        );

        #endregion 

        #region Public Ctor

        public MainWindow()
        {
            InitializeComponent();
            vm.start(txt);
            this.DataContext = vm;
        }

        #endregion

        #region Protected Override functions

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!vm.Shutdown())
            {
                e.Cancel = true;

                //TODO SHOW ERROR
            }
            base.OnClosing(e);
        }

        #endregion

        #region Private Command Implementation

        private void InstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vm.InstallPlugin((Guid)e.Parameter);
        }

        private void UnInstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vm.UnInstallPlugin((Guid)e.Parameter);
        }

        private void StartInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            vm.InterfaceManager.StartInterface(key);
        }

        private void StopInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            vm.InterfaceManager.StopInterface(key);
        }
       
        private void ExecuteInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var command = e.Parameter as IInterfaceCommand;

            if (command != null)
            {
                if (command.Definition.ArgumentsDefinition.Count > 0)
                {
                    ShowCommandDialog(command);
                }
                else
                {
                    if (!vm.InterfaceManager.ExecuteCommand(command.InterfaceKey, command.NodeKey, command.Definition.Key, null))
                    {
                        //Show alert
                        MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
        }

        private void ExecuteInterfaceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var command = e.Parameter as IInterfaceCommand;
            e.CanExecute = false;
            if (command != null)
            {
                e.CanExecute = vm.InterfaceManager.CanExecuteCommand(command.NodeKey, command.Definition.Key);
            }
        }

        private void ExecuteNodeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var command = e.Parameter as IInterfaceCommand;
            if (command != null)
            {
                if (command.Definition.ArgumentsDefinition.Count > 0)
                {
                    ShowCommandDialog(command);
                }
                else
                {
                    if (!vm.InterfaceManager.ExecuteCommand(command.InterfaceKey, command.NodeKey, command.Definition.Key, null))
                    {
                        //Show alert
                        MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
        }

        private void ExecuteNodeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var command = e.Parameter as OHM.Commands.ICommand;
            e.CanExecute = false;
            if (command != null)
            {
                e.CanExecute = command.CanExecute();
            }
        }

        private void ExecuteVrAddNodeBasic_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO
        }

        #endregion

        #region Private Handler 

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            vm.SelectedNode = e.NewValue;
        }

        #endregion

        #region Private Helper functions

        private void ShowCommandDialog(IInterfaceCommand command)
        {
            var w = new CommandDialog();
            w.init(command);
            var result = w.ShowDialog();

            if (result.HasValue && result.Value)
            {
                if (!vm.InterfaceManager.ExecuteCommand(command.InterfaceKey, command.NodeKey, command.Definition.Key, w.ArgumentsResult))
                {
                    //Show alert
                    MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
        }

        #endregion

    }

    public class MainWindowVM : INotifyPropertyChanged
    {
        private OpenHomeMation ohm;
        private IInterfacesManager interfacesMng;

        private object selectedNode;

        public MainWindowVM() {}

        public void start(TextBox txt)
        {
            ILoggerManager loggerMng = new WpfLoggerManager(txt);
            IDataManager dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            IPluginsManager pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            IVrManager vrMng = new VrManager(loggerMng, pluginMng);

            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng, vrMng);
            ohm.API.PropertyChanged += API_PropertyChanged;
            ohm.Start();
        }

        void API_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "plugins/installed/")
            {
                this.NotifyPropertyChanged("InstalledPlugins");
            }
        }

        public void InstallPlugin(Guid guid)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("guid", guid.ToString());
            ohm.API.ExecuteCommand("plugins/install/", args);
        }
        
        public void UnInstallPlugin(Guid guid)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("guid", guid.ToString());
            ohm.API.ExecuteCommand("plugins/uninstall/", args);
        }

        public IInterfacesManager InterfaceManager { get { return interfacesMng; } }

        public bool Shutdown()
        {
            ohm.Shutdown();
            return true;
        }

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
                NotifyPropertyChanged("IsNodeViewVisible");
                NotifyPropertyChanged("IsInterfaceViewVisible");
                NotifyPropertyChanged("SelectedNode");
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
                if (IsNodeViewVisible != Visibility.Visible && IsInterfaceViewVisible != Visibility.Visible)
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
                if (IsInterfaceViewVisible != Visibility.Visible && selectedNode is INode)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
