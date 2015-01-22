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
        
        public ZWaveNode(string key, string name, INode parent, uint homeId, byte nodeId, ZWManager manager, ILogger logger)
            : base(key, name, parent, logger)
        {
            _homeId = homeId;
            _nodeId = nodeId;
            _manager = manager;

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeBeamingDevice",
                     "Is Node Beaming Device",
                     typeof(Boolean),
                     "",
                     Manager.IsNodeBeamingDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeListeningDevice",
                     "Is Node Listening Device",
                     typeof(Boolean),
                     "",
                     Manager.IsNodeListeningDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeFrequentListeningDevice",
                     "Is Node Frequent Listening Device",
                     typeof(Boolean),
                     "",
                     Manager.IsNodeFrequentListeningDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeRoutingDevice",
                     "Is Node Routing Device",
                     typeof(Boolean),
                     "",
                     Manager.IsNodeRoutingDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeSecurityDevice",
                     "Is Node Security Device",
                     typeof(Boolean),
                     "",
                     Manager.IsNodeSecurityDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeLocation",
                     "Node Location",
                     typeof(Boolean),
                     "",
                     Manager.GetNodeLocation(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeManufacturerId",
                     "Node Manufacturer Id",
                     typeof(String),
                     "",
                     Manager.GetNodeManufacturerId(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeManufacturerName",
                     "Node Manufacturer Name",
                     typeof(String),
                     "",
                     Manager.GetNodeManufacturerName(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeMaxBaudRate",
                     "Node Max Baud Rate",
                     typeof(uint),
                     "",
                     Manager.GetNodeMaxBaudRate(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductId",
                     "Node Product Id",
                     typeof(String),
                     "",
                     Manager.GetNodeProductId(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductName",
                     "Node Product Name",
                     typeof(String),
                     "",
                     Manager.GetNodeProductName(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductType",
                     "Node Product Type",
                     typeof(String),
                     "",
                     Manager.GetNodeProductType(homeId, nodeId)));
        }

        internal void UpdateNode(ZWNotification n)
        {
            string name = NotificationTool.GetNodeName(n, this.Manager);
            
            this.UpdateProperty("NodeProductType", Manager.GetNodeProductType(this.HomeId, this.NodeId));
            this.UpdateProperty("NodeProductName", Manager.GetNodeProductName(this.HomeId, this.NodeId));
            this.UpdateProperty("NodeProductId", Manager.GetNodeProductId(this.HomeId, this.NodeId));
            this.UpdateProperty("NodeMaxBaudRate", Manager.GetNodeMaxBaudRate(this.HomeId, this.NodeId));
            this.UpdateProperty("NodeManufacturerName", Manager.GetNodeManufacturerName(this.HomeId, this.NodeId));
            this.UpdateProperty("NodeManufacturerId", Manager.GetNodeManufacturerId(this.HomeId, this.NodeId));
            this.UpdateProperty("NodeLocation", Manager.GetNodeLocation(this.HomeId, this.NodeId));
            this.UpdateProperty("IsNodeSecurityDevice", Manager.IsNodeSecurityDevice(this.HomeId, this.NodeId));
            this.UpdateProperty("IsNodeRoutingDevice", Manager.IsNodeRoutingDevice(this.HomeId, this.NodeId));
            this.UpdateProperty("IsNodeFrequentListeningDevice", Manager.IsNodeFrequentListeningDevice(this.HomeId, this.NodeId));
            this.UpdateProperty("IsNodeListeningDevice", Manager.IsNodeListeningDevice(this.HomeId, this.NodeId));
            this.UpdateProperty("IsNodeBeamingDevice", Manager.IsNodeBeamingDevice(this.HomeId, this.NodeId));

            this.Name = name;

        }

        private Object GetValue(ZWValueID valueId)
        {

            bool resultBool;
            byte resultByte;
            decimal resultDecimal;
            int resultInt;
            short resultShort;
            string resultString;
            Object result = null;

            switch (valueId.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    if (Manager.GetValueAsBool(valueId, out resultBool))
                    {
                        result = resultBool;
                    }
                    break;
               
                case ZWValueID.ValueType.Byte:
                    if (Manager.GetValueAsByte(valueId, out resultByte))
                    {
                        result = resultByte;
                    }
                    break;
                case ZWValueID.ValueType.Decimal:
                    //Switch thread language to en for right parsing
                    var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
                    if (Manager.GetValueAsDecimal(valueId, out resultDecimal))
                    {
                        result = resultDecimal;
                    }
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
                    break;
                case ZWValueID.ValueType.Int:
                    if (Manager.GetValueAsInt(valueId, out resultInt))
                    {
                        result = resultInt;
                    }
                    break;
                case ZWValueID.ValueType.List:
                    break;
                
                case ZWValueID.ValueType.Short:
                    if (Manager.GetValueAsShort(valueId, out resultShort))
                    {
                        result = resultShort;
                    }
                    break;
                case ZWValueID.ValueType.String:
                    if (Manager.GetValueAsString(valueId, out resultString))
                    {
                        result = resultString;
                    }
                    break;
                case ZWValueID.ValueType.Button:
                    break;
                case ZWValueID.ValueType.Schedule:
                    break;
                case ZWValueID.ValueType.Raw:
                    break;
                default:
                    break;
            }
            return result;
        }
        
        internal bool CreateOrUpdateValue(ZWNotification n)
        {
            ZWValueID valueId = n.GetValueID();
            String valueLabel = Manager.GetValueLabel(valueId);
            String valueHelp = Manager.GetValueHelp(valueId);
            Object value = GetValue(valueId);
        
            
            if (this.ContainsProperty(valueId.GetId().ToString())) {
                string units = Manager.GetValueUnits(valueId);
                this.UpdateProperty(valueId.GetId().ToString(), value);
                return true;

            } else if (value != null){
                //var units = Manager.GetValueUnits(valueId);
                if (!this.RegisterProperty(new NodeProperty(valueId.GetId().ToString(), valueLabel, value.GetType(), valueHelp, value)))
                {
                    Logger.Error("Zwave Cannot Register property : " + valueLabel);
                    return false;
                }
                return true;
            }
            return false;  
        }

        internal void RemoveValue(ZWNotification n)
        {

        }

        private void UpdateProperty(string key, object value)
        {
            if (!base.UpdateProperty(key, value))
            {
                Logger.Error("ZWave Cannot Update property : " + key + " with value " + value.ToString());
            }
        }
    
        
    }
}
