using OHM.Commands;
using OHM.Interfaces;
using OHM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Data;

namespace WUnderground.Commands
{
    public abstract class AbstractWUndergroundCommand : CommandAbstract
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
