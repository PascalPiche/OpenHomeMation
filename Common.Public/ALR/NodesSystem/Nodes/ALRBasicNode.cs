
namespace OHM.Nodes.ALR
{
    public class ALRBasicNode : ALRAbstractTreeNode
    {
        public ALRBasicNode(string key, string name) : base(key, name) {}

        protected override void RegisterCommands() {}

        protected override bool RegisterProperties()
        {
            return true;
        }
    }
}
