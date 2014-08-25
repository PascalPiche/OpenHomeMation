using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Logger
{
    public interface ISystem
    {

        ILogger Logger { get; }

        void RegisterInterface(IInterface newInterface);

        void RegisterObjectType(IAbstractNode obj);

        //IAb CreateObject(IAbstractObject obj);




    }
}
