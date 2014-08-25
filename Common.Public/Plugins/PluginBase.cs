using OHM.Commands;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Plugins
{
    [Serializable]
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {

        public abstract Guid Id { get; }

        public abstract string Name { get; }

        public abstract Version Version { get; }

        public abstract bool Install(ISystem system);

        public abstract bool Uninstall();

        public abstract bool Update();

    }
}
