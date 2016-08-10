using OHM.Nodes;
using OHM.Nodes.ALR;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using ZWaveLib.Commands;
using ZWaveLib.Nodes;
using ZWaveLib.Tools;

namespace ZWaveLib
{
    public class ZWaveInterface : ALRInterfaceAbstractNode
    {
        #region Private members

        private ZWManager _mng;
        private Dictionary<uint, string> _driverControlerPathMapping = new Dictionary<uint, string>();
        private Dictionary<string, ZWaveDriverControlerRalNode> _runningDriverControlers = new Dictionary<string, ZWaveDriverControlerRalNode>();
        private Dispatcher _dispatcher;

        #endregion

        #region Public Ctor

        public ZWaveInterface()
            : base("ZWaveInterface", "ZWave")
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }
            
        #endregion

        #region public API

        protected override void Start()
        {
            var apiPath = @"C:\Users\Scopollif\Documents\Visual Studio 2013\Projects\OpenHomeMation\external\open-zwave\openzwave-1.0.791";
            ZWOptions opt = new ZWOptions();
            opt.Create(apiPath + @"\config\", apiPath + @"", @"");
            //Lock the options
            opt.Lock();

            // Create the OpenZWave Manager
            _mng = new ZWManager();
            _mng.Create();
            _mng.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
            _mng.OnControllerStateChanged += new ManagedControllerStateChangedHandler(ControllerStateChangedHandler);

            //Manager.GetLoggingState
            //Manager.SetLoggingState
            //Manager.SetLogFileName

            //Log OpenZWave version
            Logger.Debug("OpenZWave Version: " + _mng.GetVersionAsString());
            
            //Load registered controllers
            LoadRegisteredControllers();
        }

        protected override void Shutdown()
        {
            _mng.Destroy();
            _mng = null;
        }

        #endregion

        #region internal API

        internal bool CreateNewController(int port)
        {
            return CreateController(port, true);
        }

        internal void RemoveController(int port)
        {
            Logger.Debug("Removing Driver Controller on port: " + port);
            if (_mng.RemoveDriver(@"\\.\COM" + port))
            {
                Logger.Info("Removing Driver Controller on port: " + port + " was successfull");
            }
            else
            {
                Logger.Error("Removing Driver Controller on port: " + port + " was not successfull");
            }
        }

        internal ZWManager Manager
        {
            get
            {
                return _mng;
            }
        }
        
        #endregion

        #region private Methods

        #region Manager Handler

        private void ProcessZWNotificationTypeSwitch(ZWNotification n)
        {
            switch (n.GetType())
            {
                //Driver 
                case ZWNotification.Type.DriverReady:
                    NotificationDriverReady(n);
                    break;
                case ZWNotification.Type.DriverFailed:
                    NotificationDriverFailed(n);
                    break;
                case ZWNotification.Type.DriverReset:
                    NotificationDriverReset(n);
                    break;
                case ZWNotification.Type.DriverRemoved:
                    NotificationDriverRemoved(n);
                    break;

                //Initialize Query
                case ZWNotification.Type.AwakeNodesQueried:
                    NotificationAwakeNodesQueried(n);
                    break;
                case ZWNotification.Type.AllNodesQueried:
                    NotificationAllNodesQueried(n);
                    break;
                case ZWNotification.Type.AllNodesQueriedSomeDead:
                    NotificationAllNodesQueriedSomeDead(n);
                    break;


                //Node
                case ZWNotification.Type.NodeNew:
                    NotificationNodeNew(n);
                    break;
                case ZWNotification.Type.NodeAdded:
                    NotificationNodeAdded(n);
                    break;
                case ZWNotification.Type.EssentialNodeQueriesComplete:
                    NotificationEssentialNodeQueriesComplete(n);
                    break;
                case ZWNotification.Type.NodeQueriesComplete:
                    NotificationNodeQueriesComplete(n);
                    break;

                case ZWNotification.Type.NodeRemoved:
                    NotificationNodeRemoved(n);
                    break;

                case ZWNotification.Type.NodeEvent:
                    NotificationNodeEvent(n);
                    break;
                case ZWNotification.Type.NodeNaming:
                    NotificationNodeNaming(n);
                    break;
                case ZWNotification.Type.NodeProtocolInfo:
                    NotificationNodeProtocolInfo(n);
                    break;

                //Value
                case ZWNotification.Type.ValueAdded:
                    NotificationValueAdded(n);
                    break;
                case ZWNotification.Type.ValueChanged:
                    NotificationValueChanged(n);
                    break;
                case ZWNotification.Type.ValueRefreshed:
                    NotificationValueRefreshed(n);
                    break;
                case ZWNotification.Type.ValueRemoved:
                    NotificationValueRemoved(n);
                    break;

                //Notification
                case ZWNotification.Type.Notification:
                    Notification(n);
                    break;

                //Scene
                case ZWNotification.Type.SceneEvent:
                    NotificationSceneEvent(n);
                    break;

                //Group
                case ZWNotification.Type.Group:
                    NotificationGroup(n);
                    break;

                //Polling
                case ZWNotification.Type.PollingDisabled:

                    break;
                case ZWNotification.Type.PollingEnabled:

                    break;

                //button
                case ZWNotification.Type.ButtonOff:
                    break;
                case ZWNotification.Type.ButtonOn:
                    break;
                case ZWNotification.Type.CreateButton:
                    break;
                case ZWNotification.Type.DeleteButton:
                    break;
            }
        }

        private void NotificationHandlerThreadSafe(ZWNotification n)
        {
            // Log Base Notification Value
            Logger.Debug("ZWave: ReceivingNotification : " + 
                "Code -> " + n.GetCode() + 
                " | Type -> " + n.GetType() +
                " | HomeId -> " + n.GetHomeId() +
                " | NodeId -> " + n.GetNodeId() + 
                " | ValueId.CommandClassId -> " + n.GetValueID().GetCommandClassId() +
                " | ValueId.Genre -> " + n.GetValueID().GetGenre() +
                " | ValueId.HomeId -> " + n.GetValueID().GetHomeId() + 
                " | ValueId.Id -> " + n.GetValueID().GetId() +
                " | ValueId.Index -> " + n.GetValueID().GetIndex() +
                " | ValueId.Instance -> " + n.GetValueID().GetInstance() +
                " | ValueId.NodeId -> " + n.GetValueID().GetNodeId() +
                " | ValueId.Type -> " + n.GetValueID().GetType() +
                " | GroupIdx -> " + n.GetGroupIdx() +
                " | Event -> " + n.GetEvent() +
                " | Byte -> " + n.GetByte());

            switch (n.GetCode())
            {
                case ZWNotification.Code.Alive:
                    Logger.Warn("TODO: Receiving Notification Alive code");
                    break;
                case ZWNotification.Code.Awake:
                    Logger.Warn("TODO: Receiving Notification Awake code");
                    break;
                case ZWNotification.Code.Dead:
                    Logger.Warn("TODO: Receiving Notification Dead code");
                    break;
                case ZWNotification.Code.MsgComplete:
                    Logger.Debug("Receiving Notification Message complete code");
                    ProcessZWNotificationTypeSwitch(n);
                    break;
                case ZWNotification.Code.NoOperation:
                    Logger.Warn("TODO: Receiving Notification No Operation code");
                    break;
                case ZWNotification.Code.Sleep:
                    Logger.Warn("TODO: Receiving Notification Sleep code");
                    break;
                case ZWNotification.Code.Timeout:
                    Logger.Warn("TODO: Receiving Notification Timeout code");
                    break;
            }
        }

        private void NotificationHandler(ZWNotification n)
        {
            //Make sure we are on the right thread before adding the property
            _dispatcher.Invoke((Action)(() =>
            {
                NotificationHandlerThreadSafe(n);
            }));
        }

        private void ControllerStateChangedHandler(ZWControllerState state)
        {
            Logger.Warn("TODO: Controller State Changed:  " + state.ToString());
            switch (state)
            {
                case ZWControllerState.Cancel:
                    
                    break;
                case ZWControllerState.Completed:

                    break;
                case ZWControllerState.Error:

                    break;
                case ZWControllerState.Failed:

                    break;
                case ZWControllerState.InProgress:

                    break;
                case ZWControllerState.NodeFailed:

                    break;
                case ZWControllerState.NodeOK:

                    break;
                case ZWControllerState.Normal:

                    break;
                case ZWControllerState.Sleeping:

                    break;
                case ZWControllerState.Starting:

                    break;
                case ZWControllerState.Waiting:

                    break;
                default:
                    break;
            }
            
        }

        #endregion

        #region NotificationHandler

        #region Driver

        private void NotificationDriverReady(ZWNotification n)
        {
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();

            string driverControlerPath = Manager.GetControllerPath(homeId);
            Logger.Debug("Processing notification Driver Ready with Controler Path : " + driverControlerPath);

            if (this._runningDriverControlers.ContainsKey(driverControlerPath))
            {
                ZWaveDriverControlerRalNode driverControler = null;
                if (this._runningDriverControlers.TryGetValue(driverControlerPath, out driverControler))
                {
                    //Update controllerPathMapping
                    _driverControlerPathMapping.Add(homeId, driverControlerPath);

                    //Assign Home Id to the Driver Controller
                    driverControler.AssignHomeId(homeId, nodeId);

                    Logger.Info("Processing notification Driver Controler Ready with Controler Path : " + driverControlerPath + " was successfull");
                }
            }
        }

        private void NotificationDriverReset(ZWNotification n)
        {
            Logger.Warn("TODO: Notification Driver Reset: NodeId=" + GetNodeIdForLog(n));
        }

        private void NotificationDriverFailed(ZWNotification n)
        {
            uint homeId = n.GetHomeId();
            string DriverControlerPath = Manager.GetControllerPath(homeId);
            ZWaveDriverControlerRalNode driverControler = null;
            Logger.Debug("Processing Notification Driver Failed with ControlerPath " + DriverControlerPath);

            if (this._runningDriverControlers.TryGetValue(DriverControlerPath, out driverControler))
            {
                driverControler.SetFatalState();
                Logger.Debug("Processing Notification Driver Failed with ControlerPath " + DriverControlerPath);
            }
            else
            {
                Logger.Error("Notification Driver Failed: Controller not found in the running Controllers and it not suppose to append");
            }
        }

        private void NotificationDriverRemoved(ZWNotification n)
        {
            uint homeId = n.GetHomeId();
            string controlerPath = _driverControlerPathMapping[homeId].Clone() as string;
            ZWaveDriverControlerRalNode controler = null;

            Logger.Debug("Notification Driver removing: ControllerPath=" + controlerPath);
            if (this._runningDriverControlers.TryGetValue(controlerPath, out controler))
            {
                this.RemoveChild(controler.Key);
                this._runningDriverControlers.Remove(controlerPath);
                _driverControlerPathMapping.Remove(homeId);

                Logger.Info("Notification Driver Removed completed with success: ControllerPath=" + controlerPath);
            } else {
                Logger.Error("Notification Driver Removed: Controller not found in the running Controllers and it not suppose to append");
            }
        }

        #endregion

        #region Initialize query

        private void NotificationAllNodesQueried(ZWNotification n)
        {
            Logger.Warn("TODO: Notification All Nodes Queried: " + GetNodeIdForLog(n));
        }

        private void NotificationAwakeNodesQueried(ZWNotification n)
        {
            Logger.Warn("TODO: Notification Awake Nodes Queried: " + GetNodeIdForLog(n));
        }

        private void NotificationAllNodesQueriedSomeDead(ZWNotification n)
        {
            Logger.Warn("TODO: Notification All Nodes Queried Some Dead: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Node

        private void NotificationNodeNew(ZWNotification n)
        {
            Logger.Debug("Notification New Node: NodeId=" + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeAdded(ZWNotification n)
        {
            //Update State
            Logger.Debug("Notification Node Added: NodeId=" + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeRemoved(ZWNotification n)
        {
            Logger.Debug("Notification Node Removed: NodeId=" + GetNodeIdForLog(n));
            RemoveNode(n);
        }

        private void NotificationNodeProtocolInfo(ZWNotification n)
        {
            Logger.Debug("Notification Node Protocol Info: NodeId=" + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationEssentialNodeQueriesComplete(ZWNotification n)
        {
            Logger.Debug("Notification Essential Node Queries Complete: NodeId=" + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeQueriesComplete(ZWNotification n)
        {
            Logger.Debug("Notification Node Queries Complete: NodeId=" + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeEvent(ZWNotification n)
        {
            Logger.Warn("TODO Notification Node Event: NodeId=" + GetNodeIdForLog(n));
            Logger.Warn("TODO Event: NodeId=" + n.GetEvent().ToString());
        }

        private void NotificationNodeNaming(ZWNotification n)
        {
            //Update State
            Logger.Debug("Notification Node Naming: NodeId=" + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        #endregion

        #region Value

         private void NotificationValueAdded(ZWNotification n)
        {
            Logger.Debug("Notification Value Added: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueChanged(ZWNotification n)
        {
            Logger.Debug("Notification Value Changed: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueRefreshed(ZWNotification n)
        {
            Logger.Debug("Notification Value Refreshed: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueRemoved(ZWNotification n)
        {
            Logger.Debug("Notification Value Removed: " + GetNodeIdForLog(n));
            RemoveNodeValue(n);
        }

        #endregion

        #region Weird Notification

        private void Notification(ZWNotification n)
        {
            Logger.Warn("TODO Notification Error???: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Scene

        private void NotificationSceneEvent(ZWNotification n)
        {
            Logger.Warn("TODO Notification Scene Event: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Group

        private void NotificationGroup(ZWNotification n)
        {
            Logger.Warn("TODO Notification Group: " + GetNodeIdForLog(n));
        }

        #endregion

        #endregion

        #region Implementations

        private void LoadRegisteredControllers()
        {
            //Get DataDictionary For installed Controllers
            var registeredControllers = DataStore.GetOrCreateDataDictionary("registeredControllers");

            foreach (var item in registeredControllers.Keys)
            {
                CreateController(int.Parse(registeredControllers.GetString(item)));
            }
        }

        private bool CreateController(int port, bool isNew = false)
        {
            bool result = false;
            Logger.Debug("Creating Driver Controler on port: " + port);

            //Valid if a Controller already exist on this port
            if (this.FindChild(port.ToString()) != null)
            {
                Logger.Error("Creating Driver Controler on port: " + port + " Failed. A Controler already exist on port : " + port);
                return false;
            }

            Logger.Debug("Trying to start a Driver Controler on port " + port);
            bool mngResult = _mng.AddDriver(@"\\.\COM" + port, ZWControllerInterface.Serial);

            IDictionary<string, object> options = new Dictionary<string, object>();
            options.Add("interface", this);

            if (mngResult)
            {
                Logger.Info("Trying to start a Driver Controler on port " + port + " was successfull");
                Logger.Debug("Creating new Driver Controler node for port " + port);

                //Create new node
                ZWaveDriverControlerRalNode ctl = this.CreateChildNode("zwaveDriver", "COM" + port, "COM" + port, options) as ZWaveDriverControlerRalNode;

                if (ctl != null)
                {
                    this._runningDriverControlers.Add(@"\\.\COM" + port, ctl);
                    Logger.Info("Creating new Driver Controler node for port " + port + " was successfull");

                    //Store Driver Controler in the DataStore
                    if (isNew)
                    {
                        Logger.Debug("Saving new Driver Controler with port " + port);
                        var registeredControllers = DataStore.GetOrCreateDataDictionary("registeredControllers");
                        registeredControllers.StoreString(port.ToString(), port.ToString());
                        DataStore.Save();
                        Logger.Info("Saving new Driver Controler with port " + port + " was successfull");
                    }

                    result = true;
                }
            }
            else
            {
                Logger.Error("Trying to start a Driver Controler on port " + port + " failed");

                //Create new node
                options.Add("initialState", NodeStates.fatal);
                ZWaveDriverControlerRalNode ctl = this.CreateChildNode("zwavecontroller", "COM" + port, "COM" + port, options) as ZWaveDriverControlerRalNode;

                if (ctl != null)
                {
                    this._runningDriverControlers.Add(@"\\.\COM" + port, ctl);
                }
                
            }
            return result;
        }

        private void CreateOrUpdateNode(ZWNotification n)
        {
            //Find driverControler

            ZWaveDriverControlerRalNode controler;
            string controlerPath = Manager.GetControllerPath(n.GetHomeId());

            if (_runningDriverControlers.TryGetValue(controlerPath, out controler))
            {
                controler.CreateOrUpdateNode(n);
            }
            else
            {
                Logger.Error("Controler not found for creating or updating node");
            }
        }

        private void RemoveNode(ZWNotification n)
        {
            //Find driverControler
            ZWaveDriverControlerRalNode controller;
            string controlerPath = Manager.GetControllerPath(n.GetHomeId());

            if (_runningDriverControlers.TryGetValue(controlerPath, out controller))
            {
                //TODO driverControler.RemoveNode(n);
            }
            else
            {
                Logger.Error("Controler Node not found for removing node");
            }
        }

        private bool CreateOrUpdateNodeValue(ZWNotification n)
        {
            string controlerPath = _mng.GetControllerPath(n.GetHomeId());
            ZWaveDriverControlerRalNode controler;

            if (_runningDriverControlers.TryGetValue(controlerPath, out controler))
            {
                controler.CreateOrUpdateValue(n);
            }
            else
            {
                Logger.Error("Controler not found for Creating or updating node value");
            }
           
            return false;
        }

        private void RemoveNodeValue(ZWNotification n)
        {
            //Find node
            //Detect if its the driverControler
            byte ControllerNodeId = _mng.GetControllerNodeId(n.GetHomeId());
            bool isControllerNode = ControllerNodeId == n.GetNodeId();

            if (isControllerNode)
            {
                string controllerPath = _mng.GetControllerPath(n.GetHomeId());
                ZWaveDriverControlerRalNode controller;
                if (_runningDriverControlers.TryGetValue(controllerPath, out controller))
                {
                    //TODO driverControler.RemoveValue(n);
                }
                else
                {
                    Logger.Error("Controler Node not found for removing value");
                }
            }
            else
            {
                var node = this.FindChild(NotificationTool.MakeNodeKey(n));
                if (node != null)
                {
                    ((ZWaveNode)node).RemoveValue(n);
                }
                else
                {
                    Logger.Error("Node not found for removing value");
                }
            }
        }

        #endregion

        #region Tools

        private string GetNodeIdForLog(ZWNotification n)
        {
            return " HomeId: " + n.GetHomeId() + " - NodeId: " + n.GetNodeId() + "- InstanceId: " + n.GetGroupIdx();
        }

        #endregion

        #endregion

        protected override AbstractNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options)
        {
            AbstractNode result = null ;

            switch (model)
            {
                case "zwaveDriver":
                    result = new ZWaveDriverControlerRalNode(key, name);
                    break;
                case "ZwaveRalNodesContainer":
                    result = new ZWaveNodesContainer(key, name, (byte)options["controlerId"]);
                    break;
                case "zwaveControler":
                    result = new ZWaveControler(key, name);
                    break;
                case "zwaveNode":
                    result = new ZWaveNode(key, name);
                    break;
                default:
                    result = new ALRBasicNode(key, name);
                    break;
            }

            return result;
        }

        protected override void RegisterCommands()
        {
            //Create Commands
            this.RegisterCommand(new CreateControler());
        }

        protected override void RegisterProperties()
        {
            
        }
    }
}
