using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Logger
{
    public interface ILoggerManager
    {

        ILogger GetLogger(Type type);

        ILogger GetLogger(String name);
    }
}
