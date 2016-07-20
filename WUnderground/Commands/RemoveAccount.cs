using System.Collections.Generic;
using WUnderground.Data;

namespace WUnderground.Commands
{
    class RemoveAccount : AbstractWUndergroundCommand
    {

        private Account Account 
        {
            get
            {
                return (Account)this.Node;
            }
        }

        public RemoveAccount()
            : base("removeAccount", "Remove the account", "")
        {
            
        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return WUndergroundInterface.RemoveAccountCommand(Account);
        }
    }
}
