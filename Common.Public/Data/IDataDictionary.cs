using System;
using System.Collections.Generic;

namespace OHM.Data
{
    public interface IDataDictionary
    {
        IEnumerable<string> Keys { get; }

        void StoreInt32(string key, Int32 value);

        int GetInt32(string key);

        void StoreString(string key, string value);

        String GetString(string key);

        void StoreDataDictionary(string key, IDataDictionary store);

        IDataDictionary GetOrCreateDataDictionary(string key);

        bool GetBool(string key);

        void StoreBool(string key, bool value);

        IDataDictionary GetDataDictionary(string key);

        bool RemoveKey(string key);

        bool ContainKey(string key);

    }
}
