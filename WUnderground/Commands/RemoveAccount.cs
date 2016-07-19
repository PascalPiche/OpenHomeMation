using OHM.Commands;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
