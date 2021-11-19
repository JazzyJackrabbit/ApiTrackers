using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.DB_MainService;

namespace ApiTrackers.Services
{
    public class DB_UserService
    {
        DB_MainService bdd;
        MainService main;

        public DB_UserService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;
        }

        public int getLastId(){ return bdd.db_config.sqlTableUsers.selectLastID(main); }
        public int getNextId(){ return bdd.db_config.sqlTableUsers.selectLastID(main) + 1; }

        #region ******** public methods ********

        public List<User> selectUsers()
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableUsers, true);
            List<User> users = new List<User>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                users.Add(convertSQLToUser(row));
            return users;
        }
        public User selectUser(int _id)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableUsers, true, _id);
            User user = convertSQLToUser(row);
            return user;
        }

        public User insertUser(User _userModel)
        {
            int id = getNextId();
            SqlRow sqlRowToInsert = convertUserToSQL(_userModel, id);

            if (bdd.insert(bdd.db_config.sqlTableUsers, sqlRowToInsert))
            {
                User checkUser = selectUser(id);
                if (checkUser != null)
                    return checkUser;
            }
            return null;
        }
        public User updateUser(User _userModel)
        {
            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableUsers, true, _userModel.id);
            if (sqlRowToUpdate == null) return null;

            sqlRowToUpdate = convertUserToSQL(_userModel);

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableUsers, sqlRowToUpdate, _userModel.id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableUsers, true, _userModel.id);
            User userUpdated = convertSQLToUser(sqlRowCheck);

            return userUpdated;
        }
        public User deleteUser(int _id)
        {
            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.db_config.sqlTableUsers, true, _id);
            User user = convertSQLToUser(rowToDelete);
            if (user != null)
                bdd.delete(bdd.db_config.sqlTableUsers, _id); //delete now
            return user;
        }

        #endregion

        #region ******** convertions ******** 
        private User convertSQLToUser(SqlRow _sqlrow)
        {
            if (_sqlrow == null) return null;
            try
            {
                User user = new User();
                user.id = Utils.ConvertToInteger(_sqlrow.getAttribute( "id").value);

                user.mail = Utils.ConvertToString(_sqlrow.getAttribute( "mail").value);
                user.passwordHash = Utils.ConvertToString(_sqlrow.getAttribute( "passwordHash").value);
                user.pseudo = Utils.ConvertToString(_sqlrow.getAttribute( "pseudo").value);
                user.recoverMails = Utils.ConvertToInteger(_sqlrow.getAttribute( "recoverMails").value);
                    
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                throw new OwnException();
            }
        }
        private SqlRow convertUserToSQL(User _user, int _id=-1)
        {
            SqlRow _sqlDest = new SqlRow(bdd.db_config.sqlTableUsers);
            try
            {
                if (_user == null) return null;
                _sqlDest.SetAttribut("isEnable", 1);
                _sqlDest.SetAttribut("adminMode", 0);
                _sqlDest.SetAttribut( "recoverMails", _user.recoverMails);
                _sqlDest.SetAttribut( "mail", _user.mail);
                _sqlDest.SetAttribut( "passwordHash", _user.passwordHash);
                _sqlDest.SetAttribut( "pseudo", _user.pseudo);
                if(_user.id >= 0)
                    _sqlDest.SetAttribut("id", _user.id);
                else if(_id >= 0)
                    _sqlDest.SetAttribut("id", _id);

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
