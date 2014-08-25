using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.System
{
    [Serializable]
    public class OhmSystem : ISystem
    {

        [NonSerialized]
        private ILogger _logger;


        public ILogger Logger
        {
            get { return _logger; }
            internal set { _logger = value; }
        }

        public void RegisterInterface(IInterface newInterface)
        {
            //var t = log4net.Appender.;

            //TODO throw new NotImplementedException();
        }

        public void RegisterObjectType(IAbstractNode obj)
        {
            //TODO throw new NotImplementedException();
        }
    }
}
