
namespace OHM.Data
{
    public interface IDataStore : IDataDictionary
    {
        string Key { get; }

        //bool HasUnsavedData { get; }

        bool Save();
    }
}
