using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {
        #region Private Members

        private ICommandDefinition _definition;
        private ICommandsNode _node;

        #endregion

        #region Protected Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definition"></param>
        protected AbstractCommand(ICommandDefinition definition)
        {
            _definition = definition;
        }

        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        public string Key { get { return _definition.Key; } }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return _definition.Name; } }

        /// <summary>
        /// 
        /// </summary>
        public ICommandDefinition Definition { get { return _definition; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool CanExecute()
        {
            bool result = false;

            if (_node != null) {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public bool Execute(IDictionary<string, string> arguments)
        {
            bool result = false;
            if (this.CanExecute() && this.ValidateArguments(arguments))
            {
                result = this.RunImplementation(arguments);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public bool ValidateArguments(IDictionary<string, string> arguments)
        {
            bool result = true;

            //Validate required
            foreach (var item in this.Definition.ArgumentsDefinition.Values)
            {
                if (item.Required)
                {
                    if (arguments == null || !arguments.ContainsKey(item.Key) || arguments[item.Key] == null)
                    {
                        result = false;
                    }
                }
            }

            //Validate type value
            if (result != false && arguments != null)
            {
                foreach (var item in arguments)
                {
                    var argDef = this.Definition.ArgumentsDefinition[item.Key];
                    if (!ValidateArgumentValue(argDef, item.Value))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Protected

        /// <summary>
        /// 
        /// </summary>
        protected ICommandsNode Node { get { return _node; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected abstract bool RunImplementation(IDictionary<string, string> arguments);

        #endregion

        #region Internal Methods

        internal virtual bool Init(ICommandsNode node)
        {
            _node = node;
            return true;
        }

        #endregion

        #region Private methods

        private bool ValidateArgumentValue(IArgumentDefinition argument ,object value)
        {
            bool result = false;

            if (argument.Type == typeof(int))
            {
                int resultTemp;
                result = argument.TryGetInt32(value, out resultTemp);
            }
            else if (argument.Type == typeof(string))
            {
                string resultTemp;
                result = argument.TryGetString(value, out resultTemp);
            }

            return result;
        }

        #endregion
    }
}
