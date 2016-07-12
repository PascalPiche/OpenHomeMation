using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Data;

namespace WUnderground.Commands
{
    class RefreshCondition : AbstractWUndergroundCommand
    {

         public RefreshCondition(string key, string name, string description)
            : base(key, name, description)
        {

        }

        protected override bool RunImplementation(Dictionary<string, string> arguments)
        {
            return ((StationCondition)this.Node).refresh();
        }
    }
}
