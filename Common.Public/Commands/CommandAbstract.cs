﻿using OHM.Nodes;
using System;
using System.Collections.Generic;

namespace OHM.Commands
{
    
    public abstract class CommandAbstract : ICommand
    {

        private ICommandDefinition _definition;
        private INode _node;
        #region "Ctor"

        public CommandAbstract(INode node, string key, string name) 
            : this(node, key, name, string.Empty, null) { }

        public CommandAbstract(INode node, string key, string name, string description) 
            : this (node, key, name, description, null) { }

        public CommandAbstract(
            INode node,
            string key, 
            string name,
            string description, 
            Dictionary<string, IArgumentDefinition> argumentsDefinition
        )
        {
            _node = node;
            _definition = new CommandDefinition(key, name, description, argumentsDefinition);
        }

        public CommandAbstract(INode node, ICommandDefinition definition)
        {
            _node = node;
            _definition = definition;
        }

        #endregion

        #region "Public"

        public Nodes.INode Node
        {
            get { return _node; }
        }

        public ICommandDefinition Definition
        {
            get { return _definition; }
        }

        public virtual bool CanExecute()
        {
            return true;
        }

        #endregion

        #region "Protected"

        protected abstract bool RunImplementation(Dictionary<string, object> arguments);

        #endregion

        #region "Internal"

        public bool Execute(Dictionary<string, object> arguments)
        {
            bool result = false;
            if (_definition.ValidateArguments(arguments))
            {
                result = RunImplementation(arguments);
            }
            return result;
        }

        #endregion

        
    }
}
