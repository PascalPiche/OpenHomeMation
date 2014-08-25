using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    public interface IPlugin
    {

        Guid Id { get; }

        String Name { get; }

        Version Version { get; }

        bool Install(ISystem system);

        bool Uninstall();

        bool Update();

    }
}
