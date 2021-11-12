using ApiTrackers.BDD_Services;
using ApiTrackers.DB_Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlDelete
    {
        private SqlTable table;
        private SqlCommand command;

        public SqlDelete(SqlTable _table, SqlCommand _command)
        {
            table = _table;
            command = _command;
        }

        public bool delete(int _id)
        {
            return delete(_id, command.config.defaultIdNameTable);
        }
        public bool delete(int _whereValue1, string _whereTableName1)
        {
            return delete(true, _whereValue1, _whereTableName1, false, 0, "");
        }
        public bool delete(int _whereValue1, string _whereTableName1, int _whereValue2, string _whereTableName2)
        {
            return delete(true, _whereValue1, _whereTableName1, true, _whereValue2, _whereTableName2);
        }
        public bool delete(
            bool _hasWhereValue1, int _whereValue1, string _whereTableName1,
            bool _hasWhereValue2, int _whereValue2, string _whereTableName2)
        {

            try
            {
                string commandString = "DELETE FROM " + table.name;

                if (_hasWhereValue1)
                {
                    commandString += " WHERE " + _whereTableName1 + " = " + _whereValue1 + " ";
                    if (_hasWhereValue2)
                        commandString += " AND " + _whereTableName2 + " = " + _whereValue2 + " ";
                }

                command.executeNonQuery(commandString);

                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
