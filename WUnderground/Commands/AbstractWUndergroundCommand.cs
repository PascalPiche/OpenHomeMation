using OHM.Nodes;
using OHM.RAL.Commands;
using WUnderground.Data;

namespace WUnderground.Commands
{
    public abstract class AbstractWUndergroundCommand : InterfaceCommandAbstract
    {
        protected WUndergroundInterface WUndergroundInterface
        {
            get { return (WUndergroundInterface)base.Interface; }
        }

        public AbstractWUndergroundCommand(string key, string name, string description) 
            : base(key, name, description, null)
        {
          
        }
    }
}
