using System.Collections.Generic;

namespace OHM.Data
{
    public interface IDataDictionary
    {
        IEnumerable<string> Keys { get; }

        IDataDictionary GetOrCreateDataDictionary(string key);

        bool ContainKey(string key);

        bool RemoveKey(string key);

        bool GetBool(string key);

        void StoreBool(string key, bool value);

        void StoreInt32(string key, int value);

        int GetInt32(string key);

        void StoreString(string key, string value);

        string GetString(string key);
    }
}
