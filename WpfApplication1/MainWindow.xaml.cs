using OHM.Commands;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Plugins;
using OHM.Sys;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApplication1.Logger;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenHomeMation ohm;
        private ILoggerManager loggerMng;
        private IPluginsManager pluginMng;
        private IInterfacesManager interfacesMng;

        public static readonly RoutedUICommand InstallPluginCommand = new RoutedUICommand
        (
                "Install Plugin",
                "Install Plugin",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand UninstallPluginCommand = new RoutedUICommand
        (
                "Uninstall Plugin",
                "Uninstall Plugin",
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
            loggerMng = new WpfLoggerManager(txt);
            var dataMng = new FileDataManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\data\\");
            pluginMng = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
            interfacesMng = new InterfacesManager(loggerMng, pluginMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng, interfacesMng);

            ohm.start();
            
            this.DataContext = pluginMng;
            InterfacesTreeViewItem.DataContext = interfacesMng.RunnableInterfaces;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ohm.Shutdown();
            base.OnClosing(e);
        }

        private void PluginInstall_Click(object sender, RoutedEventArgs e)
        {
            var d = lbAvailablePlugin.SelectedItem;
        }

        private void InstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            pluginMng.InstallPlugin((Guid)e.Parameter, ohm.System);
        }

        private void StopInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            interfacesMng.StopInterface(key);
        }

        private void StartInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            interfacesMng.StartInterface(key);
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
                    interfacesMng.ExecuteCommand(command.Node.Key, command.Definition.Key, null);
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
                interfacesMng.ExecuteCommand(command.Node.Key, command.Definition.Key, w.ArgumentsResult);
            }
        }

        private void ExecuteInterfaceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var command = e.Parameter as OHM.Commands.ICommand;
            e.CanExecute = false;
            if (command != null)
            {
                e.CanExecute = interfacesMng.CanExecuteCommand(command.Node.Key, command.Definition.Key);
            }
        }
    }
}
