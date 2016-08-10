using OHM.Nodes.ALR.Commands;
using WUnderground.Nodes;

namespace WUnderground.Commands
{
    public abstract class AbstractWUndergroundCommand : InterfaceCommandAbstract
    {
        protected WUndergroundInterfaceNode WUndergroundInterface
        {
            get { return (WUndergroundInterfaceNode)base.Interface; }
        }

        public AbstractWUndergroundCommand(string key, string name, string description) 
            : base(key, name, description, null)
        {
          
        }
    }
}
