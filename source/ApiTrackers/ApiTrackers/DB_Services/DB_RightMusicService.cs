
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.DB_ORM;
using static ApiTrackers.Objects.RightMusic;

namespace ApiRightMusics.Services
{
    public class DB_RightMusicService
    {
        SqlDatabase bdd;
        Main main;
        private int lastId = -1;
        SqlTable table;
        SqlCommand command;

        public DB_RightMusicService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableRightMusics;
            command = table.command;
            lastId = command.select().lastId(true);
        }
        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<RightMusic> selectRightMusics_bytrackerid(int _idTracker)
        {
            List<SqlRow> rows = command.select().all(false, _idTracker, "idTracker");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public List<RightMusic> selectRightMusics_byuserid(int _idUser)
        {
            List<SqlRow> rows = command.select().all(false, _idUser, "idUser");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public RightMusic selectRightMusic(int _idTracker, int _idUser)
        {
            SqlRow row = command.select().row(false, _idTracker, "idTracker", _idUser, "idUser");
            RightMusic rightMusic = convertSQLToRightMusic(row);
            return rightMusic;
        }
        
        public RightMusic createRightMusic(RightMusic _right)
        {


             RightMusic right = selectRightMusic(_right.idTracker, _right.idUser);
            if (right == null)
            {
                insertRightMusic(_right.right, _right.idUser, _right.idTracker);
                RightMusic rightInsered = selectRightMusic(_right.idTracker, _right.idUser);


                if (rightInsered == null)
                    throw new DatabaseRequestException("Expected RightMusic not found.");
                else
                    return rightInsered;
            }
            else
            {
                throw new AlreadyExistException(right.GetType());
            }

        }
        public RightMusic changeRightMusic(RightMusic _right)
        {

            RightMusic right = selectRightMusic(_right.idTracker, _right.idUser);
            if (right != null)
            {
                updateRightMusic(_right.right, _right.idTracker, _right.idUser);
                RightMusic rightUpdated = selectRightMusic(_right.idTracker, _right.idUser);

                if (rightUpdated == null)
                    return null;
                else
                    return rightUpdated;
            }
            else
            {
                throw new ForbiddenException();
            }
        }

        private RightMusic insertRightMusic(RightForMusic _rightMusicModel, int idUser, int idTracker)
        {
            int _canControlRightMusics = 1;

            SqlRow sqlRowToInsert = new SqlRow(bdd.tableRightMusics, false);

            sqlRowToInsert = convertRightMusicToSQL(sqlRowToInsert, _rightMusicModel, idTracker, idUser);
            sqlRowToInsert.setAttribute("idTracker", idTracker);
            sqlRowToInsert.setAttribute("idUser", idUser);
            sqlRowToInsert.setAttribute("id", getNextId());

            if (_canControlRightMusics == 1)
            {
           
                if (command.insert().insert(sqlRowToInsert))
                {
                    RightMusic checkRightMusic = selectRightMusic(idTracker, idUser);
                    if (checkRightMusic != null)
                    {
                        return checkRightMusic;
                    }
                }

            }
            return null;
        }

        private RightMusic updateRightMusic(RightForMusic _rightforMusic, int idTracker, int idUser)
        {

            int _canControlRightMusics = 1;


            SqlRow sqlRowToUpdate = command.select().row(true, idTracker, "idTracker", idUser, "idUser");
            if (sqlRowToUpdate == null) return null;

            if (_canControlRightMusics != 1) return null;

            sqlRowToUpdate = convertRightMusicToSQL(sqlRowToUpdate, _rightforMusic, idTracker, idUser);
            sqlRowToUpdate.setAttribute("idTracker", idTracker);
            sqlRowToUpdate.setAttribute("idUser", idUser);
            sqlRowToUpdate.setAttribute("rightValue", convertRightFM_toInt(_rightforMusic));

            bool checkUpdateCorrectly = command.update().update(sqlRowToUpdate, idTracker, "idTracker", idUser, "idUser");


            if (!checkUpdateCorrectly) return null;

            RightMusic rightMusicUpdated = selectRightMusic(idTracker, idUser);

            return rightMusicUpdated;
        }

        #endregion

        #region ******** convertions ******** 

        private int convertRightFM_toInt(RightForMusic _rfm)
        {
            int i = (int)_rfm;
            return i;
        }

        private RightMusic convertSQLToRightMusic(SqlRow _sqlrow)
        {
            if (_sqlrow == null) return null;
            try
            {
                RightMusic rightMusic = new RightMusic();
                rightMusic.id = Static.convertToInteger(_sqlrow.getAttribute("id").value);

                //test id
                string idTest = Static.convertToString(_sqlrow.getAttribute("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                rightMusic.id = Static.convertToInteger(_sqlrow.getAttribute("id").value);       
                rightMusic.idUser = Static.convertToInteger(_sqlrow.getAttribute("idUser").value);
                rightMusic.idTracker = Static.convertToInteger(_sqlrow.getAttribute("idTracker").value);
                int i = Convert.ToInt32(_sqlrow.getAttribute("rightValue").value);
                rightMusic.setRight(i);

                return rightMusic;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                throw new OwnException();
            }
        }

        private SqlRow convertRightMusicToSQL(SqlRow _sqlDest, RightForMusic _rightforMusic, int _idTracker, int _idUser, int _id)
        {
            if (_sqlDest == null) return null;
            try
            {
                if (_sqlDest == null) return null;

                _sqlDest.setAttribute("id", _id);
                _sqlDest.setAttribute("idUser", _idUser);
                _sqlDest.setAttribute("idTracker", _idTracker);
                _sqlDest.setAttribute("rightValue", convertRightFM_toInt(_rightforMusic));
            }
            catch (Exception ex)
            {
                Console.WriteLine("convert<<Object>>ToSQL - err: " + ex);
                throw new OwnException();
            }
            return _sqlDest;
        }
        private SqlRow convertRightMusicToSQL(SqlRow _sqlDest, RightForMusic _rightforMusic, int _idTracker, int _idUser)
        {
            if (_sqlDest == null) return null;
            try
            {
                if (_sqlDest == null) return null;

                _sqlDest.setAttribute("idUser", _idUser);
                _sqlDest.setAttribute("idTracker", _idTracker);
                _sqlDest.setAttribute("rightValue", convertRightFM_toInt(_rightforMusic));
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
