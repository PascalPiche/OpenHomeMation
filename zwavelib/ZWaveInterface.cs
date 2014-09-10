using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;
using OHM.Sys;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace ZWaveLib
{
    public class ZWaveInterface : InterfaceAbstract
    {

        private ZWManager _mng = new ZWManager();
        private IDataDictionary _registeredControllers;
        private Dictionary<uint, ZWaveController> _runningControllers;

        #region "Ctor"

        public ZWaveInterface()
            : base("ZWaveInterface", "ZWave")
        {
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

        internal bool CreateController(int port)
        {
            Logger.Info("Creating ZWave Controller on port: " + port);

            //Valid if a Controller exist on this port
            if (this.GetChild(port.ToString()) != null) {
                Logger.Error("ZWave Controller already exist on port : " + port);
                return false;
            }

            _mng.AddDriver(@"\\.\COM" + port, ZWControllerInterface.Serial);
            Logger.Info("ZWave Controller created on port: " + port);

            //Store Controller
            _registeredControllers.StoreString(port.ToString(), port.ToString());
            DataStore.Save();
            
            return true;
        }

        internal void RemoveController(int port)
        {
            Logger.Info("Removing Driver on port: " + port);
            _mng.RemoveDriver(@"\\.\COM" + port);
        }

        internal void AllOn(uint homeId)
        {
            _mng.SwitchAllOn(homeId);
        }

        internal void AllOff(uint homeId)
        {
            _mng.SwitchAllOff(homeId);
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

        private void NotificationHandler(ZWNotification n)
        {
            /*Logger.Debug("HomeId:  " + n.GetHomeId());
            Logger.Debug("NodeId:  " + n.GetNodeId());
            Logger.Debug("GroupId: " + n.GetGroupIdx());
            Logger.Debug("Type:    " + n.GetType().ToString());
            Logger.Debug("Event:   " + n.GetEvent());
            Logger.Debug("Value ClassId: " + n.GetValueID().GetCommandClassId());
            Logger.Debug("Value Genre: " + n.GetValueID().GetGenre().ToString());
            var vid = n.GetValueID();
            Logger.Debug("Value Id: " + vid.GetId());
            Logger.Debug("Value Label: " + _mng.GetValueLabel(vid));
            Logger.Debug("Value Index: " + vid.GetIndex());
            Logger.Debug("Value type: " + n.GetValueID().GetType());
            Logger.Debug("Byte:    " + n.GetByte());
            byte classVersion;
            String className;
            _mng.GetNodeClassInformation(n.GetHomeId(), n.GetNodeId(), n.GetValueID().GetCommandClassId(), out className, out classVersion);
            Logger.Debug("ClassName (version): " + className + "(" + classVersion.ToString() + ")");
            Logger.Debug("----------------------------------------------");*/

            switch (n.GetType())
            {
                //Query
                case ZWNotification.Type.AllNodesQueried:
                case ZWNotification.Type.AllNodesQueriedSomeDead:
                case ZWNotification.Type.AwakeNodesQueried:
                case ZWNotification.Type.EssentialNodeQueriesComplete:
                    break;
                //Driver
                case ZWNotification.Type.DriverFailed:

                    break;
                case ZWNotification.Type.DriverReady:
                    NotificationDriverReady(n);
                    break;
                case ZWNotification.Type.DriverReset:

                    break;
                //Polling
                case ZWNotification.Type.PollingDisabled:
                case ZWNotification.Type.PollingEnabled:

                //button
                case ZWNotification.Type.ButtonOff:
                case ZWNotification.Type.ButtonOn:
                case ZWNotification.Type.CreateButton:
                case ZWNotification.Type.DeleteButton:

                //Group
                case ZWNotification.Type.Group:

                //Node
                case ZWNotification.Type.NodeNew:
                    NotificationNodeNew(n);
                    break;
                case ZWNotification.Type.NodeAdded:
                    NotificationNodeAdded(n);
                    break;
                case ZWNotification.Type.NodeEvent:
                    break;
                case ZWNotification.Type.NodeNaming:
                    NotificationNodeNaming(n);
                    break;
                case ZWNotification.Type.NodeProtocolInfo:
                    break;
                case ZWNotification.Type.NodeQueriesComplete:
                    break;
                case ZWNotification.Type.NodeRemoved:
                    break;

                //Notification
                case ZWNotification.Type.Notification:
                
                //Scene
                case ZWNotification.Type.SceneEvent:

                //Value
                case ZWNotification.Type.ValueAdded:
                case ZWNotification.Type.ValueChanged:
                case ZWNotification.Type.ValueRefreshed:
                case ZWNotification.Type.ValueRemoved:
                    break;
            }
        }

        private void ControllerStateChangedHandler(ZWControllerState state)
        {
            Logger.Debug("ControllerStateChanged:  " + state.ToString());
        }

        private void NotificationDriverReady(ZWNotification n)
        {
            Logger.Info("ZWave Driver Reader");
            string key = MakeNodeKey(n);
            string name = GetNodeName(n);
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();
            var ctl = new ZWaveController(key, name, this, homeId, nodeId);
            this.AddChild(ctl);
            this._runningControllers.Add(homeId, ctl);
        }

        private void NotificationNodeNew(ZWNotification n)
        {
            Logger.Info("ZWave New Node : " + n.GetHomeId() + " - " + n.GetNodeId());
            //Find node
            var node = this.GetChild(MakeNodeKey(n));
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

        private void NotificationNodeAdded(ZWNotification n)
        {
            //Update State
            Logger.Info("ZWave Node Added: " + n.GetHomeId() + " - " + n.GetNodeId());
            //Find node
            var node = this.GetChild(MakeNodeKey(n));
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

        private void NotificationNodeNaming(ZWNotification n)
        {
            //Update State
            Logger.Info("ZWave Node Naming: " + n.GetHomeId() + " - " + n.GetNodeId());
            //Find node
            var node = this.GetChild(MakeNodeKey(n));
            if (node != null)
            {
                //Update Node
                UpdateNode(node, n);
            }
        }

        private string GetNodeName(ZWNotification n)
        {
            string result = _mng.GetNodeName(n.GetHomeId(), n.GetNodeId());
            if (String.IsNullOrEmpty(result))
            {
                result = _mng.GetNodeProductName(n.GetHomeId(), n.GetNodeId());
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "Unknow device";
            }
            return result;
        }
        
        private string MakeNodeKey(ZWNotification n)
        {
            return n.GetHomeId().ToString() + "-" + n.GetNodeId().ToString();
        }

        private void CreateNode(ZWNotification n)
        {
            string key = MakeNodeKey(n);
            string name = GetNodeName(n);
            uint homeId = n.GetHomeId();
            byte nodeId = n.GetNodeId();
            var newNode = new ZWaveNode(key, name, this, homeId, nodeId);
            this.AddChild(newNode);
        }

        private void UpdateNode(INode node, ZWNotification n)
        {
            ZWaveNode no = node as ZWaveNode;
            string name = GetNodeName(n);
            no.UpdateName(name);

        }

        #endregion
    }
}
