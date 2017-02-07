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

        internal AbstractPowerNode(string key, string name, NodeStates initialState = NodeStates.creating)
            : base(key, name, initialState)
        {
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _commandsReadOnly = new ReadOnlyObservableCollection<ICommand>(_commands); 
        }

        #endregion

        #region Public Properties

        public IReadOnlyList<ICommand> Commands 
        { 
            get 
            { 
                return _commandsReadOnly; 
            } 
        }

        #endregion

        #region Protected Functions
        
        protected virtual bool InitSubChild()
        {
            return true;
        }

        private List<AbstractCommand> commandsToInit = new List<AbstractCommand>();

        private bool InitCommandAndAdd(AbstractCommand command)
        {
            bool result = false;
            if (command.Init(this))
            {
                _commandsDic.Add(command.Key, command);
                _commands.Add(command);
                this.NotifyPropertyChanged("Commands");
                result = true;
            }
            return result;
        }

        protected bool RegisterCommand(AbstractCommand command)
        {
            bool result = false;
            
            if (this.State != NodeStates.creating && command != null && !_commandsDic.ContainsKey(command.Key))
            {
                if (!(this.State == NodeStates.fatal))
                {
                    result = InitCommandAndAdd(command);
                }
                else
                {
                    //TODO Log node is in fatal state...
                }
            }
            return result;
        }

        protected bool UnRegisterCommand(ICommand command)
        {
            bool result = false;

            //Critical zone
            if (command != null && _commandsDic.ContainsKey(command.Key) && _commands.Remove(command))
            {
                _commandsDic.Remove(command.Key);
                result = true;
            }
            //end critical zone

            return result; ;
        }

        protected abstract void RegisterCommands();

        #endregion

        #region Internal Methods

        internal bool Initing()
        {
            RegisterCommands();
            return InitSubChild();
        }

        internal bool CanExecuteCommand(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key) && _commandsDic.ContainsKey(key))
            {
                result = _commandsDic[key].CanExecute();
            }
            return result;
        }

        internal bool ExecuteCommand(string commandKey, IDictionary<string, string> arguments)
        {
            bool result = false;
            if (CanExecuteCommand(commandKey))
            {
                result = _commandsDic[commandKey].Execute(arguments);
            }
            return result;
        }

        #endregion
    }
}
