using OpenZWaveDotNet;
using System.Collections.Generic;
using ZWaveLib.Commands;

namespace ZWaveLib.Nodes
{
    public class ZWaveNodesContainer : ZWaveHomeNode
    {
        #region Private Members

        private byte _controlerNodeId;

        #endregion

        #region Public Ctor

        public ZWaveNodesContainer(string key, string name, byte controlerNodeId) 
            : base(key, name) 
        {
            _controlerNodeId = controlerNodeId;
        }

        #endregion

        #region Protected Methods

        protected override void RegisterCommands()
        {
            this.RegisterCommand(new ControlerAllOnCommand());
            this.RegisterCommand(new ControlerAllOffCommand());
            this.RegisterCommand(new ControlerAddNodeCommand());
        }

        protected override bool RegisterProperties()
        { return true; }

        #endregion

        #region Internal Methods

        internal new bool AssignZWaveId(uint homeId)
        {
            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("controlerNodeId", _controlerNodeId);
            ZWaveControler newNode = this.CreateChildNode("zwaveControler", _controlerNodeId.ToString(), "controler", options) as ZWaveControler;
            newNode.AssignZWaveId(homeId, _controlerNodeId);
            return true;
        }
        
        internal void CreateOrUpdateNode(ZWNotification n)
        {
            ZWaveNode node = this.FindDirectChild(n.GetNodeId().ToString()) as ZWaveNode;
            if (node != null)
            {
                //Update Node
                node.UpdateNode(n);
            }
            else
            {
                //Create Node
                ZWaveNode newNode = this.CreateChildNode("zwaveNode", n.GetNodeId().ToString(), "Unknow device") as ZWaveNode;
                newNode.AssignZWaveId(n.GetHomeId(), n.GetNodeId());
            }
        }

        internal void CreateOrUpdateValue(ZWNotification n)
        {
            ZWaveNode node = this.FindDirectChild(n.GetNodeId().ToString()) as ZWaveNode;
            if (node != null)
            {
                node.CreateOrUpdateValue(n);
            }
            else
            {
                Logger.Error("Node not found for Create Or Update Value");
            }
        }

        #endregion
    }
}
