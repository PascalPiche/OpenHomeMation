using System;

namespace OHM.Data
{
    [Serializable]
    public class DataValueInt : IDataValue
    {

        private int _value;

        public DataValueInt() { }

        public DataValueInt(int value)
        {
            _value = value;
        }

        public int Value
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
