using ApiTrackers.DB_ORM;
using ApiTrackers.DB_Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.SqlDatabase;

namespace ApiTrackers.BDD_Services
{
    public class SqlConfiguration
    {
        internal string db_host = "localhost";
        internal string db_database = "trackers";
        internal string db_user = "TRACKERS";
        internal string db_pass = "TRACKERS";

        internal string defaultIdNameTable = "id";

        public void db_tablesDefinition(SqlDatabase _db)
        {
            _db.tableUsers = new SqlTable(_db, "Users", new List<SqlAttribut> {
                new SqlAttribut("id", "id", typeSql.tInt),
                new SqlAttribut("pseudo", "pseudo", typeSql.tVarchar),
                new SqlAttribut("mail", "mail", typeSql.tVarchar),
                new SqlAttribut("passwordHash", "passwordHash", typeSql.tVarchar),
                new SqlAttribut("recoverMails", "recoverMails", typeSql.tInt),
                new SqlAttribut("isEnable", "isEnable", typeSql.tInt),
                new SqlAttribut("adminMode", "adminMode", typeSql.tInt),
            });


            _db.tableTrackers = new SqlTable(_db, "Trackers", new List<SqlAttribut> {
                new SqlAttribut("id", "id", typeSql.tInt),
                new SqlAttribut("artist", "artist", typeSql.tVarchar),
                new SqlAttribut("title", "title", typeSql.tVarchar),
                new SqlAttribut("copyrightInformation", "copyrightInformation", typeSql.tVarchar),
                new SqlAttribut("comments", "comments", typeSql.tVarchar),
                new SqlAttribut("bpm", "bpm", typeSql.tDouble),
                new SqlAttribut("idUser", "idUser", typeSql.tInt),
            });

            _db.tableSamples = new SqlTable(_db, "Samples", new List<SqlAttribut> {
                new SqlAttribut("id","id", typeSql.tInt),
                new SqlAttribut("name","name",typeSql.tVarchar),
                new SqlAttribut("linkSample","linkSample",typeSql.tVarchar),
                new SqlAttribut("idLogo","idLogo",typeSql.tInt),
                new SqlAttribut("color","color",typeSql.tVarchar),
            });

            _db.tableSamplesAlias = new SqlTable(_db, "SamplesAlias", new List<SqlAttribut> {
                new SqlAttribut("id","id", typeSql.tInt),
                new SqlAttribut("name","name",typeSql.tVarchar),
                new SqlAttribut("idUser","idUser",typeSql.tInt),
                new SqlAttribut("idSample","idSample",typeSql.tInt),
                new SqlAttribut("idLogo","idLogo",typeSql.tInt),
                new SqlAttribut("color","color",typeSql.tVarchar),
            });

            _db.tableCells = new SqlTable(_db, "Cells", new List<SqlAttribut> {
                new SqlAttribut("id", "id", typeSql.tInt),
                new SqlAttribut("idTracker", "idTracker", typeSql.tInt),
                new SqlAttribut("idSample", "idSample", typeSql.tInt),
                new SqlAttribut("idPiste","idPiste", typeSql.tInt),
                new SqlAttribut("frequence","frequence", typeSql.tDouble),
                new SqlAttribut("effect","effect", typeSql.tInt),
                new SqlAttribut("volume","volume", typeSql.tDouble),
                new SqlAttribut("positionKey","positionKey", typeSql.tVarchar),
                new SqlAttribut("position","position", typeSql.tDouble),
            });

            _db.tableRightMusics = new SqlTable(_db, "RightMusics", new List<SqlAttribut> {
                new SqlAttribut("id","id", typeSql.tInt),
                new SqlAttribut("idUser","idUser",typeSql.tInt),
                new SqlAttribut("idTracker","idTracker",typeSql.tInt),
                new SqlAttribut("canEdit","canEdit",typeSql.tInt),
            });
        }
    }


}
