﻿using OHM.Nodes.Properties;
using System.Collections.Generic;
using WUnderground.Api;

namespace WUnderground.Nodes
{
    public class StationNode : WUndergroundNodeAbstract
    {
        #region Private Members

        private int _zip;
        private int _magic;
        private string _wmo;

        #endregion

        #region Public Ctor

        public StationNode(string keyId, string name, int zip, int magic, string wmo)
            : base(keyId, name)
        {
            _zip = zip;
            _magic = magic;
            _wmo = wmo;
        }

        #endregion

        #region Protected Methods

        protected override bool InitSubChild()
        {
            bool result = true;

            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("station", this);
            this.CreateChildNode("station-condition", base.SystemKey + "-condition", "Condition", options);

            return result;
        }

        protected override void RegisterCommands()
        {
            //No commands
        }

        protected override bool RegisterProperties()
        {
            this.RegisterProperty(new NodeProperty("zip", "zip", typeof(int), true, "", _zip));
            this.RegisterProperty(new NodeProperty("magic", "magic", typeof(int), true, "", _magic));
            this.RegisterProperty(new NodeProperty("wmo", "wmo", typeof(string), true, "", _wmo));

            return true;
        }

        #endregion

        #region Internal Methods

        internal bool GetCondition(StationConditionNode condition)
        {
            bool result = false;
            AccountNode acc = (AccountNode)this.Parent;
            var resultData = WUndergroundApi.QueryConditions(acc.ApiKey, _zip, _magic, _wmo);
            if (resultData != null)
            {
                result = condition.update(resultData);
            }
            return result;
        }

        #endregion
    }
}
