using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.SYS
{

    public interface IAPIResult
    {
        bool IsSuccess { get; }

        object Result { get; }
    }
}
