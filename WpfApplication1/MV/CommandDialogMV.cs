using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApplication1.Designer;

namespace WpfApplication1.MV
{
    public sealed class CommandDialogMV
    {
        private ICommandDefinition _commandDefinition;
        private ObservableCollection<ArgumentsDefinitionDesigner> _arguments = new ObservableCollection<ArgumentsDefinitionDesigner>();
        private ObservableCollection<string> _optionalArguments = new ObservableCollection<string>();

        public ICommandDefinition CommandDefinition { get { return _commandDefinition; } }

        public ObservableCollection<ArgumentsDefinitionDesigner> Arguments { get { return _arguments; } }

        internal CommandDialogMV(ICommandDefinition commandDefinition)
        {
            _commandDefinition = commandDefinition;
            Init();
        }

        internal Dictionary<string, string> ArgumentsResult { get { return CreateArgumentsResult(); } }

        private void Init()
        {
            //Add required arguments
            foreach (var item in _commandDefinition.ArgumentsDefinition.Values)
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
