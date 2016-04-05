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

        private string _apiKey;

        

        public Account(string keyId, string name, ILogger logger, string apiKey)
            : base(keyId, name, logger)
        {
            _apiKey = apiKey;

            this.RegisterCommand(new AddLocation(this, _apiKey));
            this.RegisterCommand(new RemoveAccount(this));
        }

        public bool AddLocation(Location location)
        {
            return this.AddChild(location);
        }

    }
}
