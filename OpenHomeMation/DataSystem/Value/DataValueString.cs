using System;

namespace OHM.Data
{
    [Serializable]
    public class DataValueString : IDataValue
    {

        private string _value;

        public DataValueString() { }

        public DataValueString(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Type Type
        {
            get { return this.GetType(); }
        }
    }
}
