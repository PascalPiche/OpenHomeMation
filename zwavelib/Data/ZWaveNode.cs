using OHM.Logger;
using OHM.Nodes;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Tools;

namespace ZWaveLib.Data
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
                     true,
                     "",
                     Manager.IsNodeBeamingDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeListeningDevice",
                     "Is Node Listening Device",
                     typeof(Boolean),
                     true,
                     "",
                     Manager.IsNodeListeningDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeFrequentListeningDevice",
                     "Is Node Frequent Listening Device",
                     typeof(Boolean),
                     true,
                     "",
                     Manager.IsNodeFrequentListeningDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeRoutingDevice",
                     "Is Node Routing Device",
                     typeof(Boolean),
                     true,
                     "",
                     Manager.IsNodeRoutingDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "IsNodeSecurityDevice",
                     "Is Node Security Device",
                     typeof(Boolean),
                     true,
                     "",
                     Manager.IsNodeSecurityDevice(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeLocation",
                     "Node Location",
                     typeof(String),
                     false,
                     "",
                     Manager.GetNodeLocation(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeManufacturerId",
                     "Node Manufacturer Id",
                     typeof(String),
                     true,
                     "",
                     Manager.GetNodeManufacturerId(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeManufacturerName",
                     "Node Manufacturer Name",
                     typeof(String),
                     true,
                     "",
                     Manager.GetNodeManufacturerName(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeMaxBaudRate",
                     "Node Max Baud Rate",
                     typeof(uint),
                     true,
                     "",
                     Manager.GetNodeMaxBaudRate(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductId",
                     "Node Product Id",
                     typeof(String),
                     true,
                     "",
                     Manager.GetNodeProductId(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductName",
                     "Node Product Name",
                     typeof(String),
                     true,
                     "",
                     Manager.GetNodeProductName(homeId, nodeId)));

            this.RegisterProperty(
                 new NodeProperty(
                     "NodeProductType",
                     "Node Product Type",
                     typeof(String),
                     true,
                     "",
                     Manager.GetNodeProductType(homeId, nodeId)));
        }

        internal void UpdateNode(ZWNotification n)
        {
            this.Name = NotificationTool.GetNodeName(n, this.Manager);

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
        }

        private Object GetValue(ZWValueID valueId)
        {
            Object result = null;

            switch (valueId.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    bool resultBool;
                    if (Manager.GetValueAsBool(valueId, out resultBool))
                    {
                        result = resultBool;
                    }
                    else
                    {
                        Logger.Error("ZWave Get Value as bool failed");
                    }
                    break;
               
                case ZWValueID.ValueType.Byte:
                    byte resultByte;
                    if (Manager.GetValueAsByte(valueId, out resultByte))
                    {
                        result = resultByte;
                    }
                    else
                    {
                        Logger.Error("ZWave Get Value as Byte failed");
                    }
                    break;
                case ZWValueID.ValueType.Decimal:
                    decimal resultDecimal;
                    //Switch thread language to en for right parsing
                    var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
                    if (Manager.GetValueAsDecimal(valueId, out resultDecimal))
                    {
                        result = resultDecimal;
                    }
                    else
                    {
                        Logger.Error("ZWave Get Value as decimal failed");
                    }
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
                    break;
                case ZWValueID.ValueType.Int:
                    int resultInt;
                    if (Manager.GetValueAsInt(valueId, out resultInt))
                    {
                        result = resultInt;
                    }
                    else
                    {
                        Logger.Error("ZWave Get Value as Int failed");
                    }
                    break;
                case ZWValueID.ValueType.List:
                    Logger.Warn("ZWave Value list not treated. Incomplete implementation");
                    //if (Manager.getv)
                    break;
                
                case ZWValueID.ValueType.Short:
                    short resultShort;
                    if (Manager.GetValueAsShort(valueId, out resultShort))
                    {
                        result = resultShort;
                    }
                    else
                    {
                        Logger.Error("ZWave Get Value as short failed");
                    }
                    break;
                case ZWValueID.ValueType.String:
                    string resultString;
                    if (Manager.GetValueAsString(valueId, out resultString))
                    {
                        result = resultString;
                    }
                    else
                    {
                        Logger.Error("ZWave Get Value as string failed");
                    }
                    break;
                case ZWValueID.ValueType.Button:
                    Logger.Warn("ZWave Button value not treated.  Incomplete implementation");
                    break;
                case ZWValueID.ValueType.Schedule:
                    Logger.Warn("ZWave Schecule value not treated.  Incomplete implementation");
                    break;
                case ZWValueID.ValueType.Raw:
                    Logger.Warn("ZWave Raw value not treated.  Incomplete implementation");
                    break;
                default:
                    Logger.Error("ZWave Value Type Unknow!");
                    break;
            }
            return result;
        }
        
        internal bool CreateOrUpdateValue(ZWNotification n)
        {
            ZWValueID valueId = n.GetValueID();
            
            Object value = GetValue(valueId);
        
            
            if (this.ContainsProperty(valueId.GetId().ToString())) {
                string units = Manager.GetValueUnits(valueId);
                this.UpdateProperty(valueId.GetId().ToString(), value);
                return true;

            } else if (value != null){
                String valueLabel = Manager.GetValueLabel(valueId);
                String valueHelp = Manager.GetValueHelp(valueId);
                bool isReadOnly = Manager.IsValueReadOnly(valueId);
                //var units = Manager.GetValueUnits(valueId);
                if (this.RegisterProperty(new NodeProperty(valueId.GetId().ToString(), valueLabel, value.GetType(), isReadOnly, valueHelp, value)))
                {
                    Logger.Info("Zwave Registered property : " + valueLabel + "for nodeId: " + this.NodeId);
                    return true;
                }
                else
                {
                    Logger.Error("Zwave Cannot Register property : " + valueLabel);
                }
            }
            return false;  
        }

        internal void RemoveValue(ZWNotification n)
        {
            ZWValueID valueId = n.GetValueID();
            string key = valueId.GetId().ToString();
            if (this.ContainsProperty(key))
            {
                if (this.UnRegisterProperty(valueId.GetId().ToString()))
                {
                    Logger.Error("ZWave Cannot UnRegister property : " + key);
                }
            }
            else
            {
                Logger.Error("ZWave Cannot find property for unregister: " + key);
            }

        }

        private new void UpdateProperty(string key, object value)
        {
            if (!base.UpdateProperty(key, value))
            {
                Logger.Error("ZWave Cannot Update property : " + key + " with value " + value.ToString());
            }
        }
    
        
    }
}
