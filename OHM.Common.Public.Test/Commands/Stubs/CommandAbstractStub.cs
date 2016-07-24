using OHM.Nodes;
using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands.Stubs
{
    public class CommandAbstractStub : CommandAbstract
    {
        public CommandAbstractStub(string key, string name)
            : base(key, name) { }

        public CommandAbstractStub(string key, string name, string description)
            : base(key, name, description) { }

        public CommandAbstractStub(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(key, name, description, argumentsDefinition) { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return true;
        }

        public INode GetAssignedNodeForTest() {
            return this.Node;
        }
    }
}
