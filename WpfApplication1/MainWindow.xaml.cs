using OHM.Nodes.Commands;
using OHM.Nodes.Commands.ALR;
using System;
using System.Collections.Generic;
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

        public static readonly RoutedUICommand ExitCommand = new RoutedUICommand
        (
                "Exit",
                "Exit",
                typeof(MainWindow)
        );

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

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        #endregion 

        #region Public Ctor

        public MainWindow()
        {
            InitializeComponent();
            vm.start(txt);
            this.DataContext = vm;
            navTreeView.DataContext = vm;
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
                    Dictionary<string, string> arguments;
                    bool? result = ShowCommandDialog(command.Definition, out arguments);

                    if (result.HasValue && result.Value)
                    {
                        if (!vm.ExecuteHalCommand(command.NodeTreeKey, command.Definition.Key, arguments))
                        {
                            MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }

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
            //Create fake commandDefinition for commandDialog
            Dictionary<string, IArgumentDefinition> comArgDef = new Dictionary<string,IArgumentDefinition>();
            comArgDef.Add("nodeType", new CommandArgumentDefinition("nodeType", "Node Type", typeof(string), true));
            comArgDef.Add("key", new CommandArgumentDefinition("key", "Key", typeof(string), true));
            comArgDef.Add("name", new CommandArgumentDefinition("name", "Name", typeof(string), true));

            ICommandDefinition commandDefinition = new CommandDefinition("addVrNode", "Add Vr Node", "", comArgDef);

            Dictionary<string, string> arguments;

            bool? result = ShowCommandDialog(commandDefinition, out arguments);

            if (result.HasValue && result.Value)
            {
                if (!vm.ExecuteVrCommand("vrManager", commandDefinition.Key, arguments))
                {
                    MessageBox.Show("The command was not successfully executed", "Command error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
        }

        #endregion

        #region Private Handler 

        /*private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            vm.SelectedNode = e.NewValue;
        }*/

        #endregion

        #region Private Helper functions

        private bool? ShowCommandDialog(ICommandDefinition commandDefinition, out Dictionary<string,string> arguments)
        {
            var w = new CommandDialog();
            w.init(commandDefinition);

            bool? result = w.ShowDialog();

            arguments = w.ArgumentsResult;

            return result;
        }

        #endregion

        
    }
}
