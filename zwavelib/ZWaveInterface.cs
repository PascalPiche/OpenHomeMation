using OHM.Commands;
using OHM.Interfaces;
using OHM.Logger;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;

namespace ZWaveLib
{
    public class ZWaveInterface : InterfaceAbstract
    {

        private ZWManager mng = new ZWManager();
        private ILogger logger;
        
        private Dictionary<int, IZWaveController> controllers = new Dictionary<int, IZWaveController>();

        public ZWaveInterface(ILogger logger)
            : base("ZWaveInterface", "ZWave")
        {
            this.logger = logger;

            //Create Commands
            this.Commands.Add(new CreateControllerCommand(this));
        }

        public override void Start()
        {
            logger.Info("ZWave Interface initing");
            var apiPath = @"C:\Users\Scopollif\Documents\Visual Studio 2013\Projects\OpenHomeMation\external\open-zwave\openzwave-1.0.791";
            ZWOptions opt = new ZWOptions();
            opt.Create(apiPath + @"\config\", apiPath + @"", @"");
            //Lock the options
            opt.Lock();

            // Create the OpenZWave Manager
            mng.Create();
            mng.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
            mng.OnControllerStateChanged += new ManagedControllerStateChangedHandler(ControllerStateChangedHandler);
            base.Start();
            logger.Info("ZWave Interface Inited");
        }

        public override void Shutdown()
        {
            logger.Info("ZWave Interface Shutdowning");
            mng.Destroy();
            base.Shutdown();
            logger.Info("ZWave Interface Shutdowned");
        }

        internal bool CreateController(int port)
        {
            logger.Info("Creating ZWave Controller on port: " + port);

            //Valid if a Controller exist on this port
            if (controllers.ContainsKey(port)) {
                logger.Error("ZWave Controller already exist on port : " + port);
                return false;
            }

            mng.AddDriver(@"\\.\COM" + port, ZWControllerInterface.Serial);
            logger.Info("ZWave Controller created on port: " + port);
            return true;
        }

        internal void RemoveController(int port)
        {
            logger.Info("Removing Driver on port: " + port);
            mng.RemoveDriver(@"\\.\COM" + port);
        }

        private void NotificationHandler(ZWNotification n)
        {
            logger.Debug("HomeId:  " + n.GetHomeId());
            logger.Debug("NodeId:  " + n.GetNodeId());
            logger.Debug("GroupId: " + n.GetGroupIdx());
            logger.Debug("Type:    " + n.GetType().ToString());
            logger.Debug("Event:   " + n.GetEvent());
            logger.Debug("Value ClassId: " + n.GetValueID().GetCommandClassId());
            logger.Debug("Value Genre: " + n.GetValueID().GetGenre().ToString());
            var vid = n.GetValueID();
            logger.Debug("Value Id: " + vid.GetId());
            logger.Debug("Value Label: " + mng.GetValueLabel(vid));
            logger.Debug("Value Index: " + vid.GetIndex());
            logger.Debug("Value type: " + n.GetValueID().GetType());
            logger.Debug("Byte:    " + n.GetByte());
            byte classVersion;
            String className;
            mng.GetNodeClassInformation(n.GetHomeId(), n.GetNodeId(), n.GetValueID().GetCommandClassId(), out className, out classVersion);
            logger.Debug("ClassName (version): " + className + "(" + classVersion.ToString() + ")");
            logger.Debug("----------------------------------------------");
        }

        private void ControllerStateChangedHandler(ZWControllerState state)
        {
            
        }
    }


    public class CreateControllerCommand : CommandAbstract
    {

        private ZWaveInterface _interface;
        public CreateControllerCommand(ZWaveInterface interf)
            : base("createController", "Create a controller")
        {
            _interface = interf;
            this.ArgumentsDefinition.Add
            (
                "port", 
                new ArgumentDefinition(
                    "port", 
                    "Port", 
                    typeof(int), 
                    true
                )
            );
        }


       protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            return false;/* _interface.CreateController(
                this.ArgumentsDefinition["port"]).GetValue(arguments["port"]);
            );*/
        }
    }

}
