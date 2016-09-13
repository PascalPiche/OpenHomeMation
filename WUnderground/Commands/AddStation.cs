using OHM.Nodes.Commands;
using System.Collections.Generic;
using WUnderground.Nodes;

namespace WUnderground.Commands
{
    class AddStation : AbstractWUndergroundCommand
    {

        private AccountNode _account
        {
            get
            {
                return (AccountNode)this.Node;
            }
        }

        private string _apiKey;

        public AddStation(string apiKey)
            : base("addLocation", "Add a location", "")
        {
            _apiKey = apiKey;

            this.Definition.ArgumentsDefinition.Add("name", new CommandArgumentDefinition("name", "Name", typeof(string), true));
            this.Definition.ArgumentsDefinition.Add("zip", new CommandArgumentDefinition("zip", "Zip", typeof(int), true));
            this.Definition.ArgumentsDefinition.Add("magic", new CommandArgumentDefinition("magic", "Magic", typeof(int), true));
            this.Definition.ArgumentsDefinition.Add("wmo", new CommandArgumentDefinition("wmo", "Wmo", typeof(string), true));
        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            string name;
            int zip;
            int magic;
            string wmo;

            //Check name
            if (!this.Definition.ArgumentsDefinition["name"].TryGetString(arguments, out name))
            {
                return false;
            }

            if (name.Length <= 0)
            {
                return false;
            }

            //Check zip
            if (!this.Definition.ArgumentsDefinition["zip"].TryGetInt32(arguments, out zip))
            {
                return false;
            }

            if (zip < 0 && zip > 99999)
            {
                return false;
            }

            //Check magic
            if (!this.Definition.ArgumentsDefinition["magic"].TryGetInt32(arguments, out magic))
            {
                return false;
            }

            //Check wmo
            if (!this.Definition.ArgumentsDefinition["wmo"].TryGetString(arguments, out wmo))
            {
                return false;
            }

            var result = WUnderground.Api.WUndergroundApi.QueryLocationExist(_apiKey, zip, magic, wmo);

            return WUndergroundInterface.AddStationCommandExecution(_account, name, zip, magic, wmo);
        }
    }
}
