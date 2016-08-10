using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.Nodes.Properties;
using OpenZWaveDotNet;
using System;
using System.Collections.ObjectModel;
using ZWaveLib.Commands;

namespace ZWaveLib.Nodes
{
    public abstract class ZWaveHomeNode : ALRAbstractTreeNode, IZWaveHomeNode
    {
        private uint _homeId;

        public ZWaveHomeNode(string key, string name)
            : base(key, name) {}

        public uint? HomeId
        {
            get { return _homeId; }
        }

        internal bool AssignZWaveId(uint homeId)
        {
            _homeId = homeId;
            return true;
        }
    }

    public class ZWaveControler : ZWaveNode
    {
        public ZWaveControler(string key, string name)
            : base(key, name) {}
    }

    public class ZWaveNode : ZWaveHomeNode, IZWaveNode
    {
        private byte? _nodeId = new byte?();

        #region Public properties

        public byte? NodeId
        {
            get { return _nodeId; }
        }

        #endregion

        #region Public Ctor

        public ZWaveNode(string key, string name)
            : base(key, name) { }

        #endregion

        #region Internal method

        internal bool AssignZWaveId(uint homeId, byte nodeId)
        {
            bool result = false;
            if (base.AssignZWaveId(homeId))
            {
                _nodeId = nodeId;
                UpdateZWaveNodeProperties();
                State = NodeStates.normal;
                result = true;
            }
            return result;
        }

        internal void UpdateNode(ZWNotification n)
        {
            UpdateZWaveNodeProperties();
        }

        internal bool CreateOrUpdateValue(ZWNotification n)
        {
            ZWValueID valueId = n.GetValueID();
            Object value = GetValue(valueId);
            bool result = false;

            //Grouping values by Command Class
            byte commandClassId = valueId.GetCommandClassId();
            //if (this.Children)
            if (this.ContainProperty(valueId.GetId().ToString()))
            {
                //string units = Manager.GetValueUnits(valueId);
                //Manager.GetPollIntensity
                //Manager.IsPolled
                //Manager.IsValueSet
                //Manager.IsValuePolled
                this.UpdateProperty(valueId.GetId().ToString(), value);
                result = true;
            }
            else if (value != null)
            {
                result = CreateValue(n);
            }
            return result;
        }

        internal bool RemoveValue(ZWNotification n)
        {
            ZWValueID valueId = n.GetValueID();
            string key = valueId.GetId().ToString();
            bool result = false;

            if (this.ContainProperty(key))
            {
                if (this.UnRegisterProperty(valueId.GetId().ToString()))
                {
                    Logger.Info("ZWave: Removed property : " + key);
                }
            }
            else
            {
                Logger.Error("ZWave: Cannot find property " + key + " for removing");
            }
            return result;

        }

        #endregion

        #region Private

        private ZWManager Manager
        {
            get
            {
                return ((ZWaveInterface)base.Interface).Manager;
            }
        }

        private void UpdateZWaveNodeProperties()
        {
            this.UpdateProperty("IsNodeAwake", Manager.IsNodeAwake(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeBeamingDevice", Manager.IsNodeBeamingDevice(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeFailed", Manager.IsNodeFailed(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeFrequentListeningDevice", Manager.IsNodeFrequentListeningDevice(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeInfoReceived", Manager.IsNodeInfoReceived(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeListeningDevice", Manager.IsNodeListeningDevice(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeRoutingDevice", Manager.IsNodeRoutingDevice(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("IsNodeSecurityDevice", Manager.IsNodeSecurityDevice(this.HomeId.Value, this.NodeId.Value));


            //Manager.GetNodeClassInformation(homeId, nodeId, )
            //Manager.GetNodeGeneric(homeId, nodeId)
            //Manager.GetNodeBasic(homeId, nodeId)

            this.UpdateProperty("NodeLocation", Manager.GetNodeLocation(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeManufacturerId", Manager.GetNodeManufacturerId(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeManufacturerName", Manager.GetNodeManufacturerName(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeMaxBaudRate", Manager.GetNodeMaxBaudRate(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeName", Manager.GetNodeName(this.HomeId.Value, this.NodeId.Value));

            //Manager.GetNodeNeighbors(homeId, nodeId,...) TODO

            this.UpdateProperty("NodeProductId", Manager.GetNodeProductId(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeProductName", Manager.GetNodeProductName(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeProductType", Manager.GetNodeProductType(this.HomeId.Value, this.NodeId.Value));

            //Manager.GetNodeQueryStage(homeId, nodeId) TODO
            //Manager.GetNodeSecurity(homeId, nodeId) TODO
            //Manager.GetNodeSpecific(homeId,nodeId) TODO

            this.UpdateProperty("NodeType", Manager.GetNodeType(this.HomeId.Value, this.NodeId.Value));
            this.UpdateProperty("NodeVersion", Manager.GetNodeVersion(this.HomeId.Value, this.NodeId.Value));

            //Manager.GetNumGroups(homeId, nodeId) TODO
            //Manager.HasNodeFailed(homeId, nodeId) TODO (STANDBY)
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
                        Logger.Error("Get Value as bool failed");
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
                        Logger.Error("ZWave: Get Value as Byte failed");
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
                        Logger.Error("ZWave: Get Value as decimal failed");
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
                        Logger.Error("ZWave: Get Value as Int failed");
                    }
                    break;
                case ZWValueID.ValueType.List:
                    //Logger.Warn("ZWave: Value list not treated. Incomplete implementation");
                    string value;
                    Manager.GetValueListSelection(valueId, out value);

                    result = value;
                    
                    break;
                
                case ZWValueID.ValueType.Short:
                    short resultShort;
                    if (Manager.GetValueAsShort(valueId, out resultShort))
                    {
                        result = resultShort;
                    }
                    else
                    {
                        Logger.Error("ZWave: Get Value as short failed");
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
                        Logger.Error("ZWave: Get Value as string failed");
                    }
                    break;
                case ZWValueID.ValueType.Button:
                    Logger.Warn("ZWave: Button value not treated.  Incomplete implementation");
                    break;
                case ZWValueID.ValueType.Schedule:
                    Logger.Warn("ZWave: Schecule value not treated.  Incomplete implementation");
                    break;
                case ZWValueID.ValueType.Raw:
                    Logger.Warn("ZWave: Raw value not treated.  Incomplete implementation");
                    break;
                default:
                    Logger.Error("ZWave: Value Type Unknow!");
                    break;
            }
            return result;
        }

        private bool CreateValue(ZWNotification n)
        {
            ZWValueID valueId = n.GetValueID();
            Object value = GetValue(valueId);

            String valueLabel = Manager.GetValueLabel(valueId);
            String valueHelp = Manager.GetValueHelp(valueId);
            bool isReadOnly = Manager.IsValueReadOnly(valueId);
            string units = Manager.GetValueUnits(valueId);

            ObservableCollection<INodeProperty> extraInfo = new ObservableCollection<INodeProperty>();
            extraInfo.Add(new NodeProperty("units", "Units", typeof(string), true, "", units));
            extraInfo.Add(new NodeProperty("commandClassId", "Command Class Id", typeof(byte), true, "", valueId.GetCommandClassId()));
            extraInfo.Add(new NodeProperty("genre", "Genre", typeof(OpenZWaveDotNet.ZWValueID.ValueGenre), true, "", valueId.GetGenre()));
            extraInfo.Add(new NodeProperty("index", "Index", typeof(byte), true, "", valueId.GetIndex()));
            extraInfo.Add(new NodeProperty("instance", "Instance", typeof(byte), true, "", valueId.GetInstance()));

            if (this.RegisterProperty(
                    new NodeProperty(
                    valueId.GetId().ToString(), 
                    valueLabel, 
                    value.GetType(), 
                    isReadOnly, 
                    valueHelp, 
                    value,
                    extraInfo
                    )
                ))
            {
                Logger.Info("Zwave: Added property: " + valueLabel + " for nodeId: " + this.NodeId);
                return true;
            }
            else
            {
                Logger.Error("Zwave: Cannot Add property: " + valueLabel + " for nodeId: " + this.NodeId);
            }
            return false;
        }

        private new void UpdateProperty(string key, object value)
        {
            if (!base.UpdateProperty(key, value))
            {
                Logger.Error("ZWave Cannot Update property : " + key + " with value " + value.ToString());
            }
        }

        #endregion

        protected override void RegisterCommands()
        {
            this.RegisterCommand(new RefreshNodeCommand());

            //this.RegisterCommand(new RefreshNodeValueCommand(this));
        }

        protected override void RegisterProperties()
        {
            //Manager.GetAllScenes
            //Manager.GetAssociations

            //Manager.GetNodeClassInformation(homeId, nodeId...) TODO
            //Manager.GetNodeGeneric(homeId, nodeId) TODO
            //Manager.GetNodeBasic(homeId, nodeId) TODO
            //Manager.GetNodeQueryStage(homeId, nodeId) TODO
            //Manager.GetNodeSecurity(homeId, nodeId) TODO
            //Manager.GetNodeSpecific(homeId,nodeId) TODO
            //Manager.GetNumGroups(homeId, nodeId) TODO
            //Manager.HasNodeFailed(homeId, nodeId) TODO (STANDBY)

            this.RegisterProperty(new NodeProperty("IsNodeAwake", "Is Node Awake", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeBeamingDevice", "Is Node Beaming Device", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeFailed", "Is Node Failed", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeFrequentListeningDevice", "Is Node Frequent Listening Device", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeInfoReceived", "Is Node Info Received", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeListeningDevice", "Is Node Listening Device", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeRoutingDevice", "Is Node Routing Device", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("IsNodeSecurityDevice", "Is Node Security Device", typeof(Boolean), true));
            this.RegisterProperty(new NodeProperty("NodeLocation", "Node Location", typeof(String), false));
            this.RegisterProperty(new NodeProperty("NodeManufacturerId", "Node Manufacturer Id", typeof(String), true));
            this.RegisterProperty(new NodeProperty("NodeManufacturerName", "Node Manufacturer Name", typeof(String), true));
            this.RegisterProperty(new NodeProperty("NodeMaxBaudRate", "Node Max Baud Rate", typeof(uint), true));
            this.RegisterProperty(new NodeProperty("NodeName", "Node Name", typeof(String), true));

            //Manager.GetNodeNeighbors(homeId, nodeId,...) TODO

            this.RegisterProperty(new NodeProperty("NodeProductId", "Node Product Id", typeof(String), true));
            this.RegisterProperty(new NodeProperty("NodeProductName", "Node Product Name", typeof(String), true));
            this.RegisterProperty(new NodeProperty("NodeProductType", "Node Product Type", typeof(String), true));
            this.RegisterProperty(new NodeProperty("NodeType", "Node Type", typeof(String), true));
            this.RegisterProperty(new NodeProperty("NodeVersion", "Node Version", typeof(byte), true));
        }
    }
}
