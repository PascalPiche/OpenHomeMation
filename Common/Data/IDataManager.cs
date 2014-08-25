using OHM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Data
{
    public interface IDataManager
    {

        void Init();

        IDataStore GetDataStore(string key);

        IDataStore CreateDataStore(string key);

        bool SaveDataStore(IDataStore dataStore);
    }
}
