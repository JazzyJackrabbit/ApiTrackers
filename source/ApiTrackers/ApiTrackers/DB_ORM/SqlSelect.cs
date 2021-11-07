using ApiTrackers.BDD_Services;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlSelect
    {
        private SqlTable table;
        private SqlCommand command;
        public SqlSelect(SqlTable _table, SqlCommand _command)
        {
            table = _table;
            command = _command;
        }

        public List<SqlRow> all(bool _allColumns, int _filterAttr, string filterAttr_nametable, bool _selfOpenClose)
        {
            return all(_allColumns, true, _filterAttr, filterAttr_nametable, false, 0, "", _selfOpenClose);
        }
        public List<SqlRow> all(bool _allColumns, bool _selfOpenClose)
        {
            return all(_allColumns, false, 0, "", false, 0, "", _selfOpenClose);
        }
        public List<SqlRow> all(bool _selfOpenClose)
        {
            return all(false, false, 0, "", false, 0, "", _selfOpenClose);
        }
        public List<SqlRow> all(bool _allColumns, int _filterAttr1, string filterAttr1_nametable, int _filterAttr2, string filterAttr2_nametable, bool _selfOpenClose)
        {
            return all(_allColumns, true, _filterAttr1, filterAttr1_nametable, true, _filterAttr2, filterAttr2_nametable, _selfOpenClose);
        }
        public List<SqlRow> all(bool _allColumns, bool _hasFilter1, int _filterAttr1, string filterAttr1_nametable, bool _hasFilter2, int _filterAttr2, string filterAttr2_nametable, bool _selfOpenClose)
        {
            try
            {
                command.connectOpen(_selfOpenClose);

                string sqlTableName = table.name;
                List<SqlAttribut> sqlAttributsModel = table.attributesModels;

                string columns = " ";

                if (_allColumns)
                    columns += "*";
                else
                {
                    foreach (SqlAttribut sqlAttr in sqlAttributsModel)
                    {
                        columns += "`" + sqlAttr.name + "`,";
                    }
                    columns = Static.removeLastCharacter(columns);
                }
                columns += " ";
                string commandString = "SELECT " + columns + " FROM " + sqlTableName + " ";
                if (_hasFilter1)
                {
                    commandString += "WHERE " + filterAttr1_nametable + " = " + _filterAttr1 + " ";
                    if (_hasFilter2)
                        commandString += " AND " + filterAttr2_nametable + " = " + _filterAttr2 + " ";
                }

                List<SqlRow> rows = command.executeReader(table, commandString, false);

                command.connectClose(_selfOpenClose);
                return rows;
            }
            catch
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
        }

        public SqlRow row(bool _allColumns, int _id, bool _selfOpenClose)
        {
            return row(_allColumns, _id, command.config.defaultIdNameTable, _selfOpenClose);
        }
        public SqlRow row(bool _allColumns, int _filterAttr, string filterAttr_nametable, bool _selfOpenClose)
        {
            return row(_allColumns, true, _filterAttr, filterAttr_nametable, false, 0, "", _selfOpenClose);
        }
        public SqlRow row(bool _allColumns, int _filterAttr1, string filterAttr1_nametable, int _filterAttr2, string filterAttr2_nametable, bool _selfOpenClose)
        {
            return row(_allColumns, true, _filterAttr1, filterAttr1_nametable, true, _filterAttr2, filterAttr2_nametable, _selfOpenClose);
        }
        public SqlRow row(bool _allColumns, bool hasFilter1, int _filterAttr1, string filterAttr1_nametable, bool hasFilter2, int _filterAttr2, string filterAttr2_nametable, bool _selfOpenClose)
        {
            SqlSelect sqlSelect = new SqlSelect(table, command);
            List<SqlRow> sqlRows = sqlSelect.all(_allColumns, hasFilter1, _filterAttr1, filterAttr1_nametable, hasFilter2, _filterAttr2, filterAttr2_nametable, _selfOpenClose);
            if (sqlRows != null && sqlRows.Count > 0) return sqlRows[0];
            return null;
        }

        public int lastId(bool _selfOpenClose)
        {
            try
            {
                command.connectOpen(_selfOpenClose);

                int lastId = command.executeReaderMaxId(table.name, false);

                command.connectClose(_selfOpenClose);

                Console.WriteLine("Max Id = " + lastId + " .. " + table.name);

                return lastId;
            }
            catch (MySqlException ex)
            {
                command.connectClose(_selfOpenClose);
                Console.WriteLine("BDDService - connection - err: " + ex);
                throw new DatabaseRequestException();
            }
        }

    }
}
