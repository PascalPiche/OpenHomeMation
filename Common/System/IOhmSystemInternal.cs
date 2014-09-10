using OHM.Data;

namespace OHM.Sys
{
    public interface IOhmSystemInternal : IOhmSystem
    {
        IDataManager DataMng { get; }
    }
}
