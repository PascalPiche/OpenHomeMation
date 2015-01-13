using OHM.Commands;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;
using OHM.Plugins;
using OHM.Sys;
using System;
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

        public MainWindow()
        {
            
            InitializeComponent();
            vm.start(txt);
            this.DataContext = vm;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            vm.OHM.Shutdown();
            base.OnClosing(e);
        }

        private void PluginInstall_Click(object sender, RoutedEventArgs e)
        {
            var d = lbAvailablePlugin.SelectedItem;
        }

        private void InstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vm.PluginManager.InstallPlugin((Guid)e.Parameter, vm.OHM.System);
        }

        private void StopInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            vm.InterfaceManager.StopInterface(key);
        }

        private void StartInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            vm.InterfaceManager.StartInterface(key);
        }

        private void ExecuteInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var command = e.Parameter as OHM.Commands.ICommand;

            if (command != null)
            {
                if (command.Definition.ArgumentsDefinition.Count > 0)
                {
                    ShowCommandDialog(command);
                }
                else
                {
                    vm.InterfaceManager.ExecuteCommand(command.Node.Key, command.Definition.Key, null);
                }
            }
        }

        private void ShowCommandDialog(OHM.Commands.ICommand command)
        {
            var w = new CommandDialog();
            w.init(command);
            var result = w.ShowDialog();

            if (result.HasValue && result.Value)
            {
                vm.InterfaceManager.ExecuteCommand(command.Node.Key, command.Definition.Key, w.ArgumentsResult);
            }
        }

        private void ExecuteInterfaceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var command = e.Parameter as OHM.Commands.ICommand;
            e.CanExecute = false;
            if (command != null)
            {
                e.CanExecute = vm.InterfaceManager.CanExecuteCommand(command.Node.Key, command.Definition.Key);
            }
        }

        private void ExecuteNodeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var command = e.Parameter as OHM.Commands.ICommand;
            if (command != null)
            {
                command.Execute(null);
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

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            vm.SelectedNode = e.NewValue;
        }

        private void UnInstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vm.PluginManager.UnInstallPlugin((Guid)e.Parameter, vm.OHM.System);
        }
    }

    public class MainWindowVM : INotifyPropertyChanged
    {
        private OpenHomeMation ohm;
        private ILoggerManager loggerMng;
        private IPluginsManager pluginMng;
        private IInterfacesManager interfacesMng;

        private object selectedNode;

        public MainWindowVM()
        {
            
        }

        public void start(TextBox txt)
        {
            loggerMng = new WpfLoggerManager(txt);
            var dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng);
            ohm.start();
        }

        public IPluginsManager PluginManager { get { return pluginMng; } }

        public OpenHomeMation OHM { get { return ohm; } }

        public IInterfacesManager InterfaceManager { get { return interfacesMng; } }

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
