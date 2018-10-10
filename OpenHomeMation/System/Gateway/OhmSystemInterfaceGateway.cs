using OHM.Nodes;
using OHM.Nodes.ALR;
using System;

namespace OHM.SYS
{
    public sealed class OhmSystemInterfaceGateway : IOhmSystemInterfaceGateway
    {
        #region Private Members

        private IOhmSystemInternal _system;
        private IALRInterface _interface;

        #endregion 

        #region Internal Ctor

        internal OhmSystemInterfaceGateway(IOhmSystemInternal system, IALRInterface interf)
        {
            _system = system;
            _interface = interf;
        }

        #endregion

        #region Public Method

        public bool CreateNode(AbstractPowerTreeNode node)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool RemoveNode(AbstractPowerTreeNode node)
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion
    }
}