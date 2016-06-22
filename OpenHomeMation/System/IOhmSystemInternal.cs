using OHM.Common.Vr;
using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;

namespace OHM.Sys
{
    internal interface IOhmSystemInternal : IOhmSystem
    {
        IDataManager DataMng { get; }

        ILoggerManager LoggerMng { get; }

        IInterfacesManager InterfacesMng { get; }

        IVrManager vrMng { get; }


    }
}
