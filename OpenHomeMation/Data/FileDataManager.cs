using OHM.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace OHM.Data
{
    public class FileDataManager : IDataManager
    {
        #region Private Member
        
        private ILoggerManager _loggerMng;
        private ILogger _logger;
        private string _filePath;
        private IDictionary<String, IDataStore> _loadedDataStore = new Dictionary<string,IDataStore>();

        #endregion

        #region Public Ctor

        public FileDataManager(ILoggerManager loggerMng, string filePath) {
            _filePath = filePath;
            _loggerMng = loggerMng;
        }

        #endregion

        #region Public Api

        public bool Init()
        {
            bool result = true;
            _logger = _loggerMng.GetLogger("FileDataManager");
            _logger.Debug("Initing");

            if (!Directory.Exists(_filePath))
            {
                _logger.Debug("Creating Data Directory : " + _filePath);
                try
                {
                    Directory.CreateDirectory(_filePath);
                }
                catch (Exception ex)
                {
                    result = false;
                    _logger.Error("Can not create data directory : " + _filePath, ex);
                }
            }

            if (result)
            {
                _logger.Info("Inited");
            }
            return result;
        }

        public void Shutdown()
        {
            //Save All Data store
            _logger.Debug("Shutdowning");
            var enumerator = _loadedDataStore.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SaveDataStore(enumerator.Current.Value);
            }
            _logger.Debug("Shutdowned");
        }

        public IDataStore GetDataStore(string key)
        {
            IDataStore result = null;

            if (_loadedDataStore.ContainsKey(key)) {
                result = _loadedDataStore[key];
            }
            else 
            {
                string path = BuildDataStorePath(key);
                if (File.Exists(path))
                {
                    IDataStore newDataStore = DataStoreFromFile(path);
                    _loadedDataStore.Add(key, newDataStore);
                    result = newDataStore;
                }
            }

            return result;
        }

        public IDataStore GetOrCreateDataStore(string key)
        {
            //Check if DataStore exist
            IDataStore result = GetDataStore(key);

            if (result == null)
            {
                _logger.Debug("Creating new DataStore: " + key);

                //Create object
                DataStore newDataStore = new DataStore(key);


                //Init object
                newDataStore.Init(this);

                //Save Data Store to disk
                if (!DataStoreToFile(newDataStore))
                {
                    //Save To drive Failed
                    _logger.Error("New Datastore " + key + " only created in memory");
                }

                //Add data store to the loaded dataStore
                _loadedDataStore.Add(key, newDataStore);

                //Set result to the new value
                result = newDataStore;
                _logger.Debug("Created new DataStore: " + key);
            }

            //Return result;
            return result;
        }

        public bool SaveDataStore(IDataStore dataStore)
        {
            _logger.Debug("Saving DataStore: " + dataStore.Key);
            DataStoreToFile(dataStore);
            return true;
        }

        #endregion

        #region Private

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
            listKnowTypes.Add(typeof(DataValueBool));
            listKnowTypes.Add(typeof(DataValueInt));
            listKnowTypes.Add(typeof(DataValueString));
            return listKnowTypes;
        }

        private bool DataStoreToFile(IDataStore data) 
        {
            bool result = false;
            string path = BuildDataStorePath(data.Key);

            _logger.Debug("Writting DataStore on drive with path: " + path);
            try
            {
                DataContractSerializer formatter = new DataContractSerializer(typeof(DataStore), GetSerializationTypes());
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                var writter = File.OpenWrite(path);
                formatter.WriteObject(writter, data);
                writter.Flush();
                writter.Close();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.Error("An Error occured while saving DataStore with path: "  + path, ex);
                result = false;
            }
            _logger.Debug("Writted DataStore on drive with path: " + path);
            return result;
        }

        private string BuildDataStorePath(string key)
        {
            return _filePath + key + ".data";
        }

        #endregion

    }
}
