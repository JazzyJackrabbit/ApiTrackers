using ApiTrackers.BDD_Services;
using ApiTrackers.DB_Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlUpdate
    {
        private SqlTable table;
        private SqlCommand command;
        private bool selfOpenClose;
        public SqlUpdate(SqlTable _table, SqlCommand _command, bool _selfOpenClose = false)
        {
            table = _table;
            command = _command;
            selfOpenClose = _selfOpenClose;
        }

        public bool update(SqlRow _sqlRow, int _id, bool _selfOpenClose)
        {
            return update(_sqlRow, _id, command.config.defaultIdNameTable, _selfOpenClose);
        }
        public bool update(SqlRow _sqlRow, int _filterAttr1, string _filterAttr1_nametable, bool _selfOpenClose)
        {
            return update(_sqlRow, true, _filterAttr1, _filterAttr1_nametable, false, 0, "", _selfOpenClose);
        }

        public bool update(SqlRow _sqlRow, int _filterAttr1, string _filterAttr1_nametable, int _filterAttr2, string _filterAttr2_nametable, bool _selfOpenClose)
        {
            return update(_sqlRow, true, _filterAttr1, _filterAttr1_nametable, true, _filterAttr2, _filterAttr2_nametable, _selfOpenClose);
        }
        public bool update(SqlRow _sqlRow, bool _hasfilter1, int _filterAttr1, string _filterAttr1_nametable, bool _hasfilter2, int _filterAttr2, string _filterAttr2_nametable, bool _selfOpenClose)
        {
            try
            {
                command.connectOpen(_selfOpenClose);

                List<SqlAttribut> sqlAttributsModel = table.attributesModels;

                string sqlTableName = table.name;
                string commandString = "UPDATE " + sqlTableName
                    + " SET  ";

                foreach (SqlAttribut sqlAttr in _sqlRow.attributs)
                {

                    string value = "" + sqlAttr.value;
                    if (double.TryParse(value, out var parsedNumber))
                    {
                        value = value.Replace(',', '.');
                    }
                    commandString += "" + sqlAttr.name + " = '" + value + "',";

                }
                commandString = Static.removeLastCharacter(commandString);

                if (_hasfilter1)
                {
                    commandString += " WHERE " + _filterAttr1_nametable + " = '" + _filterAttr1 + "'";
                    if (_hasfilter2)
                        commandString += " AND " + _filterAttr2_nametable + " = '" + _filterAttr2 + "'";
                }

                command.executeNonQuery(commandString, false);


                command.connectClose(_selfOpenClose);

                return true;
            }
            catch
            {

                command.connectClose(_selfOpenClose);
                return false;
            }
        }

    }
}
