using OHM.Nodes;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.RAL
{
    public interface IInterface : INode, INotifyPropertyChanged
    {
        #region Properties

        new InterfaceStates State { get; }

        bool IsRunning { get; }

        bool StartOnLaunch { get; set; }

        #endregion

        #region API

        bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, string> arguments);

        void Starting();

        void Shutdowning();

        #endregion

    }
}
