using System;

namespace OHM.Data
{
    public class DataValueDictionary : IDataValue
    {

        private IDataDictionary _data;

        public DataValueDictionary() { }

        public DataValueDictionary(IDataDictionary data)
        {
            _data = data;
        }

        public IDataDictionary Value
        {
            get { return _data; }
            set { _data = value; }
        }

        public Type Type
        {
            get { return this.GetType(); }
        }
    }
}
