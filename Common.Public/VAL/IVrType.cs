using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.VAL
{
    public interface IVrType : IPowerNode
    {
        IList<string> GetAllowedSubVrType();
    }
}
