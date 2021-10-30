﻿using ApiTrackers.Exceptions;
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

            lastId = bdd.sqlTableTrackers.selectLastID(_main);
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
            sqlRowToInsert.setAttribute("id", id);
            sqlRowToInsert.setAttribute("idUser", _trackerModel.idUser);

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

            sqlRowToUpdate.setAttribute("id", id);
            sqlRowToUpdate.setAttribute("idUser", _trackerToUpdate.idUser);

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
            rowToDelete.setAttribute("idUser", tracker.idUser);

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
