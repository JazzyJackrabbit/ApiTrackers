using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTrackers.BDD_Services;
using ApiTrackers.Services;
using MySql.Data.MySqlClient;

namespace ApiTrackers
{
    public class DB_MainService
    {
        MainService mainService;

        string connectionString;

        List<SqlTable> sqlTables = new List<SqlTable>();

        public DB_Configuration db_config = new DB_Configuration();

        string defaultIdNameTable;

        public DB_MainService(MainService _mainService)
        {
            mainService = _mainService;

            connectionString = 
                "server="+ db_config.db_host + ";"               
                + "database="+ db_config.db_database+";"                                      
                + "uid="+ db_config.db_user + ";"
                + "pwd="+ db_config.db_pass + ";";                       

            defaultIdNameTable = db_config.defaultIdNameTable;
            db_config.db_tablesDefinition(sqlTables);

        }

        public void executeNonQuery(string _sqlCommand)
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(_sqlCommand, sqlConnection);
                command.Connection = sqlConnection;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("BDDService - connection - err: " + ex);
                return;
            }
         
        }
        public List<SqlRow> executeReader(string _sqlCommand, SqlTable _sqlTable)
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(_sqlCommand, sqlConnection);
                command.Connection = sqlConnection;
                sqlConnection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                // traitment data rows
                string sqlTableName = _sqlTable.tableName;
                List<SqlAttribut> sqlAttributsModel = _sqlTable.attributesModels;

                List<SqlRow> sqlRowsResp = new List<SqlRow>();
                int posI = 0;
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        SqlRow row = new SqlRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                            for (int j = 0; j < sqlAttributsModel.Count; j++)
                            {
                                if (sqlAttributsModel[j].name == reader.GetName(i))
                                {
                                    row.attributs.Add(new SqlAttribut(
                                        sqlAttributsModel[j].name,
                                        reader[i])
                                    );
                                }
                            }
                        
                        sqlRowsResp.Add(row);
                        ++posI;
                    }

                sqlConnection.Close();
                return sqlRowsResp;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("BDDService - connection - err: " + ex);
                return null;
            }
        }

        public bool insert(SqlTable _sqlTable, SqlRow _sqlRow)
        {
            try { 
                string sqlTableName = _sqlTable.tableName;
                List<SqlAttribut> sqlAttributsModel = _sqlTable.attributesModels;
            
                string commandString = "INSERT INTO " + sqlTableName + " ";
                string columns = "( ";
                string values = "( ";
                foreach (SqlAttribut sqlAttr in _sqlRow.attributs)
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
                executeNonQuery(commandString);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<SqlRow> select(SqlTable _sqlTable, bool _allColumns)
        {
            return select(_sqlTable, _allColumns, false, 0, "", false, 0, "");
        }
        public SqlRow selectOnlyRow(SqlTable _sqlTable, bool _allColumns, int _id)
        {
            return selectOnlyRow(_sqlTable, _allColumns, _id, defaultIdNameTable);
        }
        public SqlRow selectOnlyRow(SqlTable _sqlTable, bool _allColumns, int _filterAttr, string filterAttr_nametable)
        {
            return selectOnlyRow(_sqlTable, _allColumns, true, _filterAttr, filterAttr_nametable, false, 0, "");
        }
        public SqlRow selectOnlyRow(SqlTable _sqlTable, bool _allColumns, int _filterAttr1, string filterAttr1_nametable, int _filterAttr2, string filterAttr2_nametable)
        {
            return selectOnlyRow(_sqlTable, _allColumns, true, _filterAttr1, filterAttr1_nametable, true, _filterAttr2, filterAttr2_nametable);
        }
        public SqlRow selectOnlyRow(SqlTable _sqlTable, bool _allColumns, bool hasFilter1, int _filterAttr1, string filterAttr1_nametable, bool hasFilter2, int _filterAttr2, string filterAttr2_nametable)
        {
            List<SqlRow> sqlRows = select(_sqlTable, _allColumns, hasFilter1, _filterAttr1, filterAttr1_nametable, hasFilter2, _filterAttr2, filterAttr2_nametable);
            if (sqlRows != null && sqlRows.Count > 0) return sqlRows[0];
            return null;
        }
        public List<SqlRow> select(SqlTable _sqlTable, bool _allColumns, int _filterAttr, string filterAttr_nametable)
        {
            return select(_sqlTable, _allColumns, true, _filterAttr, filterAttr_nametable, false, 0, "");
        }
        public List<SqlRow> select(SqlTable _sqlTable, bool _allColumns, int _filterAttr1, string filterAttr1_nametable, int _filterAttr2, string filterAttr2_nametable)
        {
            return select(_sqlTable, _allColumns, true, _filterAttr1, filterAttr1_nametable, true, _filterAttr2, filterAttr2_nametable);
        }
        public List<SqlRow> select(SqlTable _sqlTable, bool _allColumns, bool _hasFilter1, int _filterAttr1, string filterAttr1_nametable, bool _hasFilter2, int _filterAttr2, string filterAttr2_nametable)
        {
            try
            {
                string sqlTableName = _sqlTable.tableName;
                List<SqlAttribut> sqlAttributsModel = _sqlTable.attributesModels;

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

                List<SqlRow> rows = executeReader(commandString, _sqlTable);

                return rows;
            }
            catch
            {
                return null;
            }
        }

        public bool delete(SqlTable _sqlTable, int _id)
        {
            return delete(_sqlTable, _id, defaultIdNameTable);
        }
        public bool delete(SqlTable _sqlTable, int _whereValue1, string _whereTableName1)
        {
            return delete(_sqlTable, true, _whereValue1, _whereTableName1, false, 0, "");
        }
        public bool delete(SqlTable _sqlTable, int _whereValue1, string _whereTableName1, int _whereValue2, string _whereTableName2)
        {
            return delete(_sqlTable, true, _whereValue1, _whereTableName1, true, _whereValue2, _whereTableName2);
        }
        public bool delete(SqlTable _sqlTable, 
            bool _hasWhereValue1, int _whereValue1, string _whereTableName1, 
            bool _hasWhereValue2, int _whereValue2, string _whereTableName2)
        {
            try
            {
                string sqlTableName = _sqlTable.tableName;

                string commandString = "DELETE FROM " + sqlTableName;

                if (_hasWhereValue1)
                {
                    commandString += " WHERE " + _whereTableName1 + " = " + _whereValue1 + " ";
                    if (_hasWhereValue2)
                        commandString += " AND " + _whereTableName2 + " = " + _whereValue2 + " ";
                }

                executeNonQuery(commandString);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool update(SqlTable _sqlTable, SqlRow _sqlRow, int _id)
        {
            return update(_sqlTable, _sqlRow, _id, defaultIdNameTable);
        }
        public bool update(SqlTable _sqlTable, SqlRow _sqlRow, int _filterAttr1, string _filterAttr1_nametable)
        {
            return update(_sqlTable, _sqlRow, true, _filterAttr1, _filterAttr1_nametable, false, 0, "");
        }

        public bool update(SqlTable _sqlTable, SqlRow _sqlRow, int _filterAttr1, string _filterAttr1_nametable, int _filterAttr2, string _filterAttr2_nametable)
        {
            return update(_sqlTable, _sqlRow, true, _filterAttr1, _filterAttr1_nametable, true, _filterAttr2, _filterAttr2_nametable);
        }
        public bool update(SqlTable _sqlTable, SqlRow _sqlRow, bool _hasfilter1, int _filterAttr1, string _filterAttr1_nametable, bool _hasfilter2, int _filterAttr2, string _filterAttr2_nametable)
        {
            try
            {
                List<SqlAttribut> sqlAttributsModel = _sqlTable.attributesModels;

                string sqlTableName = _sqlTable.tableName;
                string commandString = "UPDATE " + sqlTableName
                    + " SET  ";

                foreach (SqlAttribut sqlAttr in _sqlRow.attributs)
                {

                        string value = "" + sqlAttr.value;
                        if (double.TryParse(value, out var parsedNumber))
                        {
                            value = value.Replace(',', '.');
                        }
                        commandString += ""+sqlAttr.name + " = '" + value + "',";
              
                }
                commandString = Static.removeLastCharacter(commandString);

                if (_hasfilter1)
                {
                    commandString += " WHERE " + _filterAttr1_nametable + " = '" + _filterAttr1 + "'";
                    if(_hasfilter2)
                        commandString += " AND " + _filterAttr2_nametable + " = '" + _filterAttr2 + "'";
                }

                executeNonQuery(commandString);
                return true;
            }
            catch
            {
                return false;
            }
        }

      

        #region ******** classes *********
        public class SqlAttribut
        {
            public string name;
            public object value = null;
            public typeSql typ;
            public string jsonName = null;
            public SqlTable foreignKeyReference = null;

            public SqlAttribut(string _name, object _value)
            { name = _name; value = _value; }
            public SqlAttribut(string _name, typeSql _typ)
            { name = _name; typ = _typ; }
            public SqlAttribut(string _jsonName, string _name, typeSql _typ)
            { jsonName = _jsonName; name = _name; typ = _typ; }
            public SqlAttribut(string _jsonName, string _name, typeSql _typ, object _defaultValue)
            { jsonName = _jsonName; name = _name; typ = _typ; value = _defaultValue; }
            public SqlAttribut(SqlTable _foreignKeyReference, string _jsonName, string _name, typeSql _typ)
            { jsonName = _jsonName; name = _name; typ = _typ; foreignKeyReference = _foreignKeyReference; }
            public SqlAttribut(SqlTable _foreignKeyReference, string _jsonName, string _name, typeSql _typ, object _defaultValue)
            { jsonName = _jsonName; name = _name; typ = _typ; value = _defaultValue; foreignKeyReference = _foreignKeyReference; }



        }

        public class SqlRow
        {
            public SqlRow() { }
            public SqlRow(SqlTable _sqlTableModelStructure) {
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

        public class SqlTable
        {
            public SqlTable() { }
            public SqlTable(string _tableName, List<SqlAttribut> _attributesModels) 
            { tableName = _tableName; attributesModels = _attributesModels; }
            public SqlTable(string _tableName, List<SqlRow> _rows) 
            { tableName = _tableName; rows = _rows; }

            public string tableName;
            public List<SqlRow> rows = new List<SqlRow>();
            public List<SqlAttribut> attributesModels = new List<SqlAttribut>();


            public int selectLastID(MainService _mainService)
            {
                List<SqlRow> sqlRows = _mainService.bdd.select(this, false);
                int id = -1;
                if (sqlRows == null) return id;
                if (sqlRows.Count > 0)
                    foreach (SqlRow sqlRow in sqlRows)
                    {
                        SqlAttribut attrId = sqlRow.getAttribute(_mainService.bdd.defaultIdNameTable);
                        if (attrId != null)
                            id = Math.Max(id, (int)attrId.value);
                    }
                else
                    return 0;
                return id;
            }

            public int selectLastIDPlusOne(MainService _mainService)
            {
                return this.selectLastID(_mainService) + 1;
            }

        }
        public enum typeSql
        {
            tVarchar,
            tDouble,
            tInt,
        }

        #endregion

    }
}
