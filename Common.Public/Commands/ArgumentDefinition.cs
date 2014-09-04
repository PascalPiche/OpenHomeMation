using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Commands
{
    public class ArgumentDefinition : IArgumentDefinition
    {
        private string _key;
        private string _name;
        private Type _type;
        private bool _required;

        public ArgumentDefinition(string key, string name, Type t) : this(key, name, t, false) { }

        public ArgumentDefinition(string key, string name, Type t, bool required)
        {
            _key = key;
            _name = name;
            _type = t;
            _required = required;
        }

        public string Key { get { return _key; } }

        public string Name { get { return _name; } }

        public Type Type { get { return _type; } }

        public bool Required { get { return _required; } }

        public bool ValidateValue(object value)
        {
            /*if (value is )
            {
                return true;
            }

            //Try convert
            if (this.Type == typeof(int))
            {
                int result;
                return TryConvertInt(value, out result);
            }*/
            return false;
        }

        

        private bool TryConvertInt(object value, out int result)
        {
            if (value is string)
            {
                return int.TryParse((string)value, out result);
            }
            result = 0;
            return false;
        }


        public IArgumentConverter ArgumentConverter
        {
            get { throw new NotImplementedException(); }
        }
    }
}
