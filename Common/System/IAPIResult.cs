using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Sys
{

    public interface IAPIResult
    {
        bool IsSuccess { get; }

        object Result { get; }
    }
}
