using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUnderground.Data
{
    public class Location : NodeAbstract
    {
         //private string _key;

         public Location(string keyId, string name, ILogger logger)
            : base(keyId, name, logger)
        {
            //_key = key;

            
        }
    }
}
