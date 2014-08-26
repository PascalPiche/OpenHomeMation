using System;
using System.Collections.Generic;

namespace OHM.Data
{
    [Serializable]
    public class DataDictionary : IDataDictionary
    {
        private Dictionary<string, IDataValue> _dataValues = new Dictionary<string, IDataValue>();

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

        public void StoreString(string key, string value)
        {
            StoreValue(key, new DataValueString(value));
        }

        public void StoreDataDictionary(string key, IDataDictionary store)
        {
            StoreValue(key, new DataValueDictionary(store));
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

        public IDataDictionary GetDataDictionary(string key)
        {
            var value = GetValue(key);
            if (value != null)
            {
                return ((DataValueDictionary)value).Value;
            }
            return null;
        }
    }
}
