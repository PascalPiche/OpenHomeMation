using System.Collections.Generic;
using ZWaveLib.Nodes;

namespace ZWaveLib.Commands
{
    public class ControlerAllOnCommand : ZWaveCommandAbstract
    {
        public ControlerAllOnCommand()
            : base("allOn", "Switch all on", "")
        { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            ZWaveInterface.Manager.SwitchAllOn(((IZWaveHomeNode)Node).HomeId.Value);
            return true;
        }
    }
}
