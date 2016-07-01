using OHM.Logger;
using OHM.Nodes;
using OpenZWaveDotNet;
using System;
using ZWaveLib.Commands;
using ZWaveLib.Tools;

namespace ZWaveLib.Data
{

    public class ZWaveController : ZWaveNode, IZWaveController
    {
        public ZWaveController(string key, string name, ZWaveInterface parent, ILogger logger, NodeStates initialState = NodeStates.initializing)
            : base(key, name, parent, logger, initialState)
        {
            RegisterProperties();
            RegisterCommands();
        }

        #region Internal

        internal void CreateOrUpdateNode(ZWNotification n)
        {
            var node = GetNode(n);
            if (node == null)
            {
                //Create Node
                CreateNode(n);
            }
            else
            {
                //Update Node
                UpdateNode(node, n);
            }
        }

        internal new void UpdateNode(ZWNotification n)
        {
            var node = GetNode(n);
            if(node != null) {
                if (node == this)
                {
                    UpdateSelf(n);
                }
                else
                {
                    UpdateNode(node, n);
                }  
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

        #region Private

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

        private ZWaveInterface Interface
        {
            get
            {
                return ((ZWaveInterface)this.Parent);
            }
        }

        private void RegisterCommands()
        {
            this.RegisterCommand(new AllOnCommand(this));
            this.RegisterCommand(new AllOffCommand(this));
            this.RegisterCommand(new SoftResetCommand(this));
            this.RegisterCommand(new HardResetCommand(this));

            //parent.Manager.HealNetwork(new HealNetworkCommand(this, parent));
        }

        private void RegisterProperties()
        {
            this.RegisterProperty(
                 new NodeProperty("ControllerInterfaceType", "Controller Interface Type", typeof(ZWControllerInterface), true, ""));

            this.RegisterProperty(
                 new NodeProperty("IsBridgeController", "Is Bridge Controller", typeof(Boolean), true, ""));

            this.RegisterProperty(
                 new NodeProperty("IsPrimaryController", "Is Primary Controller", typeof(Boolean), true, ""));

            this.RegisterProperty(
                 new NodeProperty("IsStaticUpdateController", "Is Static Update Controller", typeof(Boolean), true, ""));
        }

        private ZWaveNode GetNode(ZWNotification n)
        {
            if (n.GetNodeId() == this.NodeId)
            {
                return this;
            }
            return (ZWaveNode)this.GetChild(NotificationTool.MakeNodeKey(n));
        }

        private void CreateNode(ZWNotification n)
        {
            string key = NotificationTool.MakeNodeKey(n);
            string name = NotificationTool.GetNodeName(n, this.Interface.Manager);
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();

            var newNode = new ZWaveNode(key, name, this.Interface, this.Logger, homeId, nodeId);

            this.AddChild(newNode);
        }

        private void UpdateNode(ZWaveNode node, ZWNotification n)
        {
            node.UpdateNode(n);
        }

        #endregion
    }
}
