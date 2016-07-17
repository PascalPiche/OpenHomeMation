using System.Collections.Generic;
using System.ComponentModel;

namespace OHM.SYS
{
    public interface IAPI
    {
        IAPIResult ExecuteCommand(string key);

        IAPIResult ExecuteCommand(string key, Dictionary<string, string> arguments);

        event PropertyChangedEventHandler PropertyChanged;
    }
}
