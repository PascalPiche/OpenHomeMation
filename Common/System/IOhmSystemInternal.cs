using OHM.Data;
using OHM.RAL;

namespace OHM.SYS
{
    public interface IOhmSystemInternal : IOhmSystemPlugins
    {

        IDataStore GetOrCreateDataStore(string key);

        IOhmSystemInterfaceGateway GetInterfaceGateway(IInterface interf);

        IAPI API { get; }
    }
}
