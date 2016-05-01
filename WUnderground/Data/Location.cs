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

        public Location(string keyId, string name, ILogger logger, int zip, int magic, int wmo)
            : base(keyId, name, logger)
        {

            
        }
    }
}
