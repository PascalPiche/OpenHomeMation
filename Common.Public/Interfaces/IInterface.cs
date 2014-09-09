using OHM.Nodes;
using System.ComponentModel;

namespace OHM.Interfaces
{
    public interface IInterface : INode, INotifyPropertyChanged
    {

        InterfaceState State { get; }

        bool IsRunning { get; }

        bool StartOnLaunch { get; set; }

        void Start();

        void Shutdown();
        
    }
}
