using System.ServiceProcess;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;

namespace OhmClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceController myController;

        #region Command definition

        public static readonly RoutedUICommand CloseWindowCommand = new RoutedUICommand
        (
                "Close window",
                "Close window",
                typeof(MainWindow)
        );

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

        public static readonly RoutedUICommand PauseServiceCommand = new RoutedUICommand
        (
                "Pause service",
                "Pause service",
                typeof(MainWindow)
        );

        public static readonly RoutedUICommand ContinueServiceCommand = new RoutedUICommand
        (
                "Continue service",
                "Continue service",
                typeof(MainWindow)
        );

        #endregion

        private NotifyIcon notifier = new NotifyIcon();

        private ServiceController GetServiceByName(string name)
        {
            ServiceController result = null;

            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController item in services)
            {
                if (item.ServiceName == "OpenHomeMation")
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public MainWindow()
        {
            InitializeComponent();

            this.notifier.MouseDown += new System.Windows.Forms.MouseEventHandler(notifier_MouseDown);
            //this.notifier.Icon = ForumProjects.Properties.Resources.A;
            this.notifier.Visible = true;

            ServiceController[] services = ServiceController.GetServices();

            myController = GetServiceByName("OpenHomeMation");

            if (myController != null)
            {
                txtStatus.Text = myController.Status.ToString();
                txtDisplayName.Text = myController.DisplayName;

                txtMachineName.Text = myController.MachineName;
                txtServiceName.Text = myController.ServiceName;
                txtServiceType.Text = myController.ServiceType.ToString();
                txtCanPauseAndContinue.Text = myController.CanPauseAndContinue.ToString();
                txtCanShutdown.Text = myController.CanShutdown.ToString();
                txtCanStop.Text = myController.CanStop.ToString();

                //Show panel
                header_menu_service.Visibility = Visibility.Visible;
                service_info_pnl.Visibility = Visibility.Visible;
                service_not_found_pnl.Visibility = Visibility.Collapsed;
            }
            else
            {
                //Show Not found panel
                service_not_found_pnl.Visibility = Visibility.Visible;
                header_menu_service.Visibility = Visibility.Collapsed;
                service_info_pnl.Visibility = Visibility.Collapsed;
            }

            //myController.ExecuteCommand(int)
            //myController.Refresh
            //myController.GetLifetimeService
            //myController.WaitForStatus
        }

        void notifier_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = (ContextMenu)this.FindResource("NotifierContextMenu");
                //menu.
                //menu.IsOpen = true;
            }
        }

        #region Commands

        #region CloseWindowCommand

        private void CloseWindowCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region ExitCommand

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown(0);
        }
        #endregion

        #region StartCommand

        private void StartServiceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void StartServiceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myController.Start();
        }
        
        #endregion

        #region StopCommand

        private void StopServiceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void StopServiceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myController.Stop();
        }
       
        #endregion

        #region ContinueCommand

        private void ContinueServiceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void ContinueServiceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myController.Continue();
        }

        #endregion

        #region PauseCommand

        private void PauseServiceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void PauseServiceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myController.Pause();
        }

        #endregion

        #endregion
    }
}
