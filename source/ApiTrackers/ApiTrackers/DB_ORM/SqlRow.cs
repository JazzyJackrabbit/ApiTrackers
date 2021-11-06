using ApiTrackers.DB_Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlRow
    {
        bool selfOpenClose = false;
        SqlTable sqlparentTable;
        public SqlRow(SqlTable _sqlTableParent, bool _selfOpenClose = false)
        {
            selfOpenClose = _selfOpenClose;
            sqlparentTable = _sqlTableParent;
        }
        public SqlRow(SqlTable _sqlTableModelStructure, SqlTable _sqlTableParent, bool _selfOpenClose = false)
        {
            selfOpenClose = _selfOpenClose;
            sqlparentTable = _sqlTableParent;

            foreach (SqlAttribut attrModel in _sqlTableModelStructure.attributesModels)
                attributs.Add(new SqlAttribut(attrModel.name, null));
        }

        public List<SqlAttribut> attributs = new List<SqlAttribut>();

        public SqlAttribut getAttribute(string _attributeName)
        {
            //if (_sqlRow == null) return new SqlAttribut("", null);
            foreach (SqlAttribut sqlAttr in attributs)
                if (sqlAttr.name == _attributeName)
                    if (sqlAttr == null)
                        return new SqlAttribut("", null);
                    else
                        return sqlAttr;
            return new SqlAttribut("", null);
        }
        public bool setAttribute(string _attributeName, object _value)
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

        
    }
}
