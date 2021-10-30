using ApiTrackers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.BDD_MainService;

namespace ApiTrackers.Services
{
    public class BDD_TrackerService
    {

        BDD_MainService bdd;
        MainService main;
        private int lastId = -1;

        public BDD_TrackerService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = main.bdd.selectLastID(bdd.sqlTableTrackers);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Tracker> selectTrackers()
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableTrackers, true);
            List<Tracker> trackers = new List<Tracker>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                trackers.Add(convertSQLToTracker(row));
            return trackers;
        }
        public Tracker selectTracker(int _id)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.sqlTableTrackers, true, _id);
            Tracker tracker = convertSQLToTracker(row);
            return tracker;
        }

        public Tracker insertTracker(Tracker _trackerModel)
        {
            
            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableTrackers);

            sqlRowToInsert = convertTrackerToSQL(sqlRowToInsert, _trackerModel);
            bdd.setAttribute(sqlRowToInsert, "id", id);
            bdd.setAttribute(sqlRowToInsert, "idUser", _trackerModel.idUser);

            if (bdd.insert(bdd.sqlTableTrackers, sqlRowToInsert))
            {
                int id2 = getLastId();
                Tracker checkTracker = selectTracker(id2);
                if (checkTracker != null)
                    return checkTracker;
            }
            return null;
        }

        public Tracker updateTracker(Tracker _trackerModel, int id, int _idUser)
        {
            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.sqlTableTrackers, true, id);
            if (sqlRowToUpdate == null) return null;

            sqlRowToUpdate = convertTrackerToSQL(sqlRowToUpdate, _trackerModel);

            Tracker _trackerToUpdate = convertSQLToTracker(sqlRowToUpdate);

            bdd.setAttribute(sqlRowToUpdate, "id", id); 
            bdd.setAttribute(sqlRowToUpdate, "idUser", _trackerToUpdate.idUser);

            if (_trackerToUpdate == null) 
                throw new Exception();

            if (_trackerToUpdate.idUser != _idUser)
                //if ()  //TODO rightMusics users
                throw new ForbiddenException();

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableTrackers, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.sqlTableTrackers, true, id);
            Tracker trackerUpdated = convertSQLToTracker(sqlRowCheck);

            return trackerUpdated;
        }
        public Tracker deleteTracker(int _id, int _idUser)
        {
            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.sqlTableTrackers, true, _id);
            Tracker tracker = convertSQLToTracker(rowToDelete);

            if (tracker == null)
                throw new Exception();

            // ? >> bdd.setAttribute(rowToDelete, "id", _id);
            bdd.setAttribute(rowToDelete, "idUser", tracker.idUser);

            if (tracker.idUser == _idUser)
            {
                bdd.delete(bdd.sqlTableTrackers, _id); //delete now
            }
            else
                throw new ForbiddenException();
            return tracker;
        }

        #endregion

        #region ******** convertions ******** 

        private Tracker convertSQLToTracker(SqlRow _sqlrow)
        {
            try
            {
                Tracker tracker = new Tracker();
                tracker.idTracker = Static.convertToInteger(bdd.getAttribute(_sqlrow, "id").value);

                //test id
                string idTest = Static.convertToString(bdd.getAttribute(_sqlrow, "id").value);
                if (!int.TryParse(idTest, out _)) return null;

                tracker.idUser = Static.convertToInteger(bdd.getAttribute(_sqlrow, "idUser").value);
                tracker.trackerContent.BPM = Static.convertToString(bdd.getAttribute(_sqlrow, "bpm").value);
                tracker.trackerMetadata.artist = Static.convertToString(bdd.getAttribute(_sqlrow, "artist").value);
                tracker.trackerMetadata.title = Static.convertToString(bdd.getAttribute(_sqlrow, "title").value);
                tracker.trackerMetadata.comments = Static.convertToString(bdd.getAttribute(_sqlrow, "comments").value);
                tracker.trackerMetadata.copyrightInformation = Static.convertToString(bdd.getAttribute(_sqlrow, "copyrightInformation").value);
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
            try { 
                //definition data
                bdd.setAttribute(_sqlDest, "bpm", _tracker.trackerContent.BPM);
                bdd.setAttribute(_sqlDest, "artist", _tracker.trackerMetadata.artist);
                bdd.setAttribute(_sqlDest, "title", _tracker.trackerMetadata.title);
                bdd.setAttribute(_sqlDest, "comments", _tracker.trackerMetadata.comments);
                bdd.setAttribute(_sqlDest, "copyrightInformation", _tracker.trackerMetadata.copyrightInformation);
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
