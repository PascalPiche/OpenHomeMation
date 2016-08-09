using System;

namespace OHM.Data
{

    [Serializable]
    public sealed class DataStore : DataDictionary, IDataStore
    {

        private string _key;

        [NonSerialized]
        private IDataManager _mng;

        internal DataStore(string key)
        {
            _key = key;
        }

        internal void Init(IDataManager mng)
        {
            _mng = mng;
        }

        public bool Save()
        {
            if (_mng != null && _key != null && _key.Trim().Length > 0)
            {
                return _mng.SaveDataStore(this);
            }
            return false;
        }

        public string Key
        {
            get { return _key; }
        }

        public bool HasUnsavedData
        {
            get { throw new NotImplementedException(); }
        }
    }
}
