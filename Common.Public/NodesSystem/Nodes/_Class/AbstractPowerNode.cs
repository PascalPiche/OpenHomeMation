using OHM.Nodes.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Nodes
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractPowerNode : AbstractBasicNode, ICommandsNode
    {
        #region Private Members

        private ObservableCollection<ICommand> _commands;
        private IDictionary<string, ICommand> _commandsDic;
        private ReadOnlyObservableCollection<ICommand> _commandsReadOnly;

        #endregion

        #region Protected ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        protected AbstractPowerNode(string key, string name)
            : base(key, name)
        {
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _commandsReadOnly = new ReadOnlyObservableCollection<ICommand>(_commands); 
        }

        #endregion

        #region Public Properties

        #region ICommandsNode implementation

        public IReadOnlyList<ICommand> Commands 
        { 
            get 
            { 
                return _commandsReadOnly; 
            } 
        }

        #endregion

        #endregion

        #region Protected Functions

        protected bool RegisterCommand(AbstractCommand command)
        {
            bool result = false;
            
            if (this.SystemState != SystemNodeStates.creating && command != null && !_commandsDic.ContainsKey(command.Key))
            {
                if (!(this.SystemState == SystemNodeStates.fatal))
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

        internal protected virtual bool Initing()
        {
            RegisterCommands();
            return true;
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

        #region Private method

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

        #endregion
    }
}
