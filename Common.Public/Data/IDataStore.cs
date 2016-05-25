using System;

namespace OHM.Data
{
    
    public interface IDataStore : IDataDictionary
    {

        String Key { get; }

        Boolean HasUnsavedData { get; }

        Boolean Save();
    }
}
