using System;
using System.Collections.Generic;

namespace OHM.Data
{
    public interface IDataDictionary
    {
        IEnumerable<string> Keys { get; }

        bool ContainKey(string key);

        bool RemoveKey(string key);

        bool GetBool(string key);

        void StoreBool(string key, bool value);

        void StoreInt32(string key, Int32 value);

        int GetInt32(string key);

        void StoreString(string key, string value);

        String GetString(string key);

        IDataDictionary GetOrCreateDataDictionary(string key);

        //IDataDictionary GetDataDictionary(string key);

        //void StoreDataDictionary(string key, IDataDictionary store);
    }
}
