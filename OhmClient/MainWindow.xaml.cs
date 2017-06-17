using System.ServiceProcess;
using System.Windows;
using System.Windows.Input;

namespace OhmClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceController myController;

        public static readonly RoutedUICommand ExitCommand = new RoutedUICommand
        (
                "Exit",
                "Exit",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand StartServiceCommand = new RoutedUICommand
        (
                "Start service",
                "Start service",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand StopServiceCommand = new RoutedUICommand
        (
                "Stop service",
                "Stop service",
                typeof(MainWindow)
        );

        public MainWindow()
        {
            InitializeComponent();

            myController = new ServiceController("OpenHomeMation");
            
            //myController.ExecuteCommand(int)

            //myController.Pause
            //myController.Continue
            //myController.Start
            //myController.Stop
            //myController.Refresh

            //myController.GetLifetimeService
            //myController.DependentServices
            //myController.ServicesDependedOn
            //myController.WaitForStatus
            
            txtDisplayName.Text = myController.DisplayName;
            txtStatus.Text = myController.Status.ToString();
            txtMachineName.Text = myController.MachineName;
            txtServiceName.Text = myController.ServiceName;
            txtServiceType.Text = myController.ServiceType.ToString();
            txtCanPauseAndContinue.Text = myController.CanPauseAndContinue.ToString();
            txtCanShutdown.Text = myController.CanShutdown.ToString();
            txtCanStop.Text = myController.CanStop.ToString();

            //myController.ExecuteCommand(128);
        }

        #region ExitCommand

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
        #endregion

        #region StartCommand

        private void StartServiceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void StartServiceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        #endregion

        #region StopCommand
        private void StopServiceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void StopServiceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
        #endregion
    }
}
