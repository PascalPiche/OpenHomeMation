using OHM.Commands;
using OHM.RAL;
using OHM.Nodes;
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
