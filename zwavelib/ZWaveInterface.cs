using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;
using OHM.Sys;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using ZWaveLib.Tools;

namespace ZWaveLib
{
    public class ZWaveInterface : InterfaceAbstract
    {

        private ZWManager _mng = new ZWManager();
        private IDataDictionary _registeredControllers;
        private Dictionary<uint, ZWaveController> _runningControllers;
        Dispatcher _dispatcher;

        #region "Ctor"

        public ZWaveInterface(ILogger logger)
            : base("ZWaveInterface", "ZWave", logger)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            //Create Commands
            this.RegisterCommand(new CreateControllerCommand(this));
        }
            
        #endregion

        #region "public"

        public override void Start()
        {
            Logger.Info("ZWave Interface initing");
            _runningControllers = new Dictionary<uint, ZWaveController>();

            var apiPath = @"C:\Users\Scopollif\Documents\Visual Studio 2013\Projects\OpenHomeMation\external\open-zwave\openzwave-1.0.791";
            ZWOptions opt = new ZWOptions();
            opt.Create(apiPath + @"\config\", apiPath + @"", @"");
            //Lock the options
            opt.Lock();

            // Create the OpenZWave Manager
            _mng.Create();
            _mng.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
            _mng.OnControllerStateChanged += new ManagedControllerStateChangedHandler(ControllerStateChangedHandler);

            //Get DataDictionary For installed Controllers
            _registeredControllers = DataStore.GetOrCreateDataDictionary("registeredControllers");
            
            //Load registered controllers
            LoadRegisteredControllers();

            base.Start();
            Logger.Info("ZWave Interface Inited");
        }

        public override void Shutdown()
        {
            Logger.Info("ZWave Interface Shutdowning");
            _mng.Destroy();
            base.Shutdown();
            Logger.Info("ZWave Interface Shutdowned");
        }

        #endregion

        #region "internal"

        internal bool CreateController(int port, bool saveController = false)
        {
            bool result = false;
            Logger.Info("Creating ZWave Controller on port: " + port);

            //Valid if a Controller exist on this port
            if (this.GetChild(port.ToString()) != null) {
                Logger.Error("ZWave Controller already exist on port : " + port);
                return false;
            }

            bool mngResult = _mng.AddDriver(@"\\.\COM" + port, ZWControllerInterface.Serial);
            if (mngResult)
            {
                Logger.Info("ZWave Controller created on port: " + port);

                //Store Controller
                if (saveController)
                {
                    _registeredControllers.StoreString(port.ToString(), port.ToString());
                    DataStore.Save();
                }
                result = true;
            }
            else
            {
                Logger.Info("ZWave Cannot create Controller on port: " + port);
            }
            return result;
        }

        internal void RemoveController(int port)
        {
            Logger.Info("Removing Driver on port: " + port);
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

        #region "private"

        private void LoadRegisteredControllers()
        {
            foreach (var item in _registeredControllers.Keys)
            {
                CreateController(int.Parse(_registeredControllers.GetString(item)));
            }
        }

        #region "Manager Handler"

        private void NotificationHandlerThreadSafe(ZWNotification n)
        {

            switch (n.GetType())
            {
                //Driver
                case ZWNotification.Type.DriverFailed:
                    NotificationDriverFailed(n);
                    break;
                case ZWNotification.Type.DriverReady:
                    NotificationDriverReady(n);
                    break;
                case ZWNotification.Type.DriverReset:
                    NotificationDriverReset(n);
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
            Logger.Debug("ControllerStateChanged:  " + state.ToString());
        }

        #endregion

        #region "NotificationHandler"

        #region Driver

        private void NotificationDriverReady(ZWNotification n)
        {
            Logger.Info("ZWave Driver Ready:" + GetNodeIdForLog(n));
            string key = NotificationTool.MakeNodeKey(n);
            string name = NotificationTool.GetNodeName(n, this.Manager);
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();

            Logger.Info("ZWave Library Type Name : " + _mng.GetLibraryTypeName(homeId));
            Logger.Info("ZWave Library Version : " + _mng.GetLibraryVersion(homeId));

            if (!this._runningControllers.ContainsKey(homeId)) {
                var ctl = new ZWaveController(key, name, this, homeId, nodeId, this.Logger);
                this.AddChild(ctl);
                this._runningControllers.Add(homeId, ctl);
            }
        }

        private void NotificationDriverReset(ZWNotification n)
        {
            Logger.Debug("NotificationDriverReset:" + GetNodeIdForLog(n));
        }

        private void NotificationDriverFailed(ZWNotification n)
        {
            Logger.Debug("NotificationDriverFailed:" + GetNodeIdForLog(n));
        }

        #endregion

        #region Initialize query

        private void NotificationAllNodesQueried(ZWNotification n)
        {
            Logger.Info("ZWave AllNodesQueried: " + GetNodeIdForLog(n));
        }

        private void NotificationAwakeNodesQueried(ZWNotification n)
        {
            Logger.Info("ZWave AwakeNodesQueried: " + GetNodeIdForLog(n));
        }

        private void NotificationAllNodesQueriedSomeDead(ZWNotification n)
        {
            Logger.Info("ZWave AllNodesQueriedSomeDead: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Node

        private void NotificationNodeNew(ZWNotification n)
        {
            Logger.Info("ZWave New Node: " + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeAdded(ZWNotification n)
        {
            //Update State
            Logger.Info("ZWave Node Added: " + GetNodeIdForLog(n));
            CreateOrUpdateNode(n);
        }

        private void NotificationNodeRemoved(ZWNotification n)
        {
            //Update State
            RemoveNode(n);
            Logger.Info("ZWave Node Removed: " + GetNodeIdForLog(n));
        }


        private void NotificationEssentialNodeQueriesComplete(ZWNotification n)
        {
            Logger.Info("ZWave EssentialNodeQueriesComplete: " + GetNodeIdForLog(n));
        }

        private void NotificationNodeProtocolInfo(ZWNotification n)
        {
            Logger.Info("ZWave NodeProtocolInfo: " + GetNodeIdForLog(n));
        }

        private void NotificationNodeQueriesComplete(ZWNotification n)
        {
            Logger.Debug("ZWave NotificationNodeQueriesComplete:" + GetNodeIdForLog(n));
        }

        private void NotificationNodeEvent(ZWNotification n)
        {
            Logger.Debug("ZWave NotificationNodeEvent: " + GetNodeIdForLog(n));
            Logger.Debug("ZWave Event: " + n.GetEvent().ToString());
        }

        private void NotificationNodeNaming(ZWNotification n)
        {
            //Update State
            Logger.Info("ZWave Node Naming: " + GetNodeIdForLog(n));
            UpdateNode(n);
        }

        #endregion

        #region Value

         private void NotificationValueAdded(ZWNotification n)
        {
            Logger.Info("ZWave NotificationValueAdded: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueChanged(ZWNotification n)
        {
            Logger.Info("ZWave NotificationValueChanged: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueRefreshed(ZWNotification n)
        {
            Logger.Info("ZWave NotificationValueRefreshed: " + GetNodeIdForLog(n));
            CreateOrUpdateNodeValue(n);
        }

         private void NotificationValueRemoved(ZWNotification n)
        {
            Logger.Info("ZWave NotificationValueRemoved: " + GetNodeIdForLog(n));
            //RemoveNodeValue(n);
        }

        #endregion

        #region Weird Notification

        private void Notification(ZWNotification n)
        {
            Logger.Info("ZWave Notification: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Scene

        private void NotificationSceneEvent(ZWNotification n)
        {
            Logger.Info("ZWave NotificationSceneEvent: " + GetNodeIdForLog(n));
        }

        #endregion

        #region Group

        private void NotificationGroup(ZWNotification n)
        {
            Logger.Info("ZWave NotificationGroup: " + GetNodeIdForLog(n));
        }

        #endregion

        #endregion

        private void CreateOrUpdateNode(ZWNotification n)
        {
            //Find controller
            var homeId = n.GetHomeId();
            ZWaveController controller;

            if (_runningControllers.TryGetValue(homeId, out controller))
            {
                controller.CreateOrUpdateNode(n);
            }
        }

        private void UpdateNode(ZWNotification n)
        {
            //Find controller
            var homeId = n.GetHomeId();
            ZWaveController controller;

            if (_runningControllers.TryGetValue(homeId, out controller))
            {
                controller.UpdateNode(n);
            }
        }

        private void RemoveNode(ZWNotification n)
        {
            //Find controller
            var homeId = n.GetHomeId();
            ZWaveController controller;

            if (_runningControllers.TryGetValue(homeId, out controller))
            {
                controller.RemoveNode(n);
            }
        }

        private bool CreateOrUpdateNodeValue(ZWNotification n)
        {
            //Find node
            var node = this.GetChild(NotificationTool.MakeNodeKey(n));
            if (node != null)
            {
                return ((ZWaveNode)node).CreateOrUpdateValue(n);
            }
            else
            {
                Logger.Error("ZWave Node not found vor Creating or updating value");
            }
            return false;
        }

        private void RemoveNodeValue(ZWNotification n)
        {
            //Find node
            var node = this.GetChild(NotificationTool.MakeNodeKey(n));
            if (node == null)
            {
                //TODO : Problem
            }
            else
            {
                ((ZWaveNode)node).RemoveValue(n);
            }
        }
        
        #region Tools

        private string GetNodeIdForLog(ZWNotification n)
        {
            return n.GetHomeId() + " - " + n.GetNodeId() + "-" + n.GetGroupIdx();
        }


        #endregion

        #endregion
    }
}
