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

        public Account(string keyId, string name, string apiKey)
            : base(keyId, name)
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

        public bool AddLocation(string locationName, int zip, int magic, string wmo)
        {
            //new Station(locationName, locationName, zip, magic, wmo)
            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("zip", zip);
            options.Add("magic", magic);
            options.Add("wmo", wmo);
            INode node = this.CreateChildNode("station", locationName, locationName, options);
            if (node != null) {
                return true;
            }
            return false;//this.AddChild(location);
        }

    }
}
