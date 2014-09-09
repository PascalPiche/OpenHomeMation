using System;

namespace OHM.Data
{
    
    public interface IDataStore : IDataDictionary
    {

        string Key { get; }

        bool Save();
    }
}
