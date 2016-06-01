using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipsAI
{
    public class API
    {

        public static CLIPSNET.PrimitiveValue Eval(String query) {
            var env = new CLIPSNET.Environment();

            var result = env.Eval(query);

            return result;
        }
        

    }
}
