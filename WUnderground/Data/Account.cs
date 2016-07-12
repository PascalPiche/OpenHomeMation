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
    public class Account : WUndergroundNodeAbstract
    {

        private string _apiKey;

        public Account(string keyId, string name, ILogger logger, string apiKey)
            : base(keyId, name, logger)
        {
            _apiKey = apiKey;

            this.RegisterCommand(new AddStation(_apiKey));
            this.RegisterCommand(new RemoveAccount());

            this.RegisterProperty(
               new NodeProperty(
                   "APIKey",
                   "API Key",
                   typeof(String),
                   true,
                   "",
                  _apiKey));

        }

        internal string ApiKey
        {
            get
            {
                return _apiKey;
            }
        }

        public bool AddLocation(Station location)
        {
            return this.AddChild(location);
        }

    }
}
