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
        private bool selfOpenClose;

        public SqlInsert(SqlTable _table, SqlCommand _command, bool _selfOpenClose = false)
        {
            table = _table;
            command = _command;
            selfOpenClose = _selfOpenClose;
        }

        public bool insert(SqlRow _row, bool _selfOpenClose)
        {
            try
            {
                command.connectOpen(_selfOpenClose);

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
                command.executeNonQuery(commandString, false);

                command.connectClose(_selfOpenClose);
                return true;
            }
            catch
            {
                command.connectClose(_selfOpenClose);
                throw new DatabaseRequestException();
            }
        }

    }
}
