using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace WUnderground.Commands
{
    class AddAccount : AbstractWUndergroundCommand
    {
        public AddAccount()
            : base("addAccount", "Add an account", "")
        {
            this.Definition.ArgumentsDefinition.Add("username", new CommandArgumentDefinition("username", "User Name", typeof(string), true));
            this.Definition.ArgumentsDefinition.Add("keyid", new CommandArgumentDefinition("keyid", "Key Id", typeof(string), true));
        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            string username;
            string keyId;

            if (!this.Definition.ArgumentsDefinition["username"].TryGetString(arguments, out username))
            {
                return false;
            }

            if (!this.Definition.ArgumentsDefinition["keyid"].TryGetString(arguments, out keyId))
            {
                return false;
            }

            if (username.Length <= 0 || keyId.Length <= 0)
            {
                return false;
            }

            return WUndergroundInterface.CreateAccountCommand(username, keyId);
        }
    }
}
