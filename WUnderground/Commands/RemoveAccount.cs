using System.Collections.Generic;

namespace WUnderground.Commands
{
    class RemoveAccount : AbstractWUndergroundCommand
    {
        public RemoveAccount()
            : base("removeAccount", "Remove the account", "")
        { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return WUndergroundInterface.RemoveAccountCommand(this.Node);
        }
    }
}
