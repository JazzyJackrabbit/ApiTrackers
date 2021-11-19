
using ApiSamples.Services;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.DB_MainService;
using static ApiTrackers.Objects.RightMusic;

namespace ApiRightMusics.Services
{
    public class DB_RightMusicService
    {
        DB_MainService bdd;
        MainService main;

        public DB_RightMusicService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;
        }

        public int getLastId() { return bdd.db_config.sqlTableRightMusics.selectLastID(main); }
        public int getNextId() { return bdd.db_config.sqlTableRightMusics.selectLastID(main) +1; }

        #region ******** public methods ********

        public List<RightMusic> selectRightMusics_bytrackerid(int _idTracker)
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableRightMusics, false, _idTracker, "idTracker");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public List<RightMusic> selectRightMusics_byuserid(int _idUser)
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableRightMusics, false, _idUser, "idUser");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public RightMusic selectRightMusic(int _idTracker, int _idUser)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableRightMusics, false, _idTracker, "idTracker", _idUser, "idUser");
            RightMusic rightMusic = convertSQLToRightMusic(row);
            return rightMusic;
        }
        
        public RightMusic createRightMusic(RightForMusic _rightForMusic, int _idTracker, int _idUser)
        {
            RightMusic right = selectRightMusic(_idTracker, _idUser);
            if (right == null)
            { // insert
                insertRightMusic(_rightForMusic, _idUser, _idTracker);
                RightMusic rightInsered = selectRightMusic(_idTracker, _idUser);

                if (rightInsered == null)
                    return null;
                else
                    return rightInsered;
            }
                else throw new AlreadyExistException(right.GetType());

        }
        public RightMusic changeRightMusic(RightForMusic _right, int _idTracker, int _idUser)
        {
            RightMusic right = selectRightMusic(_idTracker, _idUser);
            if (right != null)
            {

                //TODO Right users
                //if()

                // update
                updateRightMusic(_right, _idTracker, _idUser, right.id);
                RightMusic rightUpdated = selectRightMusic(_idTracker, _idUser);

                if (rightUpdated == null)
                    return null;
                else
                    return rightUpdated;
            }
            else
                 throw new ForbiddenException();
        }

        private RightMusic insertRightMusic(RightForMusic _rightMusicModel, int idUser, int idTracker)
        {
            int _canControlRightMusics = 1;

            int id = getNextId();
            SqlRow sqlRowToInsert = convertRightMusicToSQL(_rightMusicModel, idTracker, idUser,id);

            if (_canControlRightMusics == 1)
                if (bdd.insert(bdd.db_config.sqlTableRightMusics, sqlRowToInsert))
                {
                    RightMusic checkRightMusic = selectRightMusic(idTracker, idUser);
                    if (checkRightMusic != null)
                        return checkRightMusic;
                }
            return null;
        }

        private RightMusic updateRightMusic(RightForMusic _rightforMusic, int _idTracker, int _idUser, int _id)
        {
            int _canControlRightMusics = 1;

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableRightMusics, true, _idTracker, "idTracker", _idUser, "idUser");
            if (sqlRowToUpdate == null) return null;

            if (_canControlRightMusics != 1) return null;

            sqlRowToUpdate = convertRightMusicToSQL(_rightforMusic, _idTracker, _idUser, _id);

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableRightMusics, sqlRowToUpdate, _idTracker, "idTracker", _idUser, "idUser");
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
                rightMusic.id = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);

                //test id
                string idTest = Utils.ConvertToString(_sqlrow.getAttribute("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                rightMusic.id = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);       
                rightMusic.idUser = Utils.ConvertToInteger(_sqlrow.getAttribute("idUser").value);
                rightMusic.idTracker = Utils.ConvertToInteger(_sqlrow.getAttribute("idTracker").value);
                int i = Convert.ToInt32(_sqlrow.getAttribute("canEdit").value);
                rightMusic.SetRight(i);

                return rightMusic;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }

        private SqlRow convertRightMusicToSQL(RightForMusic _rightforMusic, int _idTracker, int _idUser, int _id)
        {
            SqlRow _sqlDest = new SqlRow(bdd.db_config.sqlTableRightMusics);
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
