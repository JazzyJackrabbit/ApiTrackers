﻿using ApiTrackers.BDD_Services;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DB_ORM
{
    public class SqlCommand
    {
        internal SqlConfiguration config;
        internal MySqlConnection connection;
        bool isConnectionOpen = false;

        SqlTable table;
        public SqlCommand(SqlConfiguration _config, MySqlConnection _mySqlConnection, SqlTable _table)
        {
            config = _config;
            connection = _mySqlConnection;
            table = _table;
        }
        public SqlSelect select()
        {
            return new SqlSelect(table, this);
        }
        public SqlInsert insert()
        {
            return new SqlInsert(table, this);
        }
        public SqlUpdate update()
        {
            return new SqlUpdate(table, this);
        }
        public SqlDelete delete()
        {
            return new SqlDelete(table, this);
        }

        public void connectClose(bool _selfOpenClose)
        {
            if (!isConnectionOpen) 
                return;

            if (_selfOpenClose)
            {
                isConnectionOpen = false;
                connection.Close();
            }
        }
        public void connectOpen(bool _selfOpenClose)
        {
            if (isConnectionOpen) 
                return;

            if (_selfOpenClose)
            {
                isConnectionOpen = true;
                connection.Open();
            }
        }

        public List<SqlRow> executeReader(SqlTable _table, string _sqlCommand, bool _selfOpenClose)
        {
            try
            {
                MySqlCommand mysqlcommand = new MySqlCommand(_sqlCommand, connection);

                if (_selfOpenClose) 
                    connection.Open();

                MySqlDataReader reader = mysqlcommand.ExecuteReader();

                // traitment data rows
                string sqlTableName = _table.name;
                List<SqlAttribut> sqlAttributsModel = _table.attributesModels;

                List<SqlRow> sqlRowsResp = new List<SqlRow>();
                int posI = 0;
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        SqlRow row = new SqlRow(_table);

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

                if (_selfOpenClose) 
                    connection.Close();

                return sqlRowsResp;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("BDDService - connection - err: " + ex);
                throw new DatabaseRequestException();
            }
        }

        public void executeNonQuery(string _sqlCommand, bool _selfOpenClose)
        {
            try
            {
                MySqlCommand mysqlcommand = new MySqlCommand(_sqlCommand, connection);

                if (_selfOpenClose) 
                    connection.Open();

                mysqlcommand.ExecuteNonQuery();

                if (_selfOpenClose) 
                    connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("BDDService - connection - err: " + ex);
                throw new DatabaseRequestException();
            }

        }

    }
}
