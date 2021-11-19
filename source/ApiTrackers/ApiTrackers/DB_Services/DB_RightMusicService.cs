
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
        SqlTable table;
        SqlCommand command;

        public DB_RightMusicService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableRightMusics;
            command = table.command;
        }

        public int getLastId() { return command.ExecuteReaderMaxId(table.name); }
        public int getNextId() { return command.ExecuteReaderMaxId(table.name) + 1; }

        #region ******** public methods ********

        public List<RightMusic> selectRightMusics_bytrackerid(int _idTracker)
        {
            List<SqlRow> rows = command.Select().all(false, _idTracker, "idTracker");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public List<RightMusic> selectRightMusics_byuserid(int _idUser)
        {
            List<SqlRow> rows = command.Select().all(false, _idUser, "idUser");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public RightMusic selectRightMusic(int _idTracker, int _idUser)
        {
            SqlRow row = command.Select().row(false, _idTracker, "idTracker", _idUser, "idUser");
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
        public RightMusic UpdateRightMusic(RightMusic _right)
        {

            RightMusic right = selectRightMusic(_right.idTracker, _right.idUser);
            if (right != null)
            {
                updateRightMusic(_right.right, _right.idTracker, _right.idUser, _right.id);
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

            SqlRow sqlRowToInsert = convertRightMusicToSQL(_rightMusicModel, idTracker, idUser, getNextId());

            if (_canControlRightMusics == 1)
            {
           
                if (command.Insert().insert(sqlRowToInsert))
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

        private RightMusic updateRightMusic(RightForMusic _rightforMusic, int _idTracker, int _idUser, int _id)
        {

            int _canControlRightMusics = 1;


            SqlRow sqlRowToUpdate = command.Select().row(true, _idTracker, "idTracker", _idUser, "idUser");
            if (sqlRowToUpdate == null) return null;

            if (_canControlRightMusics != 1) return null;

            sqlRowToUpdate = convertRightMusicToSQL(_rightforMusic, _idTracker, _idUser, _id);

            bool checkUpdateCorrectly = command.Update().update(sqlRowToUpdate, _idTracker, "idTracker", _idUser, "idUser");


            if (!checkUpdateCorrectly) return null;

            RightMusic rightMusicUpdated = selectRightMusic(_idTracker, _idUser);

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
                rightMusic.id = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);

                //test id
                string idTest = Utils.ConvertToString(_sqlrow.GetAttribut("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                rightMusic.id = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);       
                rightMusic.idUser = Utils.ConvertToInteger(_sqlrow.GetAttribut("idUser").value);
                rightMusic.idTracker = Utils.ConvertToInteger(_sqlrow.GetAttribut("idTracker").value);
                int i = Convert.ToInt32(_sqlrow.GetAttribut("canEdit").value);
                rightMusic.SetRight(i);

                return rightMusic;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                throw new OwnException();
            }
        }

        private SqlRow convertRightMusicToSQL(RightForMusic _rightforMusic, int _idTracker, int _idUser, int _id)
        {
            SqlRow _sqlDest = new(table);
            try
            {
                _sqlDest.SetAttribut("id", _id);
                _sqlDest.SetAttribut("idUser", _idUser);
                _sqlDest.SetAttribut("idTracker", _idTracker);
                _sqlDest.SetAttribut("canEdit", convertRightFM_toInt(_rightforMusic));
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
