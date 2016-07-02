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
        #region Public Ctor

        public ZWaveController(string key, string name, ZWaveInterface parent, ILogger logger, NodeStates initialState = NodeStates.initializing)
            : base(key, name, parent, logger, initialState)
        {
            RegisterProperties();
            RegisterCommands();
        }

        #endregion

        #region Internal

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

        /*private void UpdateNode(ZWNotification n)
        {
            var node = GetNode(n);
            if(node != null) {
                if (node == this)
                {
                    UpdateSelf(n);
                }
                else
                {
                    node.UpdateNode(n);
                }  
            }
        }*/

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
            this.RegisterCommand(new ControlerAllOnCommand(this));
            this.RegisterCommand(new ControlerAllOffCommand(this));
            this.RegisterCommand(new ControlerSoftResetCommand(this));
            this.RegisterCommand(new ControlerHardResetCommand(this));
            this.RegisterCommand(new ControlerAddNodeCommand(this)); 
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

        #endregion
    }
}
