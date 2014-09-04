using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

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
            var args = ArgumentsResult;
            if (_mv.Command.ValidateArguments(args))
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        public Dictionary<String, object> ArgumentsResult { get { return _mv.ArgumentsResult; } }

        public void init(OHM.Commands.ICommand command)
        {
            _mv = new CommandDialogMV(command);
            
            this.DataContext = _mv;
        }

    }

    public class CommandDialogMV
    {
        private OHM.Commands.ICommand _command;
        private ObservableCollection<ArgumentsDefinitionDesigner> _arguments = new ObservableCollection<ArgumentsDefinitionDesigner>();
        private ObservableCollection<string> _optionalArguments = new ObservableCollection<string>();

        public OHM.Commands.ICommand Command { get { return _command; } }

        public ObservableCollection<ArgumentsDefinitionDesigner> Arguments { get { return _arguments; } }
        
        internal CommandDialogMV(OHM.Commands.ICommand command)
        {
            _command = command;
            Init();
        }

        internal Dictionary<String, object> ArgumentsResult { get { return CreateArgumentsResult(); } }

        private void Init()
        {
            //Add required arguments
            foreach (var item in _command.ArgumentsDefinition.Values)
            {
                if (item.Required)
                {
                    _arguments.Add(new ArgumentsDefinitionDesigner(item));
                }
                else
                {
                    _optionalArguments.Add(item.Name);
                }
            }
        }

        private Dictionary<String, object> CreateArgumentsResult()
        {
            Dictionary<String, object> result = new Dictionary<String, object>();

            foreach (var item in _arguments)
            {
                result.Add(item.ArgumentDefinition.Key, item.Value);
            }

            return result;
        }
    }

    public class ArgumentsDefinitionDesigner : INotifyPropertyChanged
    {
        private IArgumentDefinition _argumentDef;
        private Object _value;

        public ArgumentsDefinitionDesigner(IArgumentDefinition argumentDef)
        {
            _argumentDef = argumentDef;
        }

        public IArgumentDefinition ArgumentDefinition { get { return _argumentDef; } }

        public object Value { 
            get { return _value; } 
            set 
            {
                _value = value;
                NotifyPropertyChanged("Value");
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
