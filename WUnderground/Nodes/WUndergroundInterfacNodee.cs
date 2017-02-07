using OHM.Data;
using OHM.Nodes;
using OHM.Nodes.ALR;
using System;
using System.Collections;
using System.Collections.Generic;
using WUnderground.Commands;

namespace WUnderground.Nodes
{
    public class WUndergroundInterfaceNode : ALRInterfaceAbstractNode
    {
        #region Private Members

        private IDataDictionary _registeredAccounts;

        #endregion

        #region Public Ctor

        public WUndergroundInterfaceNode()
            : base("WUndergroundInterface", "WUnderground")
        { /*Nothing to initialize*/ }

        #endregion

        #region Protected Methods

        protected override void Start()
        {
            //Get DataDictionary For installed Controllers
            _registeredAccounts = DataStore.GetOrCreateDataDictionary("registeredAccounts");

            //Load registered controllers
            LoadRegisteredAccounts();
        }

        protected override bool Shutdown()
        {
            return true;
        }

        protected override AbstractPowerNode CreateNodeInstance(string model, string key, string name, IDictionary<string, object> options)
        {
            AbstractPowerNode result = null;
            switch (model)
            {
                case "Account":
                    result = new AccountNode(key, name, key);
                    break;
                case "station":
                    result = new StationNode(key, name, (Int32)options["zip"], (Int32)options["magic"], options["wmo"] as string);
                    break;
                case "station-condition":
                    result = new StationConditionNode(key, name);
                    break;
                default:
                    throw new NotImplementedException("model not found");
            }
            return result;
        }

        protected override void RegisterCommands()
        {
            //Create Commands
            RegisterCommand(new AddAccount());
        }

        protected override bool RegisterProperties()
        {
            //No properties to register
            return true;
        }
        
        #endregion 

        #region Internal Methods

        internal bool CreateAccountCommand(string username, string keyId) 
        {
            bool result = false;
            if (_registeredAccounts.ContainKey(username))
            {
                Logger.Error("Account : " + username + " already exist, cannot create duplicate account");
            } else if (CreateAccountNode(username, keyId))
            {
                //Store new account
                IDataDictionary accountsMetaInfo = _registeredAccounts.GetOrCreateDataDictionary(keyId);
                accountsMetaInfo.StoreString("username", username);
                accountsMetaInfo.StoreString("key", keyId);

                Logger.Info("Saving new account : " + username);
                DataStore.Save();
                result = true;
            }
            return result;
        }

        internal bool RemoveAccountCommand(AbstractPowerNode node)
        {
            bool result = false;
            if (_registeredAccounts.ContainKey(node.Key))
            {
                if (RemoveChild(node.Key))
                {
                    _registeredAccounts.RemoveKey(node.Key);
                    result = DataStore.Save();
                }
            }
            return result;
        }

        internal bool AddStationCommandExecution(AccountNode node, string locationName, int zip, int magic, string wmo)
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
                    result = true;
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

        #region Private Methods

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

                        GetAccountNodeFromUsername(username).AddLocation(locationKey, zip, magic, wmo);
                    }

                } else {
                    Logger.Error("Error will creating saved accound node :  " + username);
                }
            }
        }

        private bool CreateAccountNode(string username, string key) {
            IDictionary<string, object> paramsDic = new Dictionary<string, object>();
            paramsDic.Add("username", username);

            AccountNode account = CreateChildNode("Account", key, "Account : " + username, paramsDic) as AccountNode;
            if (account != null)
            {
                return true;
            }
            return false;
        }

        private AccountNode GetAccountNodeFromUsername(string username)
        {
            return (AccountNode)FindDirectChild(username);
        }

        #endregion
    }
}
