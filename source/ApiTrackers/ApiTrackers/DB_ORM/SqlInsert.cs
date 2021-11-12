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

                string commandString = "INSERT INTO " + table.name + " ";
                string columns = "( ";
                string values = "( ";
                foreach (SqlAttribut sqlAttr in _row.attributs)
                {
                    string value = "" + sqlAttr.value;
                    if (double.TryParse(value, out var parsedNumber))
                    {
                        value = value.Replace(',', '.');
                    }

                    columns += "`" + sqlAttr.name + "`,";
                    values += " '" + value + "',";
                }
                columns = Static.removeLastCharacter(columns);
                values = Static.removeLastCharacter(values);
                columns += ") VALUES ";
                values += ") ";

                commandString = commandString + columns + values;
                command.executeNonQuery(commandString);

                return true;
            }
            catch (Exception ex)
            {
                throw new DatabaseRequestException();
            }
        }

    }
}
