using OHM.Nodes;
using OHM.Nodes.Properties;
using System;
using System.Collections.Generic;
using WUnderground.Commands;

namespace WUnderground.Nodes
{
    public class AccountNode : WUndergroundNodeAbstract
    {
        private INodeProperty _apiKeyProperty;

        public AccountNode(string keyId, string name, string apiKey)
            : base(keyId, name)
        {
            _apiKeyProperty = new NodeProperty("APIKey", "API Key", typeof(String), true, "", apiKey);
        }

        internal string ApiKey { get { return _apiKeyProperty.Value as string; } }

        public bool AddLocation(string locationName, int zip, int magic, string wmo)
        {
            bool result = false;

            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("zip", zip);
            options.Add("magic", magic);
            options.Add("wmo", wmo);

            AbstractTreeNode node = this.CreateChildNode("station", locationName, locationName, options);

            if (node != null) {
                result = true;
            }

            return result;
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
