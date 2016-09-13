using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Nodes
{
    public abstract class AbstractPowerNode : AbstractBasicNode, IPowerNode
    {
        #region Private Members

        private ObservableCollection<ICommand> _commands;
        private IDictionary<string, ICommand> _commandsDic;
        private ReadOnlyObservableCollection<ICommand> _commandsReadOnly;

        #endregion

        #region Internal ctor

        internal AbstractPowerNode(string key, string name, NodeStates initialState = NodeStates.initializing)
            : base(key, name, initialState)
        {
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _commandsReadOnly = new ReadOnlyObservableCollection<ICommand>(_commands); 
        }

        #endregion

        #region Public Properties

        public IReadOnlyList<ICommand> Commands { get { return _commandsReadOnly; } }

        #endregion

        #region Protected Functions
        
        protected virtual bool Initing()
        {
            RegisterCommands();
            RegisterProperties();
            return true;
        }

        protected bool RegisterCommand(AbstractCommand command)
        {
            bool result = false;
            if (!_commandsDic.ContainsKey(command.Key))
            {
                if (command.Init(this))
                {
                    _commandsDic.Add(command.Key, command);
                    _commands.Add(command);
                    result = true;
                }
            }
            return result;
        }

        protected bool UnRegisterCommand(ICommand command)
        {
            bool result = false;
            if (_commandsDic.ContainsKey(command.Key)) {
                _commandsDic.Remove(command.Key);
                _commands.Remove(command);
                result = true;
            }
            return result; ;
        }
       
        protected abstract void RegisterCommands();

        #endregion

        #region Internal Methods

        internal bool CanExecuteCommand(string key)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].CanExecute();
            }
            return false;
        }

        internal bool ExecuteCommand(string commandKey, IDictionary<string, string> arguments)
        {
            bool result = false;
            if (_commandsDic.ContainsKey(commandKey))
            {
                result = _commandsDic[commandKey].Execute(arguments);
            }
            return result;
        }

        #endregion
    }
}
