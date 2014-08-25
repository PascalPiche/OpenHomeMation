using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OHM.Logger;
using OHM.Data;
using OHM.Plugins;
using log4net;
using OHM.System;

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

        public static readonly RoutedUICommand InstallPluginCommand = new RoutedUICommand
        (
                "Install Plugin",
                "Install Plugin",
                typeof(MainWindow)
        );

        public MainWindow()
        {
            InitializeComponent();
            loggerMng = new WpfLoggerManager(txt);
            var dataMng = new FileDataManager();


            pluginMng = new PluginsManager(loggerMng);
            ohm = new OpenHomeMation(pluginMng, dataMng, loggerMng);

            ohm.start();
            
            this.DataContext = pluginMng;
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //zwave.Shutdown();
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
    }

    public class WpfLoggerManager : ILoggerManager
    {

        private TextBox _txt;
        public WpfLoggerManager(TextBox txt)
        {
            _txt = txt;
        }

        public ILogger GetLogger(Type type)
        {
            return new WpfLogger(log4net.LogManager.GetLogger(type), _txt);
        }

        public ILogger GetLogger(string name)
        {
            return new WpfLogger(log4net.LogManager.GetLogger(name), _txt);
        }
    }

    public class WpfLogger : DefaultLogger, ILogger
    {
        private TextBox output;
        public WpfLogger(ILog log, TextBox output)
            : base(log)
        {
            this.output = output;
        }

        public override void Debug(object message)
        {
            base.Debug(message);
            wpfLog(message.ToString());
        }

        public override void Debug(object message, Exception exception)
        {
            base.Debug(message, exception);
            wpfLog(message.ToString());
        }

        public override void Error(object message)
        {
            base.Error(message);
            wpfLog(message.ToString());
        }

        public override void Error(object message, Exception exception)
        {
            base.Error(message, exception);
            wpfLog(message.ToString());
        }

        public override void Fatal(object message)
        {
            base.Fatal(message);
            wpfLog(message.ToString());
        }

        public override void Fatal(object message, Exception exception)
        {
            base.Fatal(message, exception);
            wpfLog(message.ToString());
        }

        public override void Info(object message)
        {
            base.Info(message);
            wpfLog(message.ToString());
        }

        public override void Info(object message, Exception exception)
        {
            base.Info(message, exception);
            wpfLog(message.ToString());
        }

        public override void Warn(object message)
        {
            base.Warn(message);
            wpfLog(message.ToString());
        }

        public override void Warn(object message, Exception exception)
        {
            base.Warn(message, exception);
            wpfLog(message.ToString());
        }

        public void wpfLog(string message)
        {
            output.Dispatcher.BeginInvoke(new Action(delegate {
                output.AppendText(message + "\n");
                
            }));
            
        }
    }
}
