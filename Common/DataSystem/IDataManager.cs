
namespace OHM.Data
{
    public interface IDataManager
    {

        bool Init();

        void Shutdown();

        IDataStore GetDataStore(string key);

        IDataStore GetOrCreateDataStore(string key);

        bool SaveDataStore(IDataStore dataStore);
    }
}
