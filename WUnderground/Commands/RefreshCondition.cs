using System.Collections.Generic;
using WUnderground.Nodes;

namespace WUnderground.Commands
{
    class RefreshCondition : AbstractWUndergroundCommand
    {
         public RefreshCondition(string key, string name, string description)
            : base(key, name, description)
        { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return ((StationConditionNode)this.Node).refresh();
        }
    }
}
