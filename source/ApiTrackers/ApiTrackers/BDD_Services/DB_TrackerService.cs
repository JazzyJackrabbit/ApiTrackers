using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.DB_MainService;

namespace ApiTrackers.Services
{
    public class DB_TrackerService
    {

        DB_MainService bdd;
        MainService main;

        public DB_TrackerService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

        }

        public int getLastId() { return bdd.db_config.sqlTableTrackers.selectLastID(main); }
        public int getNextId() { return bdd.db_config.sqlTableTrackers.selectLastID(main) + 1; }

        #region ******** public methods ********

        public List<Tracker> selectTrackers()
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableTrackers, true);
            List<Tracker> trackers = new List<Tracker>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                trackers.Add(convertSQLToTracker(row));
            return trackers;
        }
        public Tracker selectTracker(int _id)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableTrackers, true, _id);
            Tracker tracker = convertSQLToTracker(row);
            return tracker;
        }

        public Tracker insertTracker(Tracker _trackerModel)
        {
            
            int id = getNextId();

            SqlRow sqlRowToInsert = convertTrackerToSQL(_trackerModel);
            sqlRowToInsert.SetAttribut("id", id);

            if (bdd.insert(bdd.db_config.sqlTableTrackers, sqlRowToInsert))
            {
                int id2 = getLastId();
                Tracker checkTracker = selectTracker(id2);
                if (checkTracker != null)
                    return checkTracker;
            }
            return null;
        }

        public Tracker updateTracker(Tracker _trackerModel, int _idUser)
        {
            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableTrackers, true, _trackerModel.idTracker);
            if (sqlRowToUpdate == null) return null; //Check the existance 

            sqlRowToUpdate = convertTrackerToSQL(_trackerModel);

            if (_trackerModel.idUser != _idUser)
                //if ()  //TODO rightMusics users
                throw new ForbiddenException();

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableTrackers, sqlRowToUpdate, _trackerModel.idTracker);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableTrackers, true, _trackerModel.idTracker);
            Tracker trackerUpdated = convertSQLToTracker(sqlRowCheck);

            return trackerUpdated;
        }
        public Tracker deleteTracker(int _id, int _idUser)
        {
            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.db_config.sqlTableTrackers, true, _id);
            Tracker tracker = convertSQLToTracker(rowToDelete);

            if (tracker == null)
                throw new Exception();

            // ? >> bdd.setAttribute(rowToDelete, "id", _id);

            if (tracker.idUser == _idUser)
            {
                bdd.delete(bdd.db_config.sqlTableTrackers, _id); //delete now
            }
            else
                throw new ForbiddenException();
            return tracker;
        }

        #endregion

        #region ******** convertions ******** 

        private Tracker convertSQLToTracker(SqlRow _sqlrow)
        {
            if (_sqlrow == null) return null;
            try
            {
                Tracker tracker = new Tracker();
                tracker.idTracker = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);
                tracker.idUser = Utils.ConvertToInteger(_sqlrow.getAttribute("idUser").value);
                tracker.trackerContent.BPM = Utils.ConvertToInteger(_sqlrow.getAttribute("bpm").value);
                tracker.trackerMetadata.artist = Utils.ConvertToString(_sqlrow.getAttribute("artist").value);
                tracker.trackerMetadata.title = Utils.ConvertToString(_sqlrow.getAttribute("title").value);
                tracker.trackerMetadata.comments = Utils.ConvertToString(_sqlrow.getAttribute("comments").value);
                tracker.trackerMetadata.copyrightInformation = Utils.ConvertToString(_sqlrow.getAttribute("copyrightInformation").value);

                // get pistes
                List<Piste> pistes = new List<Piste>();
                List<Note> notes = main.bddCells.selectCells(tracker.idTracker);
                tracker.trackerContent.pistes[0].notes = notes;

                return tracker;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertTrackerToSQL(Tracker _tracker)
        {
            SqlRow tracker = new SqlRow(bdd.db_config.sqlTableTrackers);
            try
            {
                if (_tracker == null) return null;

                tracker.SetAttribut("bpm", _tracker.trackerContent.BPM);
                tracker.SetAttribut("artist", _tracker.trackerMetadata.artist);
                tracker.SetAttribut("title", _tracker.trackerMetadata.title);
                tracker.SetAttribut("comments", _tracker.trackerMetadata.comments);
                tracker.SetAttribut("copyrightInformation", _tracker.trackerMetadata.copyrightInformation);
                tracker.SetAttribut("idUser", _tracker.idUser);

                if (_tracker.idTracker >= 0)
                    tracker.SetAttribut("id", _tracker.idTracker);

            }
            catch (Exception ex)
            {
                Console.WriteLine("convert<<Object>>ToSQL - err: " + ex);
                throw new OwnException();
            }
            return tracker;
        }
        #endregion

    }
}
