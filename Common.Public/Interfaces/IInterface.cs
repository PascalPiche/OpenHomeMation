using OHM.Commands;
using System.Collections.Generic;

namespace OHM.Interfaces
{
    public interface IInterface
    {

        void Init();

        IList<ICommandDefinition> Commands();

        void Shutdown();
        
    }
}
