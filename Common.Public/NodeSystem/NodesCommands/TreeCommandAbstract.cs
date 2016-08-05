
namespace OHM.Nodes.Commands
{
    public abstract class TreeCommandAbstract : CommandAbstract, ITreeCommand
    {
        protected TreeCommandAbstract(ICommandDefinition definition)
            :base(definition) { }

        public string NodeTreeKey { get { return ((ITreeNode)Node).TreeKey; } }
    }
}
