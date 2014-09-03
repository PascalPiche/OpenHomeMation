
using OHM.Data;
using OHM.Plugins;
using System.Collections.Generic;

namespace OHM.Interfaces
{
    public interface IInterfacesManager
    {

        bool Init(IDataStore data);

        bool RegisterInterface(string key, IPlugin plugin);

        IList<IInterface> RunnableInterfaces { get; }

        bool StartInterface(string key);

        bool StopInterface(string key);

    }

}
