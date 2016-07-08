﻿using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Commands
{
    public interface ICommand
    {

        string NodeKey { get; }

        ICommandDefinition Definition { get; }

        bool Execute(Dictionary<string, string> arguments);

        bool CanExecute();
    }

}
