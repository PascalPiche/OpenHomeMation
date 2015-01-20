using OHM.Logger;
using OHM.Nodes;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }

        internal void UpdateNode(String name, ZWNotification n)
        {
            //this.Manager.is
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
