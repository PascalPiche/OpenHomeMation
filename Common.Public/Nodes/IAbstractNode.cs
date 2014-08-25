using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Nodes
{
    public interface IAbstractNode
    {

        string Key { get; }

        IList<ICommandDefinition> Commands { get; }

        bool RunCommand(ICommandDefinition command, Dictionary<string, object> arguments);

    }
}
