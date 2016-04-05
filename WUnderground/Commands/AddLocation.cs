using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Data;

namespace WUnderground.Commands
{
    class AddLocation : AbstractWUndergroundCommand
    {

        private Account _account
        {
            get
            {
                return (Account)this.Node;
            }
        }

        string _apiKey;

        public AddLocation(Account node, string apiKey)
            : base(node, "addLocation", "Add a location", "")
        {

            _apiKey = apiKey;

            this.Definition.ArgumentsDefinition.Add(
                "name",
                new ArgumentDefinition(
                    "name",
                    "Name",
                    typeof(string),
                    true
                )
            );

            this.Definition.ArgumentsDefinition.Add(
                "zip",
                new ArgumentDefinition(
                    "zip",
                    "Zip",
                    typeof(int),
                    true
                )
            );

            this.Definition.ArgumentsDefinition.Add(
               "magic",
               new ArgumentDefinition(
                   "magic",
                   "Magic",
                   typeof(int),
                   true
               )
            );

            this.Definition.ArgumentsDefinition.Add(
               "wmo",
               new ArgumentDefinition(
                   "wmo",
                   "Wmo",
                   typeof(int),
                   true
               )
           );
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            string name;
            int zip;
            int magic;
            int wmo;

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
            if (!this.Definition.ArgumentsDefinition["zip"].TryGetInt(arguments, out zip))
            {
                return false;
            }

            if (zip < 0 && zip > 99999)
            {
                return false;
            }

            //Check magic
            if (!this.Definition.ArgumentsDefinition["magic"].TryGetInt(arguments, out magic))
            {
                return false;
            }

            //Check wmo
            if (!this.Definition.ArgumentsDefinition["wmo"].TryGetInt(arguments, out wmo))
            {
                return false;
            }

            var result = WUnderground.Api.WUndergroundApi.QueryLocationExist(_apiKey, zip, magic, wmo);

            return WUndergroundInterface.CreateLocationCommand(_account, name, zip, magic, wmo);
        }
    }
}
