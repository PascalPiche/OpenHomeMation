using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUnderground
{
    public class AddAccount : CommandAbstract
    {

        private WUndergroundInterface Interface 
        {
            get
            {
                return (WUndergroundInterface)this.Node;
            }
        }
        public AddAccount(WUndergroundInterface node)
            : base(node, "addAccount", "Add an account")
        {
            this.Definition.ArgumentsDefinition.Add(
                "username",
                new ArgumentDefinition(
                    "username",
                    "User Name",
                    typeof(string),
                    true
                )
            );

            this.Definition.ArgumentsDefinition.Add(
                "keyid",
                new ArgumentDefinition(
                    "keyid",
                    "Key Id",
                    typeof(string),
                    true
                )
            );
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            string username;
            string keyId;

            if (!this.Definition.ArgumentsDefinition["username"].TryGetString(arguments["username"], out username))
            {
                return false;
            }

            if (!this.Definition.ArgumentsDefinition["keyid"].TryGetString(arguments["keyid"], out keyId))
            {
                return false;
            }

            if (username.Length <= 0 || keyId.Length <= 0)
            {
                return false;
            }

            return Interface.CreateAccount(username, keyId);
        }
    }
}
