using OHM.Nodes.ALR;
using OHM.Nodes.Commands;

namespace OHM.Common.Public.Test.Nodes.Stubs
{
    public class RalNodeAbstractStub : ALRAbstractTreeNode
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

        protected override bool RegisterProperties()
        {
            throw new System.NotImplementedException();
        }
    }
}
