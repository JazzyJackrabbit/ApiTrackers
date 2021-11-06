using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlAttribut
    {
        public string name;
        public object value = null;
        public typeSql typ;
        public string jsonName = null;

        public SqlAttribut(string _name, object _value)
        { name = _name; value = _value; }
        public SqlAttribut(string _name, typeSql _typ)
        { name = _name; typ = _typ; }
        public SqlAttribut(string _jsonName, string _name, typeSql _typ)
        { jsonName = _jsonName; name = _name; typ = _typ; }
        public SqlAttribut(string _jsonName, string _name, typeSql _typ, object _defaultValue)
        { jsonName = _jsonName; name = _name; typ = _typ; value = _defaultValue; }
    }

}
