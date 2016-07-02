using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.SYS
{
    public sealed class APIResultTrue : IAPIResult
    {
        private object _result;

        public APIResultTrue(object result)
        {
            _result = result;
        }

        public bool IsSuccess
        {
            get { return true; }
        }

        public object Result
        {
            get { return _result; }
        }
    }
}
