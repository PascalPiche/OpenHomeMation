using log4net;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace OHM.Data
{
    public class FileDataManager : DataManagerAbstract
    {
        #region Private Member
        
        private ILoggerManager _loggerMng;
        private ILog _logger;
        private string _filePath;

        #endregion

        #region Public Ctor

        public FileDataManager(string filePath) {
            _filePath = filePath;
        }

        #endregion

        #region Public Api

        public override bool Init(ILoggerManager loggerMng)
        {
            bool result = true;
            _loggerMng = loggerMng;
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

        public override void Shutdown()
        {
            //Save All Data store
            _logger.Debug("Shutdowning");
            SaveDataStoreInMemory();
            _logger.Debug("Shutdowned");
        }

        public override IDataStore GetOrCreateDataStore(string key)
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

                //Add data store in the memory map
                StoreDataStoreInMemory(key, newDataStore);

                //Set result to the new value
                result = newDataStore;
                _logger.Debug("Created new DataStore: " + key);
            }

            //Return result;
            return result;
        }

        public override bool SaveDataStore(IDataStore dataStore)
        {
            _logger.Debug("Saving DataStore: " + dataStore.Key);
            DataStoreToFile(dataStore);
            return true;
        }

        #endregion

        #region Internal

        internal override IDataStore GetDataStore(string key)
        {
            //Check in memory first
            IDataStore result = GetDataStoreFromMemory(key);

            //Try directly with file on the drive
            if (result == null)
            {
                string path = BuildDataStorePath(key);
                if (File.Exists(path))
                {
                    result = DataStoreFromFile(path);

                    //Store result in memory
                    StoreDataStoreInMemory(key, result);
                }
            }

            return result;
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
