using System;
using System.Collections;
using System.Collections.Generic;

namespace OHM.Data
{
    public interface IDataDictionary
    {

        IEnumerable<string> Keys { get; }


        void StoreString(string key, string value);

        void StoreBool(string key, bool value);

        void StoreDataDictionary(string key, IDataDictionary store);


        String GetString(string key);

        bool GetBool(string key);

        IDataDictionary GetDataDictionary(string key);

    }
}
