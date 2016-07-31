using System.Collections.Generic;
using ZWaveLib.Data;

namespace ZWaveLib.Commands
{

    public class ControlerAllOffCommand : ZWaveCommandAbstract
    {

        public ControlerAllOffCommand()
            : base("allOff", "Switch all off")
        {
            
        }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            ZWaveInterface.Manager.SwitchAllOff(((IZWaveHomeNode)Node).HomeId.Value);
            return true;
        }
    }
}
