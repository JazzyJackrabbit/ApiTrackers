using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTrackers.BDD_Services;
using ApiTrackers.DB_ORM;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using ApiTrackers.Services;
using MySql.Data.MySqlClient;

namespace ApiTrackers
{
    public class SqlDatabase
    {
        Main mainService;

        internal SqlConfiguration config;
        internal MySqlConnection sqlConnection;

        public SqlTable tableRightMusics;
        public SqlTable tableSamples;
        public SqlTable tableSamplesAlias;
        public SqlTable tableUsers;
        public SqlTable tableTrackers;
        public SqlTable tableCells;

        public SqlDatabase(Main _mainService)
        {
            mainService = _mainService;
            config = new SqlConfiguration();

            string connectionString = 
                "server="+ config.db_host + ";"               
                + "database="+ config.db_database+";"                                      
                + "uid="+ config.db_user + ";"
                + "pwd="+ config.db_pass + ";";

            sqlConnection = new MySqlConnection(connectionString);

            config.db_tablesDefinition(this);
        }
        public MySqlConnection getSqlConnection()
        {
            return sqlConnection;
        }
        public void connectClose(bool _selfOpenClose)
        {
            if (_selfOpenClose)
                sqlConnection.Close();
        }
        public void connectOpen(bool _selfOpenClose)
        {
            if (_selfOpenClose)
                sqlConnection.Open();
        }


    }
}
