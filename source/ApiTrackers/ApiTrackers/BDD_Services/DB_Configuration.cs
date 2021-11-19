using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.DB_MainService;

namespace ApiTrackers.BDD_Services
{
    public class DB_Configuration
    {
        internal string db_host = "localhost";
        internal string db_database = "trackers";
        internal string db_user = "TRACKERS";
        internal string db_pass = "TRACKERS";

        internal string defaultIdNameTable = "id";

        public SqlTable sqlTableRightMusics;
        public SqlTable sqlTableSamples;
        public SqlTable sqlTableUsers;
        public SqlTable sqlTableTrackers;
        public SqlTable sqlTableCells;

        public void db_tablesDefinition(List<SqlTable> _sqlTables)
        {
            if (_sqlTables == null) _sqlTables = new List<SqlTable>();
            _sqlTables.Add(sqlTableUsers = new SqlTable("Users", new List<SqlAttribut> {
                new SqlAttribut("id", "id", typeSql.tInt),
                new SqlAttribut("pseudo", "pseudo", typeSql.tVarchar),
                new SqlAttribut("mail", "mail", typeSql.tVarchar),
                new SqlAttribut("passwordHash", "passwordHash", typeSql.tVarchar),
                new SqlAttribut("recoverMails", "recoverMails", typeSql.tInt),
                new SqlAttribut("isEnable", "isEnable", typeSql.tInt),
                new SqlAttribut("adminMode", "adminMode", typeSql.tInt),
            }));

            _sqlTables.Add(sqlTableTrackers = new SqlTable("Trackers", new List<SqlAttribut> {
                new SqlAttribut("id", "id", typeSql.tInt),
                new SqlAttribut("artist", "artist", typeSql.tVarchar),
                new SqlAttribut("title", "title", typeSql.tVarchar),
                new SqlAttribut("copyrightInformation", "copyrightInformation", typeSql.tVarchar),
                new SqlAttribut("comments", "comments", typeSql.tVarchar),
                new SqlAttribut("bpm", "bpm", typeSql.tDouble),
                new SqlAttribut(sqlTableUsers, "idUser", "idUser", typeSql.tInt),
            }));

            _sqlTables.Add(sqlTableSamples = new SqlTable("Samples", new List<SqlAttribut> {
                new SqlAttribut("id","id", typeSql.tInt),
                new SqlAttribut("name","name",typeSql.tVarchar),
                new SqlAttribut("linkSample","linkSample",typeSql.tVarchar),
                new SqlAttribut("idLogo","idLogo",typeSql.tInt),
                new SqlAttribut("color","color",typeSql.tVarchar),
            }));

            _sqlTables.Add(sqlTableCells = new SqlTable("Cells", new List<SqlAttribut> {
                new SqlAttribut("id", "id", typeSql.tInt),
                new SqlAttribut(sqlTableTrackers, "idTracker", "idTracker", typeSql.tInt),
                new SqlAttribut(sqlTableSamples, "idSample", "idSample", typeSql.tInt),
                new SqlAttribut("idPiste","idPiste", typeSql.tInt),
                new SqlAttribut("frequence","frequence", typeSql.tDouble),
                new SqlAttribut("effect","effect", typeSql.tInt),
                new SqlAttribut("volume","volume", typeSql.tDouble),
                new SqlAttribut("positionKey","positionKey", typeSql.tVarchar),
                new SqlAttribut("position","position", typeSql.tDouble),
            }));

            _sqlTables.Add(sqlTableRightMusics = new SqlTable("RightMusics", new List<SqlAttribut> {
                new SqlAttribut("id","id", typeSql.tInt),
                new SqlAttribut(sqlTableUsers, "idUser","idUser",typeSql.tInt),
                new SqlAttribut(sqlTableTrackers, "idTracker","idTracker",typeSql.tInt),
                new SqlAttribut("canEdit","canEdit",typeSql.tInt),
            }));
        }
    }


}
