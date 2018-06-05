using System;

namespace OHM.Data
{
    [Serializable]
    public class DataValueBool : IDataValue
    {
        #region Private Members

        private bool _value;

        #endregion

        #region Public Ctor

        public DataValueBool() { }

        public DataValueBool(bool value)
        {
            _value = value;
        }

        #endregion

        #region Public Properties

        public bool Value
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
