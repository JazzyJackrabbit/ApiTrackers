using ApiTrackers.BDD_Services;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace ApiTrackers.DB_ORM
{
    public class SqlCommand
    {
        internal SqlConfiguration config;
        internal MySqlConnection connection;
        bool isConnectionOpen = false;
        readonly SqlTable table;
        public SqlCommand(SqlConfiguration _config, MySqlConnection _mySqlConnection, SqlTable _table)
        {
            config = _config;
            connection = _mySqlConnection;
            table = _table;
        }
        public SqlSelect Select()
        {
            return new SqlSelect(table, this);
        }
        public SqlInsert Insert()
        {
            return new SqlInsert(table, this);
        }
        public SqlUpdate Update()
        {
            return new SqlUpdate(table, this);
        }
        public SqlDelete Delete()
        {
            return new SqlDelete(table, this);
        }

        public void ConnectClose()
        {
            if (!isConnectionOpen) 
                return;

            isConnectionOpen = false;
            connection.Close();
        }
        public void ConnectOpen()
        {
            if (isConnectionOpen) 
                return;

            isConnectionOpen = true;
            connection.Open();
        }

        public int ExecuteReaderMaxId(string _sqlTableName)
        {
            try
            {
                string sqlIdColumnName = "maxId";
                string commandStr = "SELECT MAX(id) as "+ sqlIdColumnName + " FROM " + _sqlTableName;
                MySqlCommand mysqlcommand = new(commandStr, connection);

                int id = 0;

                using (MySqlDataReader reader = mysqlcommand.ExecuteReader())
                {
                    if (reader.HasRows)
                        if (reader.Read())
                            id = Convert.ToInt32( reader.GetValue(0) );

                    reader.Close();
                }

                return id;
            }
            catch (MySqlException ex)
            {

                Console.WriteLine("BDDService - connection - err: " + ex);
                throw new DatabaseRequestException();
            }
        }
        public List<SqlRow> ExecuteReader(SqlTable _table, string _sqlCommand, List<Tuple<string, Object>> _attributs = null)
        {
            try
            {
                MySqlCommand mysqlcommand = new(_sqlCommand, connection);
                List<SqlRow> sqlRowsResp = new();
                MySqlDataReader reader = mysqlcommand.ExecuteReader();


                if (_attributs != null)
                {
                    foreach (Tuple<string, Object> data in _attributs)
                    {
                        mysqlcommand.Parameters.AddWithValue("@" + data.Item1, data.Item2);
                    }
                    mysqlcommand.Prepare();
                }

                // traitment data rows
                string sqlTableName = _table.name;
                List<SqlAttribut> sqlAttributsModel = _table.attributesModels;

                int posI = 0;
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        SqlRow row = new(_table, true);

                        for (int i = 0; i < reader.FieldCount; i++)
                            for (int j = 0; j < sqlAttributsModel.Count; j++)
                                if (sqlAttributsModel[j].name == reader.GetName(i))
                                {
                                    row.attributs.Add(new SqlAttribut(
                                        sqlAttributsModel[j].name,
                                        reader[i])
                                    );
                                }
                            

                        sqlRowsResp.Add(row);
                        ++posI;
                    }

                reader.Close();

                return sqlRowsResp;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("BDDService - connection - err: " + ex);
                throw new DatabaseRequestException();
            }
        }

        public void ExecuteNonQuery(string _sqlCommand, List<Tuple<string, Object>> _attributs = null)
        {
            try
            {
                MySqlCommand command = new(_sqlCommand, connection)
                {
                    Connection = connection
                };
                if (_attributs != null)
                {
                    foreach (Tuple<string, Object> data in _attributs)
                    {
                        command.Parameters.AddWithValue("@" + data.Item1, data.Item2);
                    }
                    command.Prepare();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("BDDService - connection - err: " + ex);
                throw new DatabaseRequestException();
            }

        }

    }
}
