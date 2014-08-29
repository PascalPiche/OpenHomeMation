using OHM.Commands;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Interfaces
{
    public interface IInterface : INode
    {

        void Init();

        void Shutdown();
        
    }
}
