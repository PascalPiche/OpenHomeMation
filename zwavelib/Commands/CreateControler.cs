using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace ZWaveLib.Commands
{
    public class CreateControler : ZWaveCommandAbstract
    {

        public CreateControler()
            : base("createController", "Create a controller", "")
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

        protected override bool RunImplementation(IDictionary<string, string> arguments)
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

            return ZWaveInterface.CreateNewController(port);
        }
    }
}
