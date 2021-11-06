using ApiTrackers.DB_ORM;
using ApiTrackers.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_Services.ORM
{
    public class SqlTable
    {
        internal List<SqlRow> rows = new List<SqlRow>();
        internal List<SqlAttribut> attributesModels = new List<SqlAttribut>();
        public SqlCommand command;
        public string name;

        public SqlTable(SqlDatabase _db, string _name) { 
            command = new SqlCommand(_db.config, _db.sqlConnection, this);
            name = _name;
        }
        public SqlTable(SqlDatabase _db, string _name, List<SqlAttribut> _attributesModels)
        { 
            attributesModels = _attributesModels;
            command = new SqlCommand(_db.config, _db.sqlConnection, this);
            name = _name;
        }
        public SqlTable(SqlDatabase _db, string _name, List<SqlRow> _rows)
        { 
            rows = _rows;
            command = new SqlCommand(_db.config, _db.sqlConnection, this);
            name = _name;
        }

    }
}
