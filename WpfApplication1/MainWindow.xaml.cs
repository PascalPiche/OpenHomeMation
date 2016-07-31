using OHM.Nodes.Commands;
using OHM.RAL.Commands;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApplication1.MV;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM vm = new MainWindowVM();

        #region Public CommandDefinition

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

        public static readonly RoutedUICommand ExecuteVrAddNodeCommand = new RoutedUICommand
        (
                "Execute VR add node basic command",
                "Execute VR add node basic command",
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

        #region Private CommandDefinition Implementation

        private void InstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!vm.InstallPlugin((Guid)e.Parameter))
            {
                MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void UnInstallPluginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!vm.UnInstallPlugin((Guid)e.Parameter))
            {
                MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void StartInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            if (!vm.StartInterface(key))
            {
                MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void StopInterfaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string key = (string)e.Parameter;
            if (!vm.StopInterface(key))
            {
                MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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
                    if (!vm.ExecuteHalCommand(command.NodeTreeKey, command.Key))
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
                e.CanExecute = vm.CanExecuteHalCommand(command.NodeTreeKey, command.Key);
            }
        }

        private void ExecuteVrAddNodeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Create fake commandDefinition
            //ICommand commandDefinition = new VrAddNodeCommand();

        }

        #endregion

        /*private class VrAddNodeCommand : CommandAbstract
        {

            protected override bool RunImplementation(System.Collections.Generic.IDictionary<string, string> arguments)
            {
                throw new NotImplementedException();
            }
        }*/

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
            w.init(command.Definition);
            var result = w.ShowDialog();

            if (result.HasValue && result.Value)
            {
                if (!vm.ExecuteHalCommand(command.NodeTreeKey, command.Definition.Key, w.ArgumentsResult))
                {
                    MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
        }

        #endregion
    }
}
