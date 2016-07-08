using OHM.Data;
using OHM.SYS;

namespace OHM.VAL
{
    public interface IVrManager
    {

        bool Init(IDataStore data, IOhmSystemInternal system);

        bool RegisterVrType(string key, IVrType vrType/*, IOhmSystemInternal system*/);

    }
}
