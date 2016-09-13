using OHM.Nodes;
using OHM.Nodes.Commands;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands.Stubs
{
    public class TreeCommandAbstractStub : AbstractTreeCommand
    {
        /*public TreeCommandAbstractStub(string key, string name)
            : base(key, name) { }

        public TreeCommandAbstractStub(string key, string name, string description)
            : base(key, name, description) { }
        */
        /*public TreeCommandAbstractStub(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(key, name, description, argumentsDefinition) { }
        */

        protected TreeCommandAbstractStub(ICommandDefinition definition)
            :base(definition) { }


        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return true;
        }

        /*public ITreePowerNode GetAssignedNodeForTest() {
            return this.Node;
        }*/
    }
}
