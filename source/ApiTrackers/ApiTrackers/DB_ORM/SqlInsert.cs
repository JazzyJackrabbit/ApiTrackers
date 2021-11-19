using ApiTrackers.BDD_Services;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlInsert
    {
        private SqlTable table;
        private SqlCommand command;

        public SqlInsert(SqlTable _table, SqlCommand _command)
        {
            table = _table;
            command = _command;
        }

        public bool insert(SqlRow _row)
        {
            try
            {

                List<SqlAttribut> sqlAttributsModel = table.attributesModels;

                List<Tuple<string, Object>> attributs = new();

                string commandString = "INSERT INTO " + table.name;
                string columns = "( ";
                string values = "( ";
                /*foreach (SqlAttribut sqlAttr in _sqlRow.attributs)*/
                foreach (SqlAttribut sqlAttr in _row.attributs)
                {

                    string value = "@" + sqlAttr.name;
                    Utils.AddIfNotPresent(attributs, new Tuple<string, Object>(sqlAttr.name, sqlAttr.value));

                    columns += "`" + sqlAttr.name + "`,";
                    values += value + ",";

                }
                columns = ObjectUtils.removeLastCharacter(columns);
                values = ObjectUtils.removeLastCharacter(values);
                columns += ") VALUES ";
                values += ") ";

                commandString = commandString + columns + values;
                command.ExecuteNonQuery(commandString, attributs);

                return true;
            }
            catch (Exception ex)
            {
                throw new DatabaseRequestException(ex.Message);
            }
        }

    }
}
