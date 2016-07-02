using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;
using OHM.Sys;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using ZWaveLib.Commands;
using ZWaveLib.Data;
using ZWaveLib.Tools;

namespace ZWaveLib
{
    public class ZWaveInterface : InterfaceAbstract
    {
        private ZWManager _mng = new ZWManager();
        private IDataDictionary _registeredControllers;
        private Dictionary<string, ZWaveController> _runningControllers;
        Dispatcher _dispatcher;

        #region Ctor

        public ZWaveInterface(ILogger logger)
            : base("ZWaveInterface", "ZWave", logger)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            //Create Commands
            this.RegisterCommand(new CreateControler(this));
        }
            
        #endregion

        #region public

        protected override void Start()
        {
            
            _runningControllers = new Dictionary<string, ZWaveController>();

            var apiPath = @"C:\Users\Scopollif\Documents\Visual Studio 2013\Projects\OpenHomeMation\external\open-zwave\openzwave-1.0.791";
            ZWOptions opt = new ZWOptions();
            opt.Create(apiPath + @"\config\", apiPath + @"", @"");
            //Lock the options
            opt.Lock();

            // Create the OpenZWave Manager
            _mng.Create();
            _mng.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
            _mng.OnControllerStateChanged += new ManagedControllerStateChangedHandler(ControllerStateChangedHandler);

            //Log OpenZWave version
            Logger.Info("OpenZWave Version: " + _mng.GetVersionAsString());

            //Get DataDictionary For installed Controllers
            _registeredControllers = DataStore.GetOrCreateDataDictionary("registeredControllers");
            
            //Load registered controllers
            LoadRegisteredControllers();
        }

        protected override void Shutdown()
        {
            _mng.Destroy();
        }

        #endregion

        #region internal

        internal bool CreateNewController(int port)
        {
            return CreateController(port, true);
        }

        internal bool CreateController(int port, bool isNew = false)
        {
            bool result = false;
            Logger.Info("Creating Controller on port: " + port);

            //Valid if a Controller already exist on this port
            if (this.GetChild(port.ToString()) != null) {
                Logger.Error("Controller already exist on port : " + port);
                return false;
            }

            //Store Controller
            if (isNew)
            {
                Logger.Info("Saving new controller on port " + port);
                _registeredControllers.StoreString(port.ToString(), port.ToString());
                DataStore.Save();
                Logger.Info("Saved new controller on port " + port);
            }

            Logger.Info("Trying to start a controller on port " + port);
            bool mngResult = _mng.AddDriver(@"\\.\COM" + port, ZWControllerInterface.Serial);
            if (mngResult)
            {
                Logger.Info("Tryed to start a controller on port " + port + " was successfull");
                //Create new node 
                var ctl = new ZWaveController(@"\\.\COM" + port, @"\\.\COM" + port, this, this.Logger);
                this.AddChild(ctl);
                this._runningControllers.Add(@"\\.\COM" + port, ctl);

                Logger.Info("Controller created on port " + port);
                result = true;
            }
            else
            {
                //Create new node
                var ctl = new ZWaveController(@"\\.\COM" + port, @"\\.\COM" + port, this, this.Logger, NodeStates.fatal);
                this.AddChild(ctl);
                this._runningControllers.Add(@"\\.\COM" + port, ctl);
                
                Logger.Info("Cannot create Controller on port " + port);
            }
            return result;
        }

        internal void RemoveController(int port)
        {
            Logger.Info("Removing Controller on port: " + port);
            _mng.RemoveDriver(@"\\.\COM" + port);
        }

        internal ZWManager Manager
        {
            get
            {
                return _mng;
            }
        }
        
        #endregion

        #region private

        #region Manager Handler

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

                    break;
                case ZWNotification.Code.Awake:
                    break;
                case ZWNotification.Code.Dead:
                    break;
                case ZWNotification.Code.MsgComplete:
                    break;
                case ZWNotification.Code.NoOperation:
                    break;
                case ZWNotification.Code.Sleep:
                    break;
                case ZWNotification.Code.Timeout:
                    break;
            }

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
            Logger.Debug("Controller State Changed:  " + state.ToString());
        }

        #endregion

        #region NotificationHandler

        #region Driver

        private void NotificationDriverReady(ZWNotification n)
        {
            Logger.Info("Notification Controller Ready:" + GetNodeIdForLog(n));
            string key = NotificationTool.MakeNodeKey(n);
            string name = NotificationTool.GetNodeName(n, this.Manager);
            
            
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();

            string controlerPath = Manager.GetControllerPath(homeId);

            Logger.Info("Controller Path  : " + controlerPath);

            Logger.Info("Controller Library Type Name : " + _mng.GetLibraryTypeName(homeId));
            Logger.Info("Controller Library Version : " + _mng.GetLibraryVersion(homeId));

            if (this._runningControllers.ContainsKey(controlerPath))
            {
                ZWaveController controller = null;
                if (this._runningControllers.TryGetValue(controlerPath, out controller))
                {
                    controller.Init(homeId, nodeId);
                }
            }
        }

        private void NotificationDriverReset(ZWNotification n)
        {
            Logger.Debug("Notification Driver Reset:" + GetNodeIdForLog(n));
        }

        private void NotificationDriverFailed(ZWNotification n)
        {
            uint homeId = n.GetHomeId();
            string controlerPath = Manager.GetControllerPath(homeId);
            Logger.Debug("Notification Driver Failed with controlePath:" + controlerPath);

            if (this._runningControllers.ContainsKey(controlerPath))
            {
                ZWaveController controller = null;
                if (this._runningControllers.TryGetValue(controlerPath, out controller))
                {
                    controller.SetFatalState();
                }
            }
            else
            {
                Logger.Error("Controller not found in the running Controllers and it not suppose to append");
            }
            
        }

        private void NotificationDriverRemoved(ZWNotification n)
        {
            Logger.Debug("Notification Driver Failed:" + GetNodeIdForLog(n));
        }

        #endregion

        #region Initialize query

        private void NotificationAllNodesQueried(ZWNotification n)
        {
            Logger.Info("Notification All Nodes Queried: " + GetNodeIdForLog(n));
        }

        private void NotificationAwakeNodesQueried(ZWNotification n)
        {
            Logger.Info("Notification Awake Nodes Queried: " + GetNodeIdForLog(n));
        }

        private void NotificationAllNodesQueriedSomeDead(ZWNotification n)
        {
            Logger.Info("Notification All Nodes Queried Some Dead: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Node

        private void NotificationNodeNew(ZWNotification n)
        {
            Logger.Info("Notification New Node: " + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeAdded(ZWNotification n)
        {
            //Update State
            Logger.Info("Notification Node Added: " + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeRemoved(ZWNotification n)
        {
            Logger.Info("Notification Node Removed: " + GetNodeIdForLog(n));
            RemoveNode(n);
        }

        private void NotificationEssentialNodeQueriesComplete(ZWNotification n)
        {
            Logger.Info("Notification Essential Node Queries Complete: " + GetNodeIdForLog(n));
        }

        private void NotificationNodeProtocolInfo(ZWNotification n)
        {
            Logger.Info("Notification Node Protocol Info: " + GetNodeIdForLog(n));
        }

        private void NotificationNodeQueriesComplete(ZWNotification n)
        {
            Logger.Debug("Notification Node Queries Complete:" + GetNodeIdForLog(n));
        }

        private void NotificationNodeEvent(ZWNotification n)
        {
            Logger.Debug("Notification Node Event: " + GetNodeIdForLog(n));
            Logger.Debug("Event: " + n.GetEvent().ToString());
        }

        private void NotificationNodeNaming(ZWNotification n)
        {
            //Update State
            Logger.Info("TODO Notification Node Naming: " + GetNodeIdForLog(n));
            //UpdateNode(n);
        }

        #endregion

        #region Value

         private void NotificationValueAdded(ZWNotification n)
        {
            Logger.Info("Notification Value Added: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueChanged(ZWNotification n)
        {
            Logger.Info("Notification Value Changed: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueRefreshed(ZWNotification n)
        {
            Logger.Info("Notification Value Refreshed: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueRemoved(ZWNotification n)
        {
            Logger.Info("Notification Value Removed: " + GetNodeIdForLog(n));
            RemoveNodeValue(n);
        }

        #endregion

        #region Weird Notification

        private void Notification(ZWNotification n)
        {
            Logger.Info("Notification Error???: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Scene

        private void NotificationSceneEvent(ZWNotification n)
        {
            Logger.Info("Notification Scene Event: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Group

        private void NotificationGroup(ZWNotification n)
        {
            Logger.Info("Notification Group: " + GetNodeIdForLog(n));
        }

        #endregion

        #endregion

        private void LoadRegisteredControllers()
        {
            foreach (var item in _registeredControllers.Keys)
            {
                CreateController(int.Parse(_registeredControllers.GetString(item)));
            }
        }

        private void CreateOrUpdateNode(ZWNotification n)
        {
            //Find controller
            ZWaveController controller;
            string controlerPath = Manager.GetControllerPath(n.GetHomeId());

            if (_runningControllers.TryGetValue(controlerPath, out controller))
            {
                controller.CreateOrUpdateNode(n);
            }
            else
            {
                Logger.Error("Controler Node not found for creating or updating node");
            }
        }

        private void RemoveNode(ZWNotification n)
        {
            //Find controller
            ZWaveController controller;
            string controlerPath = Manager.GetControllerPath(n.GetHomeId());

            if (_runningControllers.TryGetValue(controlerPath, out controller))
            {
                controller.RemoveNode(n);
            }
            else
            {
                Logger.Error("Controler Node not found for removing node");
            }
        }

        private bool CreateOrUpdateNodeValue(ZWNotification n)
        {
            //Find node
            //Detect if its the controller
            byte ControllerNodeId = _mng.GetControllerNodeId(n.GetHomeId());
            bool isControllerNode = ControllerNodeId == n.GetNodeId();

            if (isControllerNode)
            {
                string controllerPath = _mng.GetControllerPath(n.GetHomeId());
                ZWaveController controller;
                if (_runningControllers.TryGetValue(controllerPath, out controller))
                {
                    controller.CreateOrUpdateValue(n);
                }
                else
                {
                    Logger.Error("Controler Node not found for Creating or updating value");
                }
            }
            else
            {
                var node = this.GetChild(NotificationTool.MakeNodeKey(n));
                if (node != null)
                {
                    return ((ZWaveNode)node).CreateOrUpdateValue(n);
                }
                else
                {
                    Logger.Error("Node not found for Creating or updating value");
                }
            }
           
            return false;
        }

        private void RemoveNodeValue(ZWNotification n)
        {
            //Find node
            //Detect if its the controller
            byte ControllerNodeId = _mng.GetControllerNodeId(n.GetHomeId());
            bool isControllerNode = ControllerNodeId == n.GetNodeId();

            if (isControllerNode)
            {
                string controllerPath = _mng.GetControllerPath(n.GetHomeId());
                ZWaveController controller;
                if (_runningControllers.TryGetValue(controllerPath, out controller))
                {
                    controller.RemoveValue(n);
                }
                else
                {
                    Logger.Error("Controler Node not found for removing value");
                }
            }
            else
            {
                var node = this.GetChild(NotificationTool.MakeNodeKey(n));
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
        
        #region Tools

        private string GetNodeIdForLog(ZWNotification n)
        {
            return " HomeId: " + n.GetHomeId() + " - NodeId: " + n.GetNodeId() + "- InstanceId: " + n.GetGroupIdx();
        }

        #endregion

        #endregion
    }
}
