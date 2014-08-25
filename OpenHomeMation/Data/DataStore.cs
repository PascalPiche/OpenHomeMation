using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Data
{
    [Serializable]
    public class DataStore : IDataStore
    {

        private string _key;
        private Dictionary<string, IDataValue> _dataValues = new Dictionary<string, IDataValue>();

        [NonSerialized]
        private IDataManager _mng;

        public DataStore()
        {

        }

        public DataStore(string key)
        {
            _key = key;
        }

        internal void Init(IDataManager mng)
        {
            _mng = mng;
        }

        protected void StoreValue(string key, IDataValue obj)
        {
            _dataValues[key] = obj;
        }

        protected IDataValue GetValue(string key)
        {
            IDataValue value;
            if (_dataValues.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        public bool Save()
        {
            if (_mng != null)
            {
                return _mng.SaveDataStore(this);
            }
            return false;
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public void StoreString(string key, string value)
        {
            StoreValue(key, new DataValueString(value));
        }

        public void StoreDataStore(string key, IDataStore store)
        {
            StoreValue(key, new DataValueStore(store));
        }

        public string GetString(string key)
        {
            var value = GetValue(key);
            if (value != null)
            {
                return ((DataValueString)value).Value;
            }
            return "";
        }

        public IDataStore GetDataStore(string key)
        {
            var value = GetValue(key);
            if (value != null)
            {
                return ((DataValueStore)value).Value;
            }
            return null;
        }
    }
}
