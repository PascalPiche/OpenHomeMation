using System.Collections.Generic;
using ZWaveLib.Nodes;

namespace ZWaveLib.Commands
{
    public class ControlerSoftResetCommand : ZWaveCommandAbstract
    {
        public ControlerSoftResetCommand()
            : base("SoftReset", "Soft Reset the Z Wave Controller", "")
        {
            
        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            ZWaveInterface.Manager.SoftReset(((IZWaveHomeNode)Node).HomeId.Value);
            return true;
        }
    }

}
