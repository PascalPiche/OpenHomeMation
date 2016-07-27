﻿using OHM.RAL;
using System;

namespace OHM.SYS
{
    public sealed class OhmSystemInterfaceGateway : IOhmSystemInterfaceGateway
    {

        private IOhmSystemInternal _system;
        private IInterface _interface;

        internal OhmSystemInterfaceGateway(IOhmSystemInternal system, IInterface interf)
        {
            _system = system;
            _interface = interf;
        }

        public bool CreateNode(Nodes.ITreeNode node)
        {
            throw new NotImplementedException();
        }

        public bool RemoveNode(Nodes.ITreeNode node)
        {
            throw new NotImplementedException();
        }
    }
}
