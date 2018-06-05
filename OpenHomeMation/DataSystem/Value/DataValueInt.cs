using System;

namespace OHM.Data
{
    [Serializable]
    public class DataValueInt : IDataValue
    {
        #region Private Members

        private int _value;

        #endregion

        #region Public Ctor

        public DataValueInt() { }

        public DataValueInt(int value)
        {
            _value = value;
        }

        #endregion

        #region Public Properties

        public int Value
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
