using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using System;
using WUnderground.Nodes;

namespace WUnderground
{
    public sealed class WUndergroundPlugin : PluginBase
    {
        #region Private Members

        private Guid _id = new Guid("6e1ab586-e584-4eb3-ab3e-b46bd2a2d2c0");
        private const string _name = "Weather Underground Plugin";
        private const string _interfaceKey = "WUndergroundInterface";

        #endregion

        #region Public Properties

        public override Guid Id { get { return _id; } }

        public override string Name { get { return _name; } }

        #endregion

        #region Public Methods

        public override bool Install(IOhmSystemInstallGateway system)
        {
            return system.RegisterInterface(_interfaceKey);
        }

        public override bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            return system.UnRegisterInterface(_interfaceKey);
        }

        public override bool Update(IOhmSystemInstallGateway system)
        {
            return true;
        }

        public override ALRInterfaceAbstractNode CreateInterface(string key)
        {
            switch (key)
            {
                case _interfaceKey:
                    return new WUndergroundInterfaceNode();
            }
            return null;
        }

        public override OHM.VAL.IVrType CreateVrNode(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
