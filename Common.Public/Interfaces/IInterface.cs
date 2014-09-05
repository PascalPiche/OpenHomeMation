using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public interface IInterface : INode, INotifyPropertyChanged
    {

        InterfaceState State { get; }

        bool IsRunning { get; }

        void Start();

        void Shutdown();
        
    }

    public enum InterfaceState
    {
        Enabled,
        Disabled,
        Error
    }
}
