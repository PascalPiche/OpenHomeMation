using OHM.Nodes.Commands.ALR;

namespace ZWaveLib.Commands
{
    public abstract class ZWaveCommandAbstract : InterfaceCommandAbstract
    {

        #region Public Ctor

        public ZWaveCommandAbstract(string key, string name)
            : this(key, name, string.Empty) { }

        public ZWaveCommandAbstract(string key, string name, string description)
            : base(key, name, description)
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
