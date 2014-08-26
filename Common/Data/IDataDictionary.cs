using System;

namespace OHM.Data
{
    public interface IDataDictionary
    {
        void StoreString(string key, String vlaue);

        void StoreDataDictionary(string key, IDataDictionary store);

        String GetString(string key);

        IDataDictionary GetDataDictionary(string key);

    }
}
