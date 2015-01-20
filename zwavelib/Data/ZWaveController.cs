using OHM.Logger;
using OHM.Nodes;
using OpenZWaveDotNet;
using System;
using ZWaveLib.Tools;

namespace ZWaveLib
{
    /*[Flags]
    public enum ZWaveControllerState {
        initializing,
        ready,
        error
    }*/


    class ZWaveController : ZWaveNode, IZWaveController
    {

        //private ZWaveControllerState state = ZWaveControllerState.initializing;

        public ZWaveController(string key, string name, ZWaveInterface parent, uint homeId, byte nodeId)
            : base(key, name, parent, homeId, nodeId, parent.Manager)
        {
            this.RegisterCommand(new AllOnCommand(this, parent));
            this.RegisterCommand(new AllOffCommand(this, parent));
            this.RegisterCommand(new SoftResetCommand(this, parent));
            this.RegisterCommand(new HardResetCommand(this, parent));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsBridgeController", 
                     "Is Bridge Controller", 
                     typeof(Boolean),
                     parent.Manager.IsBridgeController(homeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsPrimaryController", 
                     "Is Primary Controller", 
                     typeof(Boolean),
                     parent.Manager.IsPrimaryController(homeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsStaticUpdateController", 
                     "Is Static Update Controller", 
                     typeof(Boolean),
                     parent.Manager.IsStaticUpdateController(homeId)));

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

        internal void UpdateNode(ZWNotification n)
        {
            var node = GetNode(n);
            if(node != null) {
                UpdateNode(node, n);
            }
        }

        internal void RemoveNode(ZWNotification n)
        {
            string key = NotificationTool.MakeNodeKey(n);
            this.RemoveChild(key);

        }

        #endregion

        #region Private

        private ZWaveInterface Interface
        {
            get
            {
                return ((ZWaveInterface)this.Parent);
            }
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

            var newNode = new ZWaveNode(key, name, this, homeId, nodeId, this.Interface.Manager);

            this.AddChild(newNode);
        }

        private void UpdateNode(INode node, ZWNotification n)
        {
            ((ZWaveNode)node).UpdateNode(n);
        }

        #endregion
    }
}
