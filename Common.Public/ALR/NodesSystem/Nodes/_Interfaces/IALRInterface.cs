﻿using System.Collections.Generic;

namespace OHM.Nodes.ALR
{
    public interface IALRInterface : IPowerTreeNode
    {
        #region Properties

        ALRInterfaceStates InterfaceState { get; }

        bool IsRunning { get; }

        bool StartOnLaunch { get; }

        #endregion

        #region API

        bool ExecuteCommand(string nodeKey, string commandKey, IDictionary<string, string> arguments);

        bool CanExecuteCommand(string nodeKey, string key);

        bool Starting();

        bool Shutdowning();

        #endregion
    }
}
