using System;

namespace OHM.Data
{
    [Serializable]
    public class DataValueString : IDataValue
    {
        #region Private Members

        private string _value;

        #endregion 

        #region Public Ctor

        public DataValueString() { }

        public DataValueString(string value)
        {
            _value = value;
        }

        #endregion

        #region Public Properties

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Type Type
        {
            get { return this.GetType(); }
        }

        #endregion
    }
}
