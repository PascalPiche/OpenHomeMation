using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApplication1.Designer;

namespace WpfApplication1.MV
{
    public sealed class CommandDialogMV
    {
        private ICommand _command;
        private ObservableCollection<ArgumentsDefinitionDesigner> _arguments = new ObservableCollection<ArgumentsDefinitionDesigner>();
        private ObservableCollection<string> _optionalArguments = new ObservableCollection<string>();

        public ICommand Command { get { return _command; } }

        public ObservableCollection<ArgumentsDefinitionDesigner> Arguments { get { return _arguments; } }

        internal CommandDialogMV(ICommand command)
        {
            _command = command;
            Init();
        }

        internal Dictionary<string, string> ArgumentsResult { get { return CreateArgumentsResult(); } }

        private void Init()
        {
            //Add required arguments
            foreach (var item in _command.Definition.ArgumentsDefinition.Values)
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

        private Dictionary<string, string> CreateArgumentsResult()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var item in _arguments)
            {
                result.Add(item.ArgumentDefinition.Key, item.Value);
            }

            return result;
        }
    }
}
