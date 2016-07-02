using OHM.Commands;
using OHM.Nodes;
using WUnderground.Data;

namespace WUnderground.Commands
{
    public abstract class AbstractWUndergroundCommand : InterfaceCommandAbstract
    {

        protected WUndergroundInterface WUndergroundInterface
        {
            get { return (WUndergroundInterface)base.Interface; }
        }

        public AbstractWUndergroundCommand(INode node, string key, string name, string description) 
            : base(node, key, name, description, null)
        {
          
        }
    }
}
