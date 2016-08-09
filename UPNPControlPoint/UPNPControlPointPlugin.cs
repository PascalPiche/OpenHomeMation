using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using System;

namespace UPNPControlPoint
{
    public class UPNPControlPointPlugin : PluginBase
    {
        #region Private Members

        private Guid _id = new Guid("ec3cb3af-23b9-4f45-8e80-66bb869cc0a3");
        private string _name = "UPNP Control Point";
        private ALRInterfaceAbstractNode _runningInterface;

        #endregion

        #region Public Properties

        public override Guid Id     { get { return _id; } } 
        public override string Name { get { return _name; } }

        #endregion

        #region Public Methods

        public override bool Install(IOhmSystemInstallGateway system)
        {
            return system.RegisterInterface("UPNPControlPointInterface");
        }

        public override bool Update(IOhmSystemInstallGateway system)
        {
            return true;
        }

        public override ALRInterfaceAbstractNode CreateInterface(string key)
        {
            switch (key)
            {
                case "UPNPControlPointInterface":
                    _runningInterface = new UPNPControlPointInterface();
                    return _runningInterface;
            }
            return null;
        }

        public override bool Uninstall(IOhmSystemUnInstallGateway system)
        {
            return system.UnRegisterInterface("UPNPControlPointInterface");
        }

        public override OHM.VAL.IVrType CreateVrNode(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
