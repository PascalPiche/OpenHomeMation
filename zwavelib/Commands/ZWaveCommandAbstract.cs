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
    public abstract class ZWaveCommandAbstract : InterfaceCommandAbstract
    {

        #region Public Ctor

        public ZWaveCommandAbstract(INode node, string key, string name)
            : this(node, key, name, string.Empty) { }

        public ZWaveCommandAbstract(INode node, string key, string name, string description)
            : base(node, key, name, description)
        {}

        #endregion

        #region Protected Properties

        protected ZWaveInterface ZWaveInterface
        {
            get {
                return (ZWaveInterface)Interface; 
            }
        }

        #endregion

    }
}
