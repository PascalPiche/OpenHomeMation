using OHM.Data;
using OHM.Plugins;
using OHM.Sys;
using System.Collections.Generic;

namespace OHM.Interfaces
{
    public interface IInterfacesManager
    {

        bool Init(IDataStore data, IOhmSystemInternal system);

        bool RegisterInterface(string key, IPlugin plugin, IOhmSystemInternal system);

        bool UnRegisterInterface(string key, IPlugin plugin, IOhmSystemInternal system);

        IList<IInterface> RunnableInterfaces { get; }

        bool StartInterface(string key);

        bool StopInterface(string key);

        bool CanExecuteCommand(string interfaceKey, string commandKey);

        bool ExecuteCommand(string interfaceKey, string commandKey, Dictionary<string, object> arguments);

    }
}
