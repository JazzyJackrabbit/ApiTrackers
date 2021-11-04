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
        private int lastId = -1;

        public DB_UserService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = bdd.db_config.sqlTableUsers.selectLastID(_main);
        }

        public int getLastId(){return lastId;}
        public int getNextId(){lastId++;return lastId;}

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
        public User insertUser()
        {
            int id = getNextId();//selectLastIDPlusOne(bdd.sqlTableUsers);
            SqlRow sqlRowToInsert = new SqlRow(bdd.db_config.sqlTableUsers);

            sqlRowToInsert.setAttribute("id", id);

            if (bdd.insert(bdd.db_config.sqlTableUsers, sqlRowToInsert))
            {
                User checkUser = selectUser(id);
                if (checkUser != null)
                    return checkUser;
            }
            return null;
        }

        public User insertUser(User _userModel)
        {
            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.db_config.sqlTableUsers);

            sqlRowToInsert = convertUserToSQL(sqlRowToInsert, _userModel);
            sqlRowToInsert.setAttribute("id", id);

            if (bdd.insert(bdd.db_config.sqlTableUsers, sqlRowToInsert))
            {
                int id2 = getLastId();
                User checkUser = selectUser(id2);
                if (checkUser != null)
                    return checkUser;
            }
            return null;
        }
        public User updateUser(User _userModel, int id)
        {
            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableUsers, true, id);
            if (sqlRowToUpdate == null) return null;

            sqlRowToUpdate = convertUserToSQL(sqlRowToUpdate, _userModel);
            sqlRowToUpdate.setAttribute("id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableUsers, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableUsers, true, id);
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
                user.id = Static.convertToInteger(_sqlrow.getAttribute( "id").value);

                //test id
                string idTest = Static.convertToString(_sqlrow.getAttribute( "id").value);
                if (!int.TryParse(idTest, out _)) return null;

                user.mail = Static.convertToString(_sqlrow.getAttribute( "mail").value);
                user.passwordHash = Static.convertToString(_sqlrow.getAttribute( "passwordHash").value);
                user.pseudo = Static.convertToString(_sqlrow.getAttribute( "pseudo").value);
                user.recoverMails = Static.convertToInteger(_sqlrow.getAttribute( "recoverMails").value);
                    
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                throw new OwnException();
            }
        }
        private SqlRow convertUserToSQL(SqlRow _sqlDest, User _user)
        {
            try
            {
                if (_sqlDest == null) return null;
                if (_user == null) return null;

                _sqlDest.setAttribute( "recoverMails", _user.recoverMails);
                _sqlDest.setAttribute( "mail", _user.mail);
                _sqlDest.setAttribute( "passwordHash", _user.passwordHash);
                _sqlDest.setAttribute( "pseudo", _user.pseudo);
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
