using OHM.Data;
using OHM.Interfaces;
using OHM.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WUnderground.Commands;
using WUnderground.Data;

namespace WUnderground.Data
{
    public class WUndergroundInterface : InterfaceAbstract
    {
        private IDataDictionary _registeredAccounts;

        public WUndergroundInterface(ILogger logger)
            : base("WUndergroundInterface", "WUnderground", logger)
        {
            //Create Commands
            this.RegisterCommand(new AddAccount(this));
        }

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

        internal bool CreateAccountCommand(string username, string keyId) 
        {
            if (_registeredAccounts.ContainsKey(username))
            {
                Logger.Error("WUnderground Account : " + username + " already exist, cannot create duplicate account");
                return false;
            }

            if (CreateAccountNode(username, keyId))
            {
                //Store new account
                IDataDictionary accountsMetaInfo = _registeredAccounts.GetOrCreateDataDictionary(username);
                accountsMetaInfo.StoreString("username", username);
                accountsMetaInfo.StoreString("key", keyId);
                _registeredAccounts.StoreDataDictionary(username, accountsMetaInfo);

                Logger.Info("WUnderground : Saving new account : " + username);
                this.DataStore.Save();
                return true;
            }
            return false;
        }

        internal bool RemoveAccountCommand(Account node)
        {
            if (_registeredAccounts.ContainsKey(node.Key))
            {
                if (this.RemoveChild(node))
                {
                    _registeredAccounts.RemoveKey(node.Key);
                    return this.DataStore.Save();
                }
            }
            
            return false;
        }

        internal bool CreateLocationCommand(Account node, string locationName, int zip, int magic, int wmo)
        {
            Boolean result = false;
            IDataDictionary dataAccount = _registeredAccounts.GetDataDictionary(node.Key);
            IDataDictionary accountLocations = dataAccount.GetOrCreateDataDictionary("locations");
            //accountLocations.StoreDataDictionary("locations", accountLocations);

            if (!accountLocations.ContainsKey(locationName))
            {
                
                if (node.AddLocation(new Station(locationName, locationName, this.Logger, zip, magic, wmo)))
                {
                    var locationData = accountLocations.GetOrCreateDataDictionary(locationName);
                    locationData.StoreString("name", locationName);
                    locationData.StoreInt("zip", zip);
                    locationData.StoreInt("magic", magic);
                    locationData.StoreInt("wmo", wmo);

                    accountLocations.StoreDataDictionary(locationName, locationData);
                    return DataStore.Save();
                }
                else
                {
                    Logger.Error("WUnderground : Error when creating new location " + locationName);
                }
            }
            else
            {
                Logger.Error("WUnderground : Location " + locationName + " already exists");
            }

            return result;
        }

        private void LoadRegisteredAccounts()
        {
            foreach (string itemKey in _registeredAccounts.Keys)
            {
                IDataDictionary data = _registeredAccounts.GetDataDictionary(itemKey);
                string username = data.GetString("username");
                string key = data.GetString("key");
                
                if (CreateAccountNode(username, key))
                {
                    //Load registered location
                    IDataDictionary locations = data.GetOrCreateDataDictionary("locations");
                    foreach (string locationKey in locations.Keys)
                    {
                        IDataDictionary locationData = locations.GetDataDictionary(locationKey);
                        int zip = locationData.GetInt("zip");
                        int magic = locationData.GetInt("magic");
                        int wmo = locationData.GetInt("wmo");

                        this.GetAccountNode(username).AddLocation(new Station(locationKey, locationKey, this.Logger, zip, magic, wmo));
                    }

                } else {
                    Logger.Error("WUnderground : Error will creating saved accound node :  " + username);
                }
            }
        }

        private bool CreateAccountNode(string username, string key) {
            //Create Account
            var account = new Account(username, "Account : " + username, this.Logger, key);
            return this.AddChild(account);
        }

        private Account GetAccountNode(string key)
        {
            return (Account)this.GetChild(key);
        }
    }
}
