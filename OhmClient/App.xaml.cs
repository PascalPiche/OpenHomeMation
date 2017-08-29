using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OhmClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //e.Args

            base.OnStartup(e);
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
        }
    }
}
