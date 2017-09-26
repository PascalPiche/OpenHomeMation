using OHM.Nodes;
using OHM.Nodes.Properties;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using ZWaveLib.Commands;

namespace ZWaveLib.Nodes
{
    public class ZWaveDriverControlerRalNode : ZWaveHomeNode, IZWaveDriverControlerNode
    {
        #region Private Members

        private ZWaveNodesContainer nodesCtn;

        #endregion

        #region Public Ctor

        public ZWaveDriverControlerRalNode(string key, string name)
            : base(key, name) {}

        #endregion

        #region Protected Methods

        protected override void RegisterCommands()
        {
            this.RegisterCommand(new ControlerSoftResetCommand());
            this.RegisterCommand(new ControlerHardResetCommand());
            this.RegisterCommand(new ControlerAddNodeCommand());
        }

        protected override bool RegisterProperties()
        {
            this.RegisterProperty(new NodeProperty("DriverControllerInterfaceType", "Driver Controller Interface Type", typeof(ZWControllerInterface), true, "", null));
            this.RegisterProperty(new NodeProperty("IsBridgeController", "Is Bridge Controller", typeof(Boolean), true, "", null));
            this.RegisterProperty(new NodeProperty("IsPrimaryController", "Is Primary Controller", typeof(Boolean), true, "", null));
            this.RegisterProperty(new NodeProperty("IsStaticUpdateController", "Is Static Update Controller", typeof(Boolean), true, "", null));

            return true;
        }

        #endregion

        #region Internal Methods

        internal void AssignHomeId(uint homeId, byte controlerId)
        {
            base.AssignZWaveId(homeId);

            //Update Self properties
            UpdateSelfProperties();

            //Create node for ZWaveNode
            IDictionary<string, object> options = new Dictionary<string,object>();
            options.Add("controlerId", controlerId);
            nodesCtn = this.CreateChildNode("ZwaveRalNodesContainer", "nodes", "Nodes", options) as ZWaveNodesContainer;
            nodesCtn.AssignZWaveId(homeId);
            this.CreateChildNode("BasicRalNode", "scenes", "Scenes (Planned)");
        }

        internal void SetFatalState()
        {
            this.SystemState = SystemNodeStates.fatal;
        }

        internal void CreateOrUpdateNode(ZWNotification n)
        {
            nodesCtn.CreateOrUpdateNode(n);
        }

        internal void CreateOrUpdateValue(ZWNotification n)
        {
            nodesCtn.CreateOrUpdateValue(n);
        }

        #endregion

        #region Private Methods

        private void UpdateSelfProperties()
        {
            this.UpdateProperty("DriverControllerInterfaceType",    ((ZWaveInterface)Parent).Manager.GetControllerInterfaceType(this.HomeId.Value));
            this.UpdateProperty("IsBridgeController",               ((ZWaveInterface)Parent).Manager.IsBridgeController(this.HomeId.Value));
            this.UpdateProperty("IsPrimaryController",              ((ZWaveInterface)Parent).Manager.IsPrimaryController(this.HomeId.Value));
            this.UpdateProperty("IsStaticUpdateController",         ((ZWaveInterface)Parent).Manager.IsStaticUpdateController(this.HomeId.Value));
        }

        #endregion
    }
}
