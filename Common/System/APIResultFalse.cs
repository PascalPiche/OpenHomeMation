using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.SYS
{
    public sealed class APIResultFalse : IAPIResult
    {
        public APIResultFalse() { }

        public bool IsSuccess
        {
            get { return false; }
        }

        public object Result
        {
            get { return null; }
        }
    }
}
