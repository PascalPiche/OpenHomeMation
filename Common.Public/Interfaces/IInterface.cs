using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Interfaces
{
    public interface IInterface
    {

        void Init();

        IList<ICommandDefinition> Commands();

        void Shutdown();
        
    }
}
