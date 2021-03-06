﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OHM.Data
{
    [Serializable]
    public class DataDictionary : IDataDictionary
    {
        private Dictionary<string, IDataValue> _dataValues = new Dictionary<string, IDataValue>();

        #region Public Properties

        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Keys
        {
            get {
                return _dataValues.Keys;
            }
        }

        #endregion

        #region Public API

        #region String

        public void StoreString(string key, string value)
        {
            StoreValue(key, new DataValueString(value));
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

        #endregion

        #region Dictionary

        public void StoreDataDictionary(string key, IDataDictionary store)
        {
            StoreValue(key, new DataValueDictionary(store));
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

        public IDataDictionary GetOrCreateDataDictionary(string key)
        {
            var result = GetDataDictionary(key);

            if (result == null) 
            {
                result = new DataDictionary();
                this.StoreDataDictionary(key, result);
            }

            return result;
        }

        #endregion

        #region bool

        public void StoreBool(string key, bool value)
        {
            StoreValue(key, new DataValueBool(value));
        }

        public bool GetBool(string key)
        {
            var value = GetValue(key);
            if (value != null)
            {
                return ((DataValueBool)value).Value;
            }
            return false;
        }

        #endregion

        #region Int32

        public void StoreInt32(string key, int value)
        {
            StoreValue(key, new DataValueInt(value));
        }

        public int GetInt32(string key)
        {
            var value = GetValue(key);
            if (value != null)
            {
                return ((DataValueInt)value).Value;
            }
            return -1;
        }

        #endregion

        public bool RemoveKey(String key)
        {
            return _dataValues.Remove(key);
        }

        public bool ContainKey(string key)
        {
            return _dataValues.ContainsKey(key);
        }
        
        #endregion

        #region Private

        private void StoreValue(string key, IDataValue obj)
        {
            _dataValues[key] = obj;
        }

        private IDataValue GetValue(string key)
        {
            IDataValue value;
            if (_dataValues.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        #endregion
    }
}
