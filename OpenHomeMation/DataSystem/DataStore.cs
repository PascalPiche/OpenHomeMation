using System;

namespace OHM.Data
{

    [Serializable]
    public sealed class DataStore : DataDictionary, IDataStore
    {

        #region Private Members

        private string _key;

        [NonSerialized]
        private IDataManager _mng;

        #endregion

        #region Internal Ctor

        internal DataStore(string key)
        {
            _key = key;
        }

        #endregion

        #region Internal Methods

        internal void Init(IDataManager mng)
        {
            _mng = mng;
        }

        #endregion

        #region Public Properties

        public string Key
        {
            get { return _key; }
        }

        #endregion

        #region Public Methods

        public bool Save()
        {
            if (_mng != null && _key != null && _key.Trim().Length > 0)
            {
                return _mng.SaveDataStore(this);
            }
            return false;
        }

        #endregion

        /*public bool HasUnsavedData
        {
            get { throw new NotImplementedException(); }
        }*/
    }
}
