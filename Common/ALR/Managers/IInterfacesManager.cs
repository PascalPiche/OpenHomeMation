using OHM.Data;
using OHM.Logger;
using OHM.Nodes.ALR;
using OHM.Plugins;
using OHM.SYS;
using System.Collections.Generic;

namespace OHM.Managers.ALR
{
    public interface IInterfacesManager
    {

        bool Init(ILoggerManager loggerMng, IDataStore data, IOhmSystemInternal system);

        bool RegisterInterface(string key, IPlugin plugin);

        bool UnRegisterInterface(string key, IPlugin plugin);

        IList<IALRInterface> RunnableInterfaces { get; }

        bool StartInterface(string key);

        bool StopInterface(string key);

        bool CanExecuteCommand(string nodeKey, string commandKey);

        bool ExecuteCommand(string nodeKey, string commandKey, IDictionary<string, string> arguments);

    }
}
