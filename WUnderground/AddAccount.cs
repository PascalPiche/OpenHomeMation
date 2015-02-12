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
           

        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            return false;
            //Interface.CreateAccount();
        }
    }
}
