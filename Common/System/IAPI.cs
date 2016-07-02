using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Sys
{
    public interface IAPI
    {
        IAPIResult ExecuteCommand(string key);

        IAPIResult ExecuteCommand(string key, Dictionary<String, object> arguments);

        event PropertyChangedEventHandler PropertyChanged;
    }
}
