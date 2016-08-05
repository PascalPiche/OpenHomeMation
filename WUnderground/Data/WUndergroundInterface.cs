using OHM.Data;
using OHM.Nodes;
using OHM.RAL;
using System;
using System.Collections;
using System.Collections.Generic;
using WUnderground.Commands;

namespace WUnderground.Data
{
    public class WUndergroundInterface : ALRInterfaceAbstractNode
    {
        private IDataDictionary _registeredAccounts;

        #region Public Ctor

        public WUndergroundInterface()
            : base("WUndergroundInterface", "WUnderground")
        {
            
        }

        #endregion

        #region Protected functions

        protected override void Start()
        {
            //Get DataDictionary For installed Controllers
            _registeredAccounts = DataStore.GetOrCreateDataDictionary("registeredAccounts");

            //Load registered controllers
            LoadRegisteredAccounts();
        }

        protected override void Shutdown()
        {
            
        }

        #endregion 

        #region Internal functions

        internal bool CreateAccountCommand(string username, string keyId) 
        {
            bool result = false;
            if (_registeredAccounts.ContainKey(username))
            {
                Logger.Error("Account : " + username + " already exist, cannot create duplicate account");
            } else if (CreateAccountNode(username, keyId))
            {
                //Store new account
                IDataDictionary accountsMetaInfo = _registeredAccounts.GetOrCreateDataDictionary(username);
                accountsMetaInfo.StoreString("username", username);
                accountsMetaInfo.StoreString("key", keyId);
                //_registeredAccounts.StoreDataDictionary(username, accountsMetaInfo);

                Logger.Info("Saving new account : " + username);
                this.DataStore.Save();
                result = true;
            }
            return result;
        }

        internal bool RemoveAccountCommand(Account node)
        {
            if (_registeredAccounts.ContainKey(node.Key))
            {
                if (this.RemoveChild(node))
                {
                    _registeredAccounts.RemoveKey(node.Key);
                    return this.DataStore.Save();
                }
            }
            
            return false;
        }

        internal bool AddStationCommandExecution(Account node, string locationName, int zip, int magic, string wmo)
        {
            bool result = false;
            IDataDictionary dataAccount = _registeredAccounts.GetOrCreateDataDictionary(node.Key);
            IDataDictionary accountLocations = dataAccount.GetOrCreateDataDictionary("locations");

            if (!accountLocations.ContainKey(locationName))
            {
                
                if (node.AddLocation(locationName, zip, magic, wmo))
                {
                    IDataDictionary locationData = accountLocations.GetOrCreateDataDictionary(locationName);
                    locationData.StoreString("name", locationName);
                    locationData.StoreInt32("zip", zip);
                    locationData.StoreInt32("magic", magic);
                    locationData.StoreString("wmo", wmo);

                    DataStore.Save();

                    return true;
                }
                else
                {
                    Logger.Error("Error when creating new location " + locationName);
                }
            }
            else
            {
                Logger.Error("Location " + locationName + " already exists");
            }

            return result;
        }

        #endregion

        #region Private functions

        private void LoadRegisteredAccounts()
        {
            foreach (string itemKey in _registeredAccounts.Keys)
            {
                IDataDictionary data = _registeredAccounts.GetOrCreateDataDictionary(itemKey);
                string username = data.GetString("username");
                string key = data.GetString("key");
                
                if (CreateAccountNode(username, key))
                {
                    //Load registered location
                    IDataDictionary locations = data.GetOrCreateDataDictionary("locations");
                    foreach (string locationKey in locations.Keys)
                    {
                        IDataDictionary locationData = locations.GetOrCreateDataDictionary(locationKey);
                        int zip = locationData.GetInt32("zip");
                        int magic = locationData.GetInt32("magic");
                        string wmo = locationData.GetString("wmo");

                        this.GetAccountNode(username).AddLocation(locationKey, zip, magic, wmo);
                    }

                } else {
                    Logger.Error("WUnderground : Error will creating saved accound node :  " + username);
                }
            }
        }

        private bool CreateAccountNode(string username, string key) {

            //Create Account
            IDictionary<string, object> paramsDic = new Dictionary<string, object>();
            paramsDic.Add("username", username);

            Account account = this.CreateChildNode("Account", key, "Account : " + username, paramsDic) as Account;
            if (account != null)
            {
                return true;
            }
            return false;
        }

        private Account GetAccountNode(string key)
        {
            return (Account)this.FindChild(key);
        }

        #endregion

        protected override AbstractNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options)
        {
            AbstractNode result = null; 
            switch (model)
            {
                case "Account":
                    result = new Account(key, name, key);
                    break;
                case "station":
                    result = new Station(key, name, (Int32)options["zip"], (Int32)options["magic"], options["wmo"] as string);
                    break;
                case "station-condition":
                    result = new StationCondition(key, name);
                    break;
                default:

                    break;
            }

            return result;
        }

        protected override void RegisterCommands()
        {
            //Create Commands
            this.RegisterCommand(new AddAccount());
        }

        protected override void RegisterProperties()
        {
            
        }
    }
}
