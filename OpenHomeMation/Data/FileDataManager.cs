using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace OHM.Data
{
    public class FileDataManager : IDataManager
    {

        private string _filePath = AppDomain.CurrentDomain.BaseDirectory + "\\data\\";
        private Dictionary<String, IDataStore> _loadedDataStore = new Dictionary<string,IDataStore>();

        public void Init()
        {

            if (!Directory.Exists(_filePath))
            {
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

        public IDataStore CreateDataStore(string key)
        {
            DataStore newDataStore = new DataStore(key);
            newDataStore.Init(this);
            DataStoreToFile(newDataStore, BuildDataStorePath(key));
            _loadedDataStore.Add(key, newDataStore);
            return newDataStore;
        }

        private IDataStore DataStoreFromFile(string path)
        {
            DataContractSerializer formatter = new DataContractSerializer(typeof(DataStore), GetSerializationTypes());
            DataStore data = (DataStore)formatter.ReadObject(File.OpenRead(path));
            data.Init(this);
            return data;
        }

        private IList<Type> GetSerializationTypes()
        {
            IList<Type> listKnowTypes = new List<Type>();
            listKnowTypes.Add(typeof(DataValueStore));
            listKnowTypes.Add(typeof(DataValueString));
            return listKnowTypes;
        }

        private void DataStoreToFile(IDataStore data, string path) 
        {
            DataContractSerializer formatter = new DataContractSerializer(typeof(DataStore), GetSerializationTypes());
            var writter = File.OpenWrite(path);
            formatter.WriteObject(writter, data);
            writter.Close();
        }

        private string BuildDataStorePath(string key)
        {
            return _filePath + key + ".data";
        }

        public bool SaveDataStore(IDataStore dataStore)
        {
            DataStoreToFile(dataStore, BuildDataStorePath(dataStore.Key));
            return true;
        }
    }
}
