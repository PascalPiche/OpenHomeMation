using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes
{
    public interface INode
    {

        string Key { get; }

        string Name { get; }

        IList<ICommandDefinition> Commands { get; }

    }

    public class NodeAbstract
    {
        private string _key;
        private string _name;
        private ObservableCollection<ICommandDefinition> _commandsDefinition;
        private Dictionary<string, ICommand> _commands;

        public event PropertyChangedEventHandler PropertyChanged;

        public NodeAbstract(string key, string name)
        {
            _key = key;
            _name = name;
            _commandsDefinition = new ObservableCollection<ICommandDefinition>();
            _commands = new Dictionary<string, ICommand>();
        }

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
        }

        public IList<ICommandDefinition> Commands { get { return new ReadOnlyObservableCollection<ICommandDefinition>(_commandsDefinition); } }

        protected bool RegisterCommand(ICommand command)
        {
            if (_commands.ContainsKey(command.Definition.Key))
            {
                return false;
            }
            _commands.Add(command.Definition.Key, command);
            _commandsDefinition.Add(command.Definition);
            return true;
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
