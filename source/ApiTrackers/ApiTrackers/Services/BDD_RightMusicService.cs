
using ApiSamples.Services;
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.BDD_MainService;
using static ApiTrackers.Objects.RightMusic;

namespace ApiRightMusics.Services
{
    public class BDD_RightMusicService
    {
        BDD_MainService bdd;
        MainService main;
        private int lastId = -1;

        public BDD_RightMusicService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = bdd.sqlTableRightMusics.selectLastID(_main);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<RightMusic> selectRightMusics_bytrackerid(int _idTracker)
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableRightMusics, false, _idTracker, "idTracker");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public List<RightMusic> selectRightMusics_byuserid(int _idUser)
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableRightMusics, false, _idUser, "idUser");
            List<RightMusic> rightMusics = new List<RightMusic>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                rightMusics.Add(convertSQLToRightMusic(row));
            return rightMusics;
        }
        public RightMusic selectRightMusic(int _idTracker, int _idUser)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.sqlTableRightMusics, false, _idTracker, "idTracker", _idUser, "idUser");
            RightMusic rightMusic = convertSQLToRightMusic(row);
            return rightMusic;
        }
        
        public RightMusic createRightMusic(RightMusic _right, int _idTracker, int _idUser)
        {
            RightMusic right = selectRightMusic(_idTracker, _idUser);
            if (right == null)
            { // insert
                insertRightMusic(_right, _idUser, _idTracker);
                RightMusic rightInsered = selectRightMusic(_idTracker, _idUser);

                if (rightInsered == null)
                    return null;
                else
                    return rightInsered;
            }
                else throw new AlreadyExistException(right.GetType());

        }
        public RightMusic changeRightMusic(RightMusic _right, int _idTracker, int _idUser)
        {
            RightMusic right = selectRightMusic(_idTracker, _idUser);
            if (right != null)
            {

                //TODO Right users
                //if()

                // update
                updateRightMusic(_right, _idTracker, _idUser);
                RightMusic rightUpdated = selectRightMusic(_idTracker, _idUser);

                if (rightUpdated == null)
                    return null;
                else
                    return rightUpdated;
            }
            else
                 throw new ForbiddenException();
        }

        private RightMusic insertRightMusic(RightMusic _rightMusicModel, int idUser, int idTracker)
        {
            int _canControlRightMusics = 1;

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableRightMusics);

            sqlRowToInsert = convertRightMusicToSQL(sqlRowToInsert, _rightMusicModel);
            sqlRowToInsert.setAttribute("idTracker", idTracker);
            sqlRowToInsert.setAttribute("idUser", idUser);

            if (_canControlRightMusics == 1)
                if (bdd.insert(bdd.sqlTableRightMusics, sqlRowToInsert))
                {
                    RightMusic checkRightMusic = selectRightMusic(idTracker, idUser);
                    if (checkRightMusic != null)
                        return checkRightMusic;
                }
            return null;
        }

        private RightMusic updateRightMusic(RightMusic _rightMusicModel, int idTracker, int idUser)
        {
            int _canControlRightMusics = 1;

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.sqlTableRightMusics, true, idTracker, "idTracker", idUser, "idUser");
            if (sqlRowToUpdate == null) return null;

            if (_canControlRightMusics != 1) return null;

            sqlRowToUpdate = convertRightMusicToSQL(sqlRowToUpdate, _rightMusicModel);
            sqlRowToUpdate.setAttribute("idTracker", idTracker);
            sqlRowToUpdate.setAttribute("idUser", idUser);

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableRightMusics, sqlRowToUpdate, idTracker, "idTracker", idUser, "idUser");
            if (!checkUpdateCorrectly) return null;

            RightMusic rightMusicUpdated = selectRightMusic(idTracker, idUser);

            return rightMusicUpdated;
        }
       
        #endregion

        #region ******** convertions ******** 

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
                rightMusic.setRight(_sqlrow.getAttribute("canEdit").value);

                return rightMusic;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }

        private SqlRow convertRightMusicToSQL(SqlRow _sqlDest, RightMusic _rightMusic)
        {
            if (_sqlDest == null) return null;
            try
            {
                if (_sqlDest == null) return null;
                if (_rightMusic == null) return null;

                _sqlDest.setAttribute( "idUser", _rightMusic.id);
                _sqlDest.setAttribute( "idUser", _rightMusic.idUser);
                _sqlDest.setAttribute( "idTracker", _rightMusic.idTracker);
                _sqlDest.setAttribute( "canEdit", _rightMusic.right);
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
