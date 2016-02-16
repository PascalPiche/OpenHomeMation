using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveLib.Data;

namespace ZWaveLib.Commands
{
    public class BeginControllerCommand : ZWaveCommandAbstract
    {
        private ZWaveController _controller;

        protected BeginControllerCommand(ZWaveController controller)
            : base(controller, "beginControllerCommand", "Begin Controller Command", "")
        {
            _controller = controller;

            /*this.Definition.ArgumentsDefinition.Add
            (
                "port",
                new ArgumentDefinition(
                    "port",
                    "Port",
                    typeof(int),
                    true
                )
            );*/
        }

        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            //_controller.i.Manager.BeginControllerCommand(_controller.HomeId, )
            return false;
        }
    }
}
