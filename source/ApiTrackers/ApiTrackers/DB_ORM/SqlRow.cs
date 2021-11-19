using ApiTrackers.DB_Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlRow
    {
        SqlTable sqlparentTable;
        public List<SqlAttribut> attributs = new List<SqlAttribut>();

        public SqlRow(SqlTable _sqlTableParent, bool _newEmptyRow = false)
        {
            sqlparentTable = _sqlTableParent;

            if(!_newEmptyRow)
                foreach (SqlAttribut attrModel in _sqlTableParent.attributesModels)
                    attributs.Add(new SqlAttribut(attrModel.name, null));
        }

        public SqlAttribut GetAttribut(string _attributeName)
        {
            //if (_sqlRow == null) return new SqlAttribut("", null);
            foreach (SqlAttribut sqlAttr in attributs)
                if (sqlAttr.name == _attributeName)
                    return sqlAttr;
            return new SqlAttribut("", null);
        }
        public bool SetAttribut(string _attributeName, object _value)
        {
            //if (_sqlRow == null) return false;
            foreach (SqlAttribut sqlAttr in attributs)
                if (sqlAttr.name == _attributeName)
                {
                    sqlAttr.value = _value;
                    return true;
                }
            return false;
        }
        public void AddAttribut(String _key, Object _value)
        {
            attributs.Add(new SqlAttribut(_key, _value));
        }

    }
}
