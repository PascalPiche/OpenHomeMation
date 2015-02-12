using OHM.Nodes;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public interface IInterface : INode, INotifyPropertyChanged
    {

        InterfaceState State { get; }

        bool IsRunning { get; }

        bool StartOnLaunch { get; set; }

        void Starting();

        void Shutdowning();
        
    }
}
