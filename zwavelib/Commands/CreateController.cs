using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib.Commands
{
    public class CreateController : ZWaveCommandAbstract, IInterfaceCommand
    {

        public CreateController(ZWaveInterface interf)
            : base(interf, "createController", "Create a controller", "")
        {

            this.Definition.ArgumentsDefinition.Add
            (
                "port",
                new ArgumentDefinition(
                    "port",
                    "Port",
                    typeof(int),
                    true
                )
            );
        }


        protected override bool RunImplementation(Dictionary<string, object> arguments)
        {
            int port;

            if (!this.Definition.ArgumentsDefinition["port"].TryGetInt32(arguments, out port))
            {
                return false;
            }

            if (port < 0)
            {
                return false;
            }

            return Interface.CreateController(port, true);

        }
    }
}
