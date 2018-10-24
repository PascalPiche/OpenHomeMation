﻿using log4net;
using OHM.Data;
using OHM.Logger;
using OHM.Nodes.ALV;
using OHM.Plugins;
using OHM.SYS;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OHM.Managers.ALV
{
    public class VrManager : IVrManager, IVrNodeCreator
    {
        #region Private members

        private ILoggerManager _loggerMng;
        private ILog _logger;
        private IDataStore _data;
        private IDictionary<string, IVrNodeCreator> _registeredType = new Dictionary<string, IVrNodeCreator>();
        private ICollection<IVrType> _rootNodes = new ObservableCollection<IVrType>();

        #endregion

        #region Public Ctor

        public VrManager()
        {
            
        }

        #endregion

        #region Public Methods

        public bool Init(ILoggerManager loggerMng, IDataStore data, IOhmSystemInternal system)
        {
            _loggerMng = loggerMng;
            _data = data;
            _logger = _loggerMng.GetLogger("VrManager");

            //Launch Main Vr Node

           /* _dataRegisteredInterfaces = _data.GetDataDictionary("RegisteredInterfaces");
            if (_dataRegisteredInterfaces == null)
            {
                _dataRegisteredInterfaces = new DataDictionary();
                _data.StoreDataDictionary("RegisteredInterfaces", _dataRegisteredInterfaces);
                _data.Save();
            }
            loadRegisteredInterfaces(system);*/

            //Register basic type

            return true;
        }

        public IVrType CreateVrNode(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool RegisterVrType(string key, IVrNodeCreator plugin)
        {
            bool result = false;
            if (!_registeredType.ContainsKey(key))
            {
                _registeredType.Add(key, plugin);
                result = true;
            }
            return result;
        }

        public IList<string> GetAllowedSubVrType(IVrType parentType)
        {
            if (parentType == null)
            {
                return (IList<string>)_registeredType.Keys;
            }
            else
            {
                return parentType.GetAllowedSubVrType();
            }
        }

        public bool CreateRootNode(string vrType, string key, string name)
        {
            bool result = false;

            //Find type
            if (_registeredType.ContainsKey(vrType))
            {
                //Launch Node
                IVrType newNode = _registeredType[vrType].CreateVrNode(vrType);

                //TODO INCOMPLETE
                throw new System.NotImplementedException();

                //Init type
                //newNode.in

                //_rootNodes 

                //Register node

            }
            
            return result;
        }

        #endregion
    }
}
