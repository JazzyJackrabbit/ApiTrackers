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
            SqlRow row = bdd.select(bdd.sqlTableTrackers, true, _id);
            Tracker tracker = convertSQLToTracker(row);
            return tracker;
        }

        public Tracker insertTracker(Tracker _trackerModel)
        {
            //TODO //TODO //TODO
    

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableTrackers);

            //definition data
            bdd.setAttribute(sqlRowToInsert, "bpm", _trackerModel.trackerContent.BPM);
            bdd.setAttribute(sqlRowToInsert, "artist", _trackerModel.trackerMetadata.artist);
            bdd.setAttribute(sqlRowToInsert, "title", _trackerModel.trackerMetadata.title);
            bdd.setAttribute(sqlRowToInsert, "comments", _trackerModel.trackerMetadata.comments);
            bdd.setAttribute(sqlRowToInsert, "copyrightInformation", _trackerModel.trackerMetadata.copyrightInformation);

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

        public Tracker updateTracker(Tracker _trackerModel, int id)
        {
            SqlRow sqlRowToUpdate = bdd.select(bdd.sqlTableTrackers, true, id);
            if (sqlRowToUpdate == null) return null;

            //definition data
            bdd.setAttribute(sqlRowToUpdate, "bpm", _trackerModel.trackerContent.BPM);
            bdd.setAttribute(sqlRowToUpdate, "artist", _trackerModel.trackerMetadata.artist);
            bdd.setAttribute(sqlRowToUpdate, "title", _trackerModel.trackerMetadata.title);
            bdd.setAttribute(sqlRowToUpdate, "comments", _trackerModel.trackerMetadata.comments);
            bdd.setAttribute(sqlRowToUpdate, "copyrightInformation", _trackerModel.trackerMetadata.copyrightInformation);

            bdd.setAttribute(sqlRowToUpdate, "id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableTrackers, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.select(bdd.sqlTableTrackers, true, id);
            Tracker trackerUpdated = convertSQLToTracker(sqlRowCheck);

            return trackerUpdated;
        }
        public Tracker deleteTracker(int _id, int _idUser)
        {
            SqlRow rowToDelete = bdd.select(bdd.sqlTableTrackers, true, _id);
            Tracker tracker = convertSQLToTracker(rowToDelete);

            if (tracker != null)
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
                Console.WriteLine("BDDService - convertSQLToTracker - err: " + ex);
                return null;
            }
        }

        #endregion
    }
}
