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
        SqlTable table;
        SqlCommand command;

        public DB_TrackerService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableTrackers;
            command = table.command;
        }

        public int getLastId() { return command.ExecuteReaderMaxId(table.name); }
        public int getNextId() { return command.ExecuteReaderMaxId(table.name) + 1; }

        #region ******** public methods ********

        public List<Tracker> selectTrackers()
        {

            List<SqlRow> rows = command.Select().all(false);
            List<Tracker> trackers = new List<Tracker>();
            if (rows == null)
            {
                return null;
            }
            foreach (SqlRow row in rows)
                trackers.Add(convertSQLToTracker(row));

            return trackers;
        }
        public Tracker selectTracker(int _id)
        {

            SqlRow row = command.Select().row(true, _id);
            Tracker tracker = convertSQLToTracker(row);


            return tracker;
        }

        public Tracker insertTracker(Tracker _trackerModel)
        {

            int id = getNextId();

            SqlRow sqlRowToInsert = convertTrackerToSQL(_trackerModel, id);

            if (command.Insert().insert(sqlRowToInsert))
            {
                Tracker checkTracker = selectTracker(id);
                if (checkTracker != null)
                {
                    return checkTracker;
                }
            }

            return null;
        }

        public Tracker updateTracker(Tracker _trackerModel, int _idUser)
        {
            SqlRow sqlRowToUpdate = command.Select().row(true, _trackerModel.id);
            if (sqlRowToUpdate == null)
            {
                return null;
            }

            sqlRowToUpdate = convertTrackerToSQL(_trackerModel);

            if (_trackerModel.idUser != _idUser)
                //if ()  //TODO rightMusics users
                throw new ForbiddenException();

            bool checkUpdateCorrectly = command.Update().update(sqlRowToUpdate, _trackerModel.id);
            if (!checkUpdateCorrectly)
            {
                return null;
            }

            SqlRow sqlRowCheck = command.Select().row(true, _trackerModel.id);
            Tracker trackerUpdated = convertSQLToTracker(sqlRowCheck);

            return trackerUpdated;
        }
        public Tracker deleteTracker(int _id, int _idUser)
        {

            SqlRow rowToDelete = command.Select().row(true, _id);
            Tracker tracker = convertSQLToTracker(rowToDelete);

            if (tracker == null)
            {
                throw new Exception();
            }

            // ? >> bdd.setAttribute(rowToDelete, "id", _id);

            if (tracker.idUser == _idUser)
            {
                command.Delete().delete(_id); //delete now
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
                tracker.id = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);
                tracker.idUser = Utils.ConvertToInteger(_sqlrow.GetAttribut("idUser").value);
                tracker.trackerContent.BPM = Utils.ConvertToInteger(_sqlrow.GetAttribut("bpm").value);
                tracker.trackerMetadata.artist = Utils.ConvertToString(_sqlrow.GetAttribut("artist").value);
                tracker.trackerMetadata.title = Utils.ConvertToString(_sqlrow.GetAttribut("title").value);
                tracker.trackerMetadata.comments = Utils.ConvertToString(_sqlrow.GetAttribut("comments").value);
                tracker.trackerMetadata.copyrightInformation = Utils.ConvertToString(_sqlrow.GetAttribut("copyrightInformation").value);

                // get pistes
                List<Piste> pistes = new List<Piste>();
                List<Note> notes = main.bddCells.selectCells(tracker.id);
                tracker.trackerContent.pistes[0].notes = notes;

                return tracker;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertTrackerToSQL(Tracker _tracker, int _id=-1)
        {
            SqlRow tracker = new(table);
            try
            {
                if (_tracker == null) return null;

                tracker.SetAttribut("bpm", _tracker.trackerContent.BPM);
                tracker.SetAttribut("artist", _tracker.trackerMetadata.artist);
                tracker.SetAttribut("title", _tracker.trackerMetadata.title);
                tracker.SetAttribut("comments", _tracker.trackerMetadata.comments);
                tracker.SetAttribut("copyrightInformation", _tracker.trackerMetadata.copyrightInformation);
                tracker.SetAttribut("idUser", _tracker.idUser);
                if (_id >= 0)
                    tracker.SetAttribut("id", _id);
                else if (_tracker.id >= 0)
                    tracker.SetAttribut("id", _tracker.id);

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
