using System;
using System.Collections;
using System.Collections.Generic;

namespace OHM.Data
{
    public interface IDataDictionary
    {
        void StoreString(string key, String vlaue);

        void StoreDataDictionary(string key, IDataDictionary store);

        String GetString(string key);

        IEnumerable<string> GetKeys();

        IDataDictionary GetDataDictionary(string key);

    }
}
