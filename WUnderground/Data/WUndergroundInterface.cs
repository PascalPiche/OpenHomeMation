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
                //todo: Log warning
                return false;
            }

            if (CreateAccountNode("Account : " + username, keyId))
            {
                //Store new account
                IDataDictionary accountsMetaInfo = _registeredAccounts.GetOrCreateDataDictionary(username);
                accountsMetaInfo.StoreString("username", username);
                accountsMetaInfo.StoreString("key", keyId);
                _registeredAccounts.StoreDataDictionary(username, accountsMetaInfo);

                //todo: Log action
                this.DataStore.Save();
                return true;
            }
            return false;
        }

        internal bool RemoveAccountCommand(Account node)
        {

            return false;
        }

        internal bool CreateLocationCommand(Account node, string locationName, int zip, int magic, int wmo)
        {
            IDataDictionary dataAccount = _registeredAccounts.GetDataDictionary(node.Key);
            IDataDictionary accountLocations = dataAccount.GetOrCreateDataDictionary("locations");

            if (!accountLocations.ContainsKey(locationName))
            {
                node.AddLocation(null);
            }
            else
            {

            }
            
            return false;
        }

        private void LoadRegisteredAccounts()
        {
            foreach (string item in _registeredAccounts.Keys)
            {
                IDataDictionary data = _registeredAccounts.GetDataDictionary(item);
                string username = data.GetString("username");
                string key = data.GetString("key");
                
                if (CreateAccountNode("Account : " + username, key))
                {
                    //Load registered location

                } else {
                    //todo: Log error
                }
            }
        }

        private bool CreateAccountNode(string username, string key) {
            //Create Account
            var account = new Account(username, username, this.Logger, key);
            return this.AddChild(account);
        }
    }
}
