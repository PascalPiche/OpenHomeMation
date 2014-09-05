using OHM.Interfaces;
using OHM.Logger;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;

namespace ZWaveLib
{
    public class ZWaveInterface : InterfaceAbstract
    {

        private ZWManager _mng = new ZWManager();
        private ILogger _logger;
        
        private Dictionary<int, IZWaveController> _controllers = new Dictionary<int, IZWaveController>();

        #region "Ctor"

        public ZWaveInterface(ILogger logger)
            : base("ZWaveInterface", "ZWave")
        {
            this._logger = logger;

            //Create Commands
            this.RegisterCommand(new CreateControllerCommand(this));
        }

        #endregion

        #region "public"

        public override void Start()
        {
            _logger.Info("ZWave Interface initing");
            var apiPath = @"C:\Users\Scopollif\Documents\Visual Studio 2013\Projects\OpenHomeMation\external\open-zwave\openzwave-1.0.791";
            ZWOptions opt = new ZWOptions();
            opt.Create(apiPath + @"\config\", apiPath + @"", @"");
            //Lock the options
            opt.Lock();

            // Create the OpenZWave Manager
            _mng.Create();
            _mng.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
            _mng.OnControllerStateChanged += new ManagedControllerStateChangedHandler(ControllerStateChangedHandler);
            base.Start();
            _logger.Info("ZWave Interface Inited");
        }

        public override void Shutdown()
        {
            _logger.Info("ZWave Interface Shutdowning");
            _mng.Destroy();
            base.Shutdown();
            _logger.Info("ZWave Interface Shutdowned");
        }

        #endregion

        #region "internal"

        internal bool CreateController(int port)
        {
            _logger.Info("Creating ZWave Controller on port: " + port);

            //Valid if a Controller exist on this port
            if (_controllers.ContainsKey(port)) {
                _logger.Error("ZWave Controller already exist on port : " + port);
                return false;
            }

            _mng.AddDriver(@"\\.\COM" + port, ZWControllerInterface.Serial);
            _logger.Info("ZWave Controller created on port: " + port);
            return true;
        }

        internal void RemoveController(int port)
        {
            _logger.Info("Removing Driver on port: " + port);
            _mng.RemoveDriver(@"\\.\COM" + port);
        }

        #endregion

        #region "private"

        private void NotificationHandler(ZWNotification n)
        {
            _logger.Debug("HomeId:  " + n.GetHomeId());
            _logger.Debug("NodeId:  " + n.GetNodeId());
            _logger.Debug("GroupId: " + n.GetGroupIdx());
            _logger.Debug("Type:    " + n.GetType().ToString());
            _logger.Debug("Event:   " + n.GetEvent());
            _logger.Debug("Value ClassId: " + n.GetValueID().GetCommandClassId());
            _logger.Debug("Value Genre: " + n.GetValueID().GetGenre().ToString());
            var vid = n.GetValueID();
            _logger.Debug("Value Id: " + vid.GetId());
            _logger.Debug("Value Label: " + _mng.GetValueLabel(vid));
            _logger.Debug("Value Index: " + vid.GetIndex());
            _logger.Debug("Value type: " + n.GetValueID().GetType());
            _logger.Debug("Byte:    " + n.GetByte());
            byte classVersion;
            String className;
            _mng.GetNodeClassInformation(n.GetHomeId(), n.GetNodeId(), n.GetValueID().GetCommandClassId(), out className, out classVersion);
            _logger.Debug("ClassName (version): " + className + "(" + classVersion.ToString() + ")");
            _logger.Debug("----------------------------------------------");
        }

        private void ControllerStateChangedHandler(ZWControllerState state)
        {

        }

        #endregion
    }
}
