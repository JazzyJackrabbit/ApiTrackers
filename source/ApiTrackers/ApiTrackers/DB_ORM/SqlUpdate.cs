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
        public SqlUpdate(SqlTable _table, SqlCommand _command)
        {
            table = _table;
            command = _command;
        }

        public bool update(SqlRow _sqlRow, int _id )
        {
            return update(_sqlRow, _id, command.config.defaultIdNameTable);
        }
        public bool update(SqlRow _sqlRow, int _filterAttr1, string _filterAttr1_nametable )
        {
            return update(_sqlRow, true, _filterAttr1, _filterAttr1_nametable, false, 0, "");
        }

        public bool update(SqlRow _sqlRow, int _filterAttr1, string _filterAttr1_nametable, int _filterAttr2, string _filterAttr2_nametable )
        {
            return update(_sqlRow, true, _filterAttr1, _filterAttr1_nametable, true, _filterAttr2, _filterAttr2_nametable);
        }
        public bool update(SqlRow _sqlRow, bool _hasfilter1, int _filterAttr1, string _filterAttr1_nametable, bool _hasfilter2, int _filterAttr2, string _filterAttr2_nametable )
        {
            try
            {
                List<SqlAttribut> sqlAttributsModel = table.attributesModels;
                List<Tuple<string, Object>> attributs = new();

                string sqlTableName = table.name;
                string commandString = "UPDATE " + sqlTableName
                    + " SET  ";

                foreach (SqlAttribut sqlAttr in _sqlRow.attributs)
                {
                    if (sqlAttr.name.Equals("id"))
                        continue;
                    attributs.Add(new Tuple<string, Object>(sqlAttr.name, sqlAttr.value));

                    commandString += "" + sqlAttr.name + " = @" + sqlAttr.name + ",";

                }
                commandString = Static.removeLastCharacter(commandString);

                if (_hasfilter1)
                {
                    Utils.AddIfNotPresent(attributs, new Tuple<string, Object>(_filterAttr1_nametable, _filterAttr1));
                    commandString += " WHERE " + _filterAttr1_nametable + " = @" + _filterAttr1_nametable;
                    if (_hasfilter2)
                    {
                        Utils.AddIfNotPresent(attributs, new Tuple<string, Object>(_filterAttr2_nametable, _filterAttr2));
                        commandString += " AND " + _filterAttr2_nametable + " = @" + _filterAttr2_nametable;
                    }
                }

                command.ExecuteNonQuery(commandString, attributs);


                return true;
            }
            catch
            {

                return false;
            }
        }

    }
}
