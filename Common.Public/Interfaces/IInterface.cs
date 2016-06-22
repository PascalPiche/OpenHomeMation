using OHM.Nodes;
using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public interface IInterface : INode, INotifyPropertyChanged
    {
        InterfaceState State { get; }

        bool ExecuteCommand(string nodeKey, string commandKey, Dictionary<string, object> arguments);

        bool IsRunning { get; }

        bool StartOnLaunch { get; set; }

        void Starting();

        void Shutdowning();
    }
}
