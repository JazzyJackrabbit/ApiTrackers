using ApiTrackers.DB_ORM;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.SqlDatabase;

namespace ApiTrackers.Services
{
    public class DB_TrackerService
    {

        SqlDatabase bdd;
        Main main;
        private int lastId = -1;
        SqlTable table;
        SqlCommand command;

        public DB_TrackerService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableTrackers;
            command = table.command;
            lastId = command.select().lastId(true);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Tracker> selectTrackers(bool _autoOpenClose)
        {
            command.connectOpen(_autoOpenClose);

            List<SqlRow> rows = command.select().all(false);
            List<Tracker> trackers = new List<Tracker>();
            if (rows == null)
            {
                command.connectClose(_autoOpenClose);
                return null;
            }
            foreach (SqlRow row in rows)
                trackers.Add(convertSQLToTracker(row, false));

            command.connectClose(_autoOpenClose);
            return trackers;
        }
        public Tracker selectTracker(int _id, bool _autoOpenClose)
        {
            command.connectOpen(_autoOpenClose);

            SqlRow row = command.select().row(true, _id, false);
            Tracker tracker = convertSQLToTracker(row, false);

            command.connectClose(_autoOpenClose);

            return tracker;
        }

        public Tracker insertTracker(Tracker _trackerModel, bool _autoOpenClose)
        {
            command.connectOpen(_autoOpenClose);

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.tableTrackers, false);

            sqlRowToInsert = convertTrackerToSQL(sqlRowToInsert, _trackerModel);
            sqlRowToInsert.setAttribute("id", id);
            sqlRowToInsert.setAttribute("idUser", _trackerModel.idUser);

            if (command.insert().insert(sqlRowToInsert, false))
            {
                int id2 = getLastId();
                Tracker checkTracker = selectTracker(id2, false);
                if (checkTracker != null)
                {
                    command.connectClose(_autoOpenClose);
                    return checkTracker;
                }
            }
            command.connectClose(_autoOpenClose);
            return null;
        }

        public Tracker updateTracker(Tracker _trackerModel, int id, int _idUser, bool _autoOpenClose)
        {

            command.connectOpen(_autoOpenClose);

            SqlRow sqlRowToUpdate = command.select().row(true, id, false);
            if (sqlRowToUpdate == null)
            {
                command.connectClose(_autoOpenClose); 
                return null;
            }

            sqlRowToUpdate = convertTrackerToSQL(sqlRowToUpdate, _trackerModel);

            Tracker _trackerToUpdate = convertSQLToTracker(sqlRowToUpdate, false);

            sqlRowToUpdate.setAttribute("id", id);
            sqlRowToUpdate.setAttribute("idUser", _trackerToUpdate.idUser);

            if (_trackerToUpdate == null) 
                throw new Exception();

            if (_trackerToUpdate.idUser != _idUser)
                //if ()  //TODO rightMusics users
                throw new ForbiddenException();

            bool checkUpdateCorrectly = command.update().update(sqlRowToUpdate, id, false);
            if (!checkUpdateCorrectly)
            {
                command.connectClose(_autoOpenClose);
                return null;
            }

            SqlRow sqlRowCheck = command.select().row(true, id, false);
            Tracker trackerUpdated = convertSQLToTracker(sqlRowCheck, false);

            command.connectClose(_autoOpenClose);
            return trackerUpdated;
        }
        public Tracker deleteTracker(int _id, int _idUser, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);

            SqlRow rowToDelete = command.select().row(true, _id, false);
            Tracker tracker = convertSQLToTracker(rowToDelete, false);

            if (tracker == null)
            {
                command.connectClose(_selfOpenClose);
                throw new Exception();
            }

            // ? >> bdd.setAttribute(rowToDelete, "id", _id);
            rowToDelete.setAttribute("idUser", tracker.idUser);

            if (tracker.idUser == _idUser)
            {
                command.delete().delete(_id, false); //delete now
                command.connectClose(_selfOpenClose);
            }
            else
                throw new ForbiddenException();

            command.connectClose(_selfOpenClose);
            return tracker;
        }

        #endregion

        #region ******** convertions ******** 

        private Tracker convertSQLToTracker(SqlRow _sqlrow, bool _selfOpenClose)
        {
            if (_sqlrow == null) return null;
            try
            {
                Tracker tracker = new Tracker();
                tracker.idTracker = Static.convertToInteger(_sqlrow.getAttribute("id").value);

                //test id
                string idTest = Static.convertToString(_sqlrow.getAttribute("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                tracker.idUser = Static.convertToInteger(_sqlrow.getAttribute("idUser").value);
                tracker.trackerContent.BPM = Static.convertToString(_sqlrow.getAttribute("bpm").value);
                tracker.trackerMetadata.artist = Static.convertToString(_sqlrow.getAttribute("artist").value);
                tracker.trackerMetadata.title = Static.convertToString(_sqlrow.getAttribute("title").value);
                tracker.trackerMetadata.comments = Static.convertToString(_sqlrow.getAttribute("comments").value);
                tracker.trackerMetadata.copyrightInformation = Static.convertToString(_sqlrow.getAttribute("copyrightInformation").value);

                // get pistes
                List<Piste> pistes = new List<Piste>();
                List<Note> notes = main.bddCells.selectCells(tracker.idTracker, _selfOpenClose);
                tracker.trackerContent.pistes[0].notes = notes;

                return tracker;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertTrackerToSQL(SqlRow _sqlDest, Tracker _tracker)
        {
            try
            {
                if (_sqlDest == null) return null;
                if (_tracker == null) return null;

                _sqlDest.setAttribute("bpm", _tracker.trackerContent.BPM);
                _sqlDest.setAttribute("artist", _tracker.trackerMetadata.artist);
                _sqlDest.setAttribute("title", _tracker.trackerMetadata.title);
                _sqlDest.setAttribute("comments", _tracker.trackerMetadata.comments);
                _sqlDest.setAttribute("copyrightInformation", _tracker.trackerMetadata.copyrightInformation);
            }
            catch (Exception ex)
            {
                Console.WriteLine("convert<<Object>>ToSQL - err: " + ex);
                throw new OwnException();
            }
            return _sqlDest;
        }
        #endregion

    }
}
