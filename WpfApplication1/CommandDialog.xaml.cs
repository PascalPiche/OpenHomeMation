using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Windows;
using WpfApplication1.MV;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for CommandDialog.xaml
    /// </summary>
    public partial class CommandDialog : Window
    {
        private CommandDialogMV _mv;
        
        public CommandDialog()
        {
            InitializeComponent();
            okBtn.Click += okBtn_Click;
        }

        void okBtn_Click(object sender, RoutedEventArgs e)
        {
            //Validate arguments
            if (_mv.Validate())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        public Dictionary<string, string> ArgumentsResult { get { return _mv.ArgumentsResult; } }

        public void init(ICommand commandDefinition)
        {
            _mv = new CommandDialogMV(commandDefinition);
            
            this.DataContext = _mv;
        }
    }

}
