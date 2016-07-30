using OHM.Nodes;
using OHM.RAL;
using OpenZWaveDotNet;
using System;
using ZWaveLib.Commands;
using ZWaveLib.Tools;

namespace ZWaveLib.Data
{
    public class ZWaveDriverControlerRalNode : RalNodeAbstract, IZWaveDriverControlerNode
    {
        private uint _homeId;

        #region Public Ctor

        public ZWaveDriverControlerRalNode(string key, string name)
            : base(key, name) {}

        #endregion

       #region Internal Methods

        internal void AssignHomeId(uint homeId)
        {
            this._homeId = homeId;
            //Update Self properties
            UpdateSelfProperties();

            //Create node for ZWaveNode
            this.CreateChildNode("BasicRalNode", "nodes", "Nodes");
            this.CreateChildNode("BasicRalNode", "scenes", "Scenes");
        }

        internal void SetFatalState()
        {
            this.State = NodeStates.fatal;
        }

        #endregion

        #region Private Methods

        private void UpdateSelfProperties()
        {
            this.UpdateProperty("DriverControllerInterfaceType", ((ZWaveInterface)Parent).Manager.GetControllerInterfaceType(this.HomeId.Value));
            //this.UpdateProperty("IsBridgeController", ((ZWaveInterface)Parent).Manager.IsBridgeController(this.HomeId.Value));
            //this.UpdateProperty("IsPrimaryController", ((ZWaveInterface)Parent).Manager.IsPrimaryController(this.HomeId.Value));
            //this.UpdateProperty("IsStaticUpdateController", ((ZWaveInterface)Parent).Manager.IsStaticUpdateController(this.HomeId.Value));
        }

        #endregion

        protected override void RegisterCommands()
        {
            this.RegisterCommand(new ControlerAllOnCommand());
            this.RegisterCommand(new ControlerAllOffCommand());
            this.RegisterCommand(new ControlerSoftResetCommand());
            this.RegisterCommand(new ControlerHardResetCommand());
            this.RegisterCommand(new ControlerAddNodeCommand());
        }

        protected override void RegisterProperties()
        {
            this.RegisterProperty(new NodeProperty("DriverControllerInterfaceType", "Driver Controller Interface Type", typeof(ZWControllerInterface), true));
            //this.RegisterProperty(new NodeProperty("IsBridgeController", "Is Bridge Controller", typeof(Boolean), true));
            //this.RegisterProperty(new NodeProperty("IsPrimaryController", "Is Primary Controller", typeof(Boolean), true));
            //this.RegisterProperty(new NodeProperty("IsStaticUpdateController", "Is Static Update Controller", typeof(Boolean), true));
        }

        public uint? HomeId
        {
            get { return this._homeId; }
        }
    }
}
