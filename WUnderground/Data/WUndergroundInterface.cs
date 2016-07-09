using OHM.Data;
using OHM.Logger;
using OHM.RAL;
using WUnderground.Commands;
using WUnderground.Data;

namespace WUnderground.Data
{
    public class WUndergroundInterface : InterfaceAbstract
    {
        private IDataDictionary _registeredAccounts;

        #region Public Ctor

        public WUndergroundInterface(ILogger logger)
            : base("WUndergroundInterface", "WUnderground", logger)
        {
            //Create Commands
            this.RegisterCommand(new AddAccount(this));
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
            if (_registeredAccounts.ContainKey(username))
            {
                Logger.Error("Account : " + username + " already exist, cannot create duplicate account");
                return false;
            }

            if (CreateAccountNode(username, keyId))
            {
                //Store new account
                IDataDictionary accountsMetaInfo = _registeredAccounts.GetOrCreateDataDictionary(username);
                accountsMetaInfo.StoreString("username", username);
                accountsMetaInfo.StoreString("key", keyId);
                _registeredAccounts.StoreDataDictionary(username, accountsMetaInfo);

                Logger.Info("Saving new account : " + username);
                this.DataStore.Save();
                return true;
            }
            return false;
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
            IDataDictionary dataAccount = _registeredAccounts.GetDataDictionary(node.Key);
            IDataDictionary accountLocations = dataAccount.GetOrCreateDataDictionary("locations");

            if (!accountLocations.ContainKey(locationName))
            {
                
                if (node.AddLocation(new Station(locationName, locationName, this.Logger, zip, magic, wmo)))
                {
                    IDataDictionary locationData = accountLocations.GetOrCreateDataDictionary(locationName);
                    locationData.StoreString("name", locationName);
                    locationData.StoreInt32("zip", zip);
                    locationData.StoreInt32("magic", magic);
                    locationData.StoreString("wmo", wmo);

                    accountLocations.StoreDataDictionary(locationName, locationData);
                    return DataStore.Save();
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
                        int zip = locationData.GetInt32("zip");
                        int magic = locationData.GetInt32("magic");
                        string wmo = locationData.GetString("wmo");

                        this.GetAccountNode(username).AddLocation(new Station(locationKey, locationKey, this.Logger, zip, magic, wmo));
                    }

                } else {
                    Logger.Error("WUnderground : Error will creating saved accound node :  " + username);
                }
            }
        }

        private bool CreateAccountNode(string username, string key) {
            //Create Account
            Account account = new Account(username, "Account : " + username, this.Logger, key);
            return this.AddChild(account);
        }

        private Account GetAccountNode(string key)
        {
            return (Account)this.GetChild(key);
        }

        #endregion

    }
}
