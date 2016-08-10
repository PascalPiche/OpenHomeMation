using OHM.Data;
using OHM.Nodes.ALR;

namespace OHM.SYS
{
    public interface IOhmSystemInternal : IOhmSystemPlugins
    {

        IDataStore GetOrCreateDataStore(string key);

        IOhmSystemInterfaceGateway GetInterfaceGateway(IALRInterface interf);

        IAPI API { get; }
    }
}
