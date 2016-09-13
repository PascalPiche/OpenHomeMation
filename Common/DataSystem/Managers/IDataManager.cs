using OHM.Logger;

namespace OHM.Data
{
    public interface IDataManager
    {
        bool Init(ILoggerManager loggerMng);

        void Shutdown();

        IDataStore GetOrCreateDataStore(string key);

        bool SaveDataStore(IDataStore dataStore);
    }
}
