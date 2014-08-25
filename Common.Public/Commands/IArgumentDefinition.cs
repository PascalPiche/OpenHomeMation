using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Commands
{
    public interface IArgumentDefinition
    {

        string Name { get;  }

        Type Type { get; }

        bool Required { get; }

    }
}
