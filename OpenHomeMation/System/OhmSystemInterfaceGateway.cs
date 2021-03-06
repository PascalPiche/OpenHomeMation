﻿using OHM.Nodes;
using OHM.Nodes.ALR;
using System;

namespace OHM.SYS
{
    public sealed class OhmSystemInterfaceGateway : IOhmSystemInterfaceGateway
    {

        private IOhmSystemInternal _system;
        private IALRInterface _interface;

        internal OhmSystemInterfaceGateway(IOhmSystemInternal system, IALRInterface interf)
        {
            _system = system;
            _interface = interf;
        }

        public bool CreateNode(AbstractPowerTreeNode node)
        {
            throw new NotImplementedException();
        }

        public bool RemoveNode(AbstractPowerTreeNode node)
        {
            throw new NotImplementedException();
        }
    }
}
