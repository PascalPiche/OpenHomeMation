using OHM.Nodes;
using System.Collections.Generic;
using WUnderground.Api;

namespace WUnderground.Nodes
{
    public class StationNode : WUndergroundNodeAbstract
    {

        private int _zip;
        private int _magic;
        private string _wmo;

        public StationNode(string keyId, string name, int zip, int magic, string wmo)
            : base(keyId, name)
        {
            _zip = zip;
            _magic = magic;
            _wmo = wmo;
        }

        protected override bool Initing()
        {
            bool result = false;
            result = base.Initing();

            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("station", this);
            this.CreateChildNode("station-condition", base.Key + "-condition", "Condition", options);

            return result;
        }

        internal bool GetCondition(StationConditionNode condition)
        {
            AccountNode acc = (AccountNode)this.Parent;
            return condition.update(WUndergroundApi.QueryConditions(acc.ApiKey, _zip, _magic, _wmo));
        }

        protected override void RegisterCommands()
        {
            //No commands
        }

        protected override void RegisterProperties()
        {
            this.RegisterProperty(new NodeProperty("zip", "zip", typeof(int), true, "", _zip));
            this.RegisterProperty(new NodeProperty("magic", "magic", typeof(int), true, "", _magic));
            this.RegisterProperty(new NodeProperty("wmo", "wmo", typeof(string), true, "", _wmo));
        }
    }
}
