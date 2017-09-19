using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;

namespace OHM.Data
{
    public abstract class DataManagerAbstract : AbstractPowerNode, IDataManager
    {
        #region Private members

        private IDictionary<String, IDataStore> _inMemoryDataStore = new Dictionary<string, IDataStore>();

        #endregion

        protected DataManagerAbstract()
            : base("data-manager", "Data manager", SystemNodeStates.creating)
        {

        }

        #region Public Methods

        public abstract bool Init(ILoggerManager loggerMng);
       
        public abstract void Shutdown();

        public abstract IDataStore GetOrCreateDataStore(string key);

        public abstract bool SaveDataStore(IDataStore dataStore);

        #endregion

        #region Protected Methods

        protected bool StoreDataStoreInMemory(string key, IDataStore dataStore) {
            _inMemoryDataStore.Add(key, dataStore);
            return true;
        }

        protected IDataStore GetDataStoreFromMemory(string key)
        {
            IDataStore result = null;
            if (_inMemoryDataStore.ContainsKey(key))
            {
                result = _inMemoryDataStore[key];
            }
            return result;
        }

        protected void SaveDataStoreInMemory()
        {
            var enumerator = _inMemoryDataStore.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SaveDataStore(enumerator.Current.Value);
            }
        }
        
        #endregion

        #region Internal Method

        internal abstract IDataStore GetDataStore(string key);

        #endregion 
    
        protected override void RegisterCommands()
        {
            //throw new NotImplementedException();
        }

        protected override bool RegisterProperties()
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
