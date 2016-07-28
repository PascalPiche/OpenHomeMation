using OHM.Nodes;
using OpenZWaveDotNet;
using System;
using ZWaveLib.Commands;
using ZWaveLib.Tools;

namespace ZWaveLib.Data
{
    public class ZWaveDriver : ZWaveNode, IZWaveController
    {
        #region Public Ctor

        public ZWaveDriver(string key, string name)
            : base(key, name)
        {
            RegisterProperties();
            RegisterCommands();
        }

        #endregion

        #region Internal Methods

        internal void CreateOrUpdateNode(ZWNotification n)
        {
            var node = GetNode(n);
            if (node == null)
            {
                //Create Node
                CreateNode(n);
            }
            else if (node == this)
            {
                this.UpdateSelf(n);
            } 
            else 
            {
                //Update Node
                node.UpdateNode(n);
            }
        }

        internal void RemoveNode(ZWNotification n)
        {
            string key = NotificationTool.MakeNodeKey(n);
            this.RemoveChild(key);
        }

        internal void SetFatalState()
        {
            this.State = NodeStates.fatal;
        }

        #endregion

        #region Private Methods

        private void UpdateSelfProperties(ZWNotification n)
        {
            this.UpdateProperty("ControllerInterfaceType", ((ZWaveInterface)Parent).Manager.GetControllerInterfaceType(this.HomeId.Value));
            this.UpdateProperty("IsBridgeController", ((ZWaveInterface)Parent).Manager.IsBridgeController(this.HomeId.Value));
            this.UpdateProperty("IsPrimaryController", ((ZWaveInterface)Parent).Manager.IsPrimaryController(this.HomeId.Value));
            this.UpdateProperty("IsStaticUpdateController", ((ZWaveInterface)Parent).Manager.IsStaticUpdateController(this.HomeId.Value));
        }

        private void UpdateSelf(ZWNotification n)
        {
            UpdateSelfProperties(n);
            base.UpdateNode(n);
        }

        private new void RegisterCommands()
        {
            base.RegisterCommands();
            this.RegisterCommand(new ControlerAllOnCommand());
            this.RegisterCommand(new ControlerAllOffCommand());
            this.RegisterCommand(new ControlerSoftResetCommand());
            this.RegisterCommand(new ControlerHardResetCommand());
            this.RegisterCommand(new ControlerAddNodeCommand()); 
        }

        private new void RegisterProperties()
        {
            base.RegisterProperties();
            this.RegisterProperty(new NodeProperty("ControllerInterfaceType",   "Controller Interface Type",    typeof(ZWControllerInterface),  true));
            this.RegisterProperty(new NodeProperty("IsBridgeController",        "Is Bridge Controller",         typeof(Boolean),                true));
            this.RegisterProperty(new NodeProperty("IsPrimaryController",       "Is Primary Controller",        typeof(Boolean),                true));
            this.RegisterProperty(new NodeProperty("IsStaticUpdateController",  "Is Static Update Controller",  typeof(Boolean),                true));
        }

        private ZWaveNode GetNode(ZWNotification n)
        {
            if (n.GetNodeId() == this.NodeId)
            {
                return this;
            }
            return (ZWaveNode)this.FindChild(NotificationTool.MakeNodeKey(n));
        }

        private void CreateNode(ZWNotification n)
        {
            string key = NotificationTool.MakeNodeKey(n);
            string name = NotificationTool.GetNodeName(n, ((ZWaveInterface)this.Interface).Manager);
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();

            ZWaveNode newNode = this.CreateChildNode("zwavenode", key, name) as ZWaveNode;
            newNode.Init(homeId, nodeId);
        }

        #endregion
    }
}
