using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Api;

namespace WUnderground.Data
{
    public class Station : WUndergroundNodeAbstract
    {

        private int _zip;
        private int _magic;
        private string _wmo;


        public Station(string keyId, string name, int zip, int magic, string wmo)
            : base(keyId, name)
        {

            _zip = zip;
            _magic = magic;
            _wmo = wmo;

            this.RegisterProperty(
               new NodeProperty(
                   "zip",
                   "zip",
                   typeof(int),
                   true,
                   "",
                  _zip));

            this.RegisterProperty(
               new NodeProperty(
                   "magic",
                   "magic",
                   typeof(int),
                   true,
                   "",
                  _magic));

            this.RegisterProperty(
               new NodeProperty(
                   "wmo",
                   "wmo",
                   typeof(string),
                   true,
                   "",
                  _wmo));

            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("station", this);
            this.CreateChildNode("station-condition", keyId + "-condition", "Condition", options);
            //  new StationCondition(keyId + "-condition", "Condition", this));
            
        }

        internal bool GetCondition(StationCondition condition)
        {
            Account acc = (Account)this.Parent;
            return condition.update(WUndergroundApi.QueryConditions(acc.ApiKey, _zip, _magic, _wmo));
        }
    }
}
