using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Data
{
    public interface IDataStore
    {

        string Key { get; }

        void StoreString(string key, String vlaue);

        void StoreDataStore(string key, IDataStore store);

        String GetString(string key);

        IDataStore GetDataStore(string key);

        bool Save();
    }
}
