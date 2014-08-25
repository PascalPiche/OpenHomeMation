using OHM.Commands;
using OHM.Interfaces;
using OHM.Logger;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;

namespace ZWaveLib
{
    public class ZWaveInterface : IInterface
    {

        private ZWManager mng = new ZWManager();
        private ILogger logger;
        private List<ICommandDefinition> commands;
        private Dictionary<int, IZWaveController> controllers = new Dictionary<int, IZWaveController>();

        public IList<ICommandDefinition> Commands()
        {
            return commands;
        }

        public ZWaveInterface(ILogger logger) 
        {
            this.logger = logger;

            //Create Commands
            commands = new List<ICommandDefinition>();
        }

        public void Init()
        {
            var apiPath = @"C:\Users\Scopollif\Documents\Visual Studio 2013\Projects\OpenHomeMation\external\open-zwave\openzwave-1.0.791";
            ZWOptions opt = new ZWOptions();
            opt.Create(apiPath + @"\config\", apiPath + @"", @"");
            //Lock the options
            opt.Lock();

            // Create the OpenZWave Manager
            mng.Create();
            mng.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
        }

        public void ExecuteCommand(String key, Dictionary<String, Object> arguments)
        {

        }

        public bool CreateController(int port)
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

        public void RemoveController(int port)
        {
            logger.Info("Removing Driver on port: " + port);
            mng.RemoveDriver(@"\\.\COM" + port);
        }

        public void Shutdown()
        {
            
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
    }
}
