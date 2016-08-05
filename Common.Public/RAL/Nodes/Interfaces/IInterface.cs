using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.RAL
{
    public interface IInterface : IPowerTreeNode
    {
        #region Properties

        InterfaceStates InterfaceState { get; }

        bool IsRunning { get; }

        bool StartOnLaunch { get; set; }

        #endregion

        #region API

        bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, string> arguments);

        bool CanExecuteCommand(string nodeKey, string key);

        void Starting();

        void Shutdowning();

        #endregion

    }
}
