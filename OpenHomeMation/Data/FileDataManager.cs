using OHM.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace OHM.Data
{
    public class FileDataManager : IDataManager
    {
        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private string _filePath;
        private Dictionary<String, IDataStore> _loadedDataStore = new Dictionary<string,IDataStore>();

        public FileDataManager(ILoggerManager loggerMng, string filePath) {
            _filePath = filePath;
            _loggerMng = loggerMng;
        }

        public void Init()
        {
            _logger = _loggerMng.GetLogger("FileDataManager");
            if (!Directory.Exists(_filePath))
            {
                _logger.Debug("Creating Data Directory : " + _filePath);
                Directory.CreateDirectory(_filePath);
            }
        }

        public IDataStore GetDataStore(string key)
        {
            if (_loadedDataStore.ContainsKey(key)) {

                return _loadedDataStore[key];
            } 
            else 
            {
                string path = BuildDataStorePath(key);
                if (File.Exists(path))
                {
                    IDataStore newDataStore = DataStoreFromFile(path);
                    _loadedDataStore.Add(key, newDataStore);
                    return newDataStore;
                }
            }
            return null;
        }

        public IDataStore GetOrCreateDataStore(string key)
        {
            //Check if DataStore exist
            IDataStore existing = GetDataStore(key);
            if (existing != null)
            {
                return existing;
            }
            else
            {
                DataStore newDataStore = new DataStore(key);
                newDataStore.Init(this);
                DataStoreToFile(newDataStore, BuildDataStorePath(key));
                _loadedDataStore.Add(key, newDataStore);
                return newDataStore;
            }
        }

        public bool SaveDataStore(IDataStore dataStore)
        {
            DataStoreToFile(dataStore, BuildDataStorePath(dataStore.Key));
            return true;
        }

        private IDataStore DataStoreFromFile(string path)
        {
            DataContractSerializer formatter = new DataContractSerializer(typeof(DataStore), GetSerializationTypes());
            var fileStream = File.OpenRead(path);
            DataStore data = (DataStore)formatter.ReadObject(fileStream);
            fileStream.Close();
            data.Init(this);
            return data;
        }

        private IList<Type> GetSerializationTypes()
        {
            IList<Type> listKnowTypes = new List<Type>();
            listKnowTypes.Add(typeof(DataValueDictionary));
            listKnowTypes.Add(typeof(DataDictionary));
            listKnowTypes.Add(typeof(DataValueString));
            listKnowTypes.Add(typeof(DataValueBool));
            return listKnowTypes;
        }

        private void DataStoreToFile(IDataStore data, string path) 
        {
            DataContractSerializer formatter = new DataContractSerializer(typeof(DataStore), GetSerializationTypes());
            if (File.Exists(path)) {
                File.Delete(path);
            }
            var writter = File.OpenWrite(path);
            formatter.WriteObject(writter, data);
            writter.Flush();
            writter.Close();
        }

        private string BuildDataStorePath(string key)
        {
            return _filePath + key + ".data";
        }

       
    }
}
