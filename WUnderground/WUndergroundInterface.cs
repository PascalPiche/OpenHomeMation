using OHM.Interfaces;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUnderground
{
    public class WUndergroundInterface : InterfaceAbstract
    {
        public WUndergroundInterface(ILogger logger)
            : base("WUndergroundInterface", "WUnderground", logger)
        {
            //Create Commands
            this.RegisterCommand(new AddAccount(this));
        }

        protected override void Start()
        {
            
        }

        protected override void Shutdown()
        {
            
        }
    }
}
