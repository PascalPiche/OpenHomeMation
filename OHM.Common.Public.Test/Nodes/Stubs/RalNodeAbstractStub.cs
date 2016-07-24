using OHM.Nodes.Commands;
using OHM.RAL;

namespace OHM.Common.Public.Test.Nodes.Stubs
{
    public class RalNodeAbstractStub : RalNodeAbstract
    {

        public RalNodeAbstractStub(string key, string name) 
            : base(key, name)
        {}

        public bool TestRegisterCommand(CommandAbstract command)
        {
            return this.RegisterCommand(command);
        }

        protected override void RegisterCommands()
        {
            //throw new System.NotImplementedException();
        }

        protected override void RegisterProperties()
        {
            //throw new System.NotImplementedException();
        }
    }
}
