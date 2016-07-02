using OHM.Commands;
using OHM.Interfaces;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Data;

namespace ZWaveLib.Commands
{
    public abstract class ZWaveCommandAbstract : CommandAbstract, IInterfaceCommand
    {

        private IInterface _interface;

        public string InterfaceKey
        {
            get { return Interface.Key; }
        }

        protected IInterface Interface
        {
            get
            {
                if (_interface == null)
                {
                    LookupAndStoreInterface(this.Node);
                }
                
                return _interface; 
            }
        }

        protected ZWaveInterface ZWaveInterface
        {
            get {
                return (ZWaveInterface)Interface; 
            }
        }

        private void LookupAndStoreInterface(INode node)
        {

            if (node == null)
            {
                node = this.Node;
            }

            if (node is IInterface)
            {
                _interface = (IInterface)node;
            }
            else if (node.Parent != null)
            {
                LookupAndStoreInterface(node.Parent);
            }
            else
            {
                //TODO log error
            }
        }

        private bool IsStateRunning()
        {
            if (Interface != null)
            {
                return _interface.IsRunning;
            } 
            return false;
        }

        public override bool CanExecute()
        {
            return IsStateRunning();
        }

        /*protected new ZWaveNode Node
        {
            get { return (ZWaveNode)base.Node; }
        }*/

        public ZWaveCommandAbstract(INode node, string key, string name, string description) 
            : base(node, key, name, description, null)
        {
            
        }
    }
}
