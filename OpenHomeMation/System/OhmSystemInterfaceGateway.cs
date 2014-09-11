using OHM.Interfaces;
using System;

namespace OHM.Sys
{
    public class OhmSystemInterfaceGateway : IOhmSystemInterfaceGateway
    {

        private IOhmSystemInternal _system;
        private IInterface _interface;

        public OhmSystemInterfaceGateway(IOhmSystemInternal system, IInterface interf)
        {
            _system = system;
            _interface = interf;
        }

        public bool CreateNode(Nodes.INode node)
        {
            throw new NotImplementedException();
        }

        public bool RemoveNode(Nodes.INode node)
        {
            throw new NotImplementedException();
        }
    }
}
