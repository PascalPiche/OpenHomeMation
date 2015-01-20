using OHM.Logger;
using OHM.Nodes;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Tools;

namespace ZWaveLib
{
    class ZWaveNode : NodeAbstract, IZWaveNode
    {

        private uint _homeId;
        private byte _nodeId;
        private ZWManager _manager;

        public uint HomeId
        {
            get { return _homeId; }
        }

        public byte NodeId
        {
            get { return _nodeId; }
        }

        

        private ZWManager Manager
        {
            get
            {
                return _manager;
            }
        }
        
        public ZWaveNode(string key, string name, INode parent, uint homeId, byte nodeId, ZWManager manager)
            : base(key, name, parent)
        {
            _homeId = homeId;
            _nodeId = nodeId;
            _manager = manager;

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeBeamingDevice",
                     "Is Node Beaming Device",
                     typeof(Boolean),
                     Manager.IsNodeBeamingDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeListeningDevice",
                     "Is Node Listening Device",
                     typeof(Boolean),
                     Manager.IsNodeListeningDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeFrequentListeningDevice",
                     "Is Node Frequent Listening Device",
                     typeof(Boolean),
                     Manager.IsNodeFrequentListeningDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeRoutingDevice",
                     "Is Node Routing Device",
                     typeof(Boolean),
                     Manager.IsNodeRoutingDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeSecurityDevice",
                     "Is Node Security Device",
                     typeof(Boolean),
                     Manager.IsNodeSecurityDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeLocation",
                     "Node Location",
                     typeof(Boolean),
                     Manager.GetNodeLocation(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeManufacturerId",
                     "Node Manufacturer Id",
                     typeof(Boolean),
                     Manager.GetNodeManufacturerId(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeManufacturerName",
                     "Node Manufacturer Name",
                     typeof(Boolean),
                     Manager.GetNodeManufacturerName(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeMaxBaudRate",
                     "Node Max Baud Rate",
                     typeof(Boolean),
                     Manager.GetNodeMaxBaudRate(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductId",
                     "Node Product Id",
                     typeof(Boolean),
                     Manager.GetNodeProductId(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductName",
                     "Node Product Name",
                     typeof(Boolean),
                     Manager.GetNodeProductName(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductType",
                     "Node Product Type",
                     typeof(Boolean),
                     Manager.GetNodeProductType(homeId, nodeId)));
        }

        internal void UpdateNode(ZWNotification n)
        {
            string name = NotificationTool.GetNodeName(n, this.Manager);
            
            this.Name = name;

        }

        internal void CreateOrUpdateValue(ZWNotification n)
        {
            
            /*var homeId = n.GetHomeId();
                var nodeId = n.GetNodeId();
                var valueId = n.GetValueID();
                var valueLabel = _mng.GetValueLabel(valueId);
                var valueHelp = _mng.GetValueHelp(valueId);*/
        }

        internal void RemoveValue(ZWNotification n)
        {

        }
       
    }
}
