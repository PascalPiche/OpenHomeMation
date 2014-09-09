using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Data
{
    [Serializable]
    public class DataValueBool : IDataValue
    {

        private bool _value;

        public DataValueBool() { }

        public DataValueBool(bool value)
        {
            _value = value;
        }

        public bool Value
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
