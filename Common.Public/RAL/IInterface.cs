using OHM.Nodes;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.RAL
{
    public interface IInterface : INode, INotifyPropertyChanged
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
