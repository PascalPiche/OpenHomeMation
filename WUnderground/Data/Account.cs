using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Commands;

namespace WUnderground.Data
{
    public class Account : NodeAbstract
    {

        private string _key;
        public Account(string keyId, string name, ILogger logger, string key)
            : base(keyId, name, logger)
        {
            _key = key;

            this.RegisterCommand(new AddLocation(this));
            this.RegisterCommand(new RemoveAccount(this));
        }

        public bool AddLocation(Location location)
        {
            return this.AddChild(location);
        }
    }
}
