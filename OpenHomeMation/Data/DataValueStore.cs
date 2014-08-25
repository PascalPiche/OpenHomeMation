using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Data
{
    public class DataValueStore : IDataValue
    {

        private IDataStore _data;

        public DataValueStore() { }

        public DataValueStore(IDataStore data)
        {
            _data = data;
        }

        public IDataStore Value
        {
            get { return _data; }
            set { _data = value; }
        }

        object IDataValue.Value
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
