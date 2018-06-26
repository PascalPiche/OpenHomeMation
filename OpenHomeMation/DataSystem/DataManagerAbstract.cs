using OHM.Logger;
using OHM.Nodes;
using System;
using System.Collections.Generic;

namespace OHM.Data
{
    /// <summary>
    /// Abstract class for all Data manager in the system.
    /// Implements AbstractPowerNode and IDataManager partialy
    /// </summary>
    public abstract class DataManagerAbstract : AbstractPowerNode, IDataManager
    {
        #region Private members

        private IDictionary<String, IDataStore> _inMemoryDataStore = new Dictionary<string, IDataStore>();

        #endregion

        #region Protected Ctor

        protected DataManagerAbstract()
            : base("data-manager", "Data manager")
        {

        }

        #endregion

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

        protected void SaveDataStoreFromMemory()
        {
            var enumerator = _inMemoryDataStore.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SaveDataStore(enumerator.Current.Value);
            }
        }

        protected override void RegisterCommands()
        {
            //Nothing to do?
        }

        protected override bool RegisterProperties()
        {
            //Nothing to do?
            return true;
        }

        #endregion

        #region Internal Method

        internal abstract IDataStore GetDataStore(string key);

        #endregion         
    }
}
