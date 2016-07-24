using OHM.Nodes;
using System;
using System.Collections.Generic;
using WUnderground.Commands;

namespace WUnderground.Data
{
    public class Account : WUndergroundNodeAbstract
    {

        private INodeProperty _apiKeyProperty;

        public Account(string keyId, string name, string apiKey)
            : base(keyId, name)
        {
            _apiKeyProperty = new NodeProperty("APIKey", "API Key", typeof(String), true, "", apiKey);
        }

        internal string ApiKey { get { return _apiKeyProperty.Value as string; } }

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

        protected override void RegisterCommands()
        {
            this.RegisterCommand(new AddStation(_apiKeyProperty.Value as string));
            this.RegisterCommand(new RemoveAccount());
        }

        protected override void RegisterProperties()
        {
            this.RegisterProperty(_apiKeyProperty);
        }
    }
}
