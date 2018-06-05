using System;

namespace OHM.Data
{
    public class DataValueDictionary : IDataValue
    {
        #region Private Members

        private IDataDictionary _data;

        #endregion

        #region Public Ctor

        public DataValueDictionary() { }

        public DataValueDictionary(IDataDictionary data)
        {
            _data = data;
        }

        #endregion

        #region Public Properties

        public IDataDictionary Value
        {
            get { return _data; }
            set { _data = value; }
        }

        public Type Type
        {
            get { return this.GetType(); }
        }

        #endregion
    }
}
