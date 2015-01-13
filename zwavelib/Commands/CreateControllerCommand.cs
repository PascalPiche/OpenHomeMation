using OHM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib
{
    public class CreateControllerCommand : CommandAbstract
    {

        private ZWaveInterface _interface;


        public CreateControllerCommand(ZWaveInterface interf)
            : base(interf, "createController", "Create a controller")
        {
            _interface = interf;
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

            if (!this.Definition.ArgumentsDefinition["port"].TryGetInt(arguments["port"], out port))
            {
                return false;
            }

            if (port < 0)
            {
                return false;
            }

            return _interface.CreateController(port, true);

        }
    }
}
