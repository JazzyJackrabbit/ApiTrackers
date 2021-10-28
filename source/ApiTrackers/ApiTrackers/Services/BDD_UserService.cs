using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.BDD_MainService;

namespace ApiTrackers.Services
{
    public class BDD_UserService
    {
        BDD_MainService bdd;
        MainService main;
        private int lastId = -1;

        public BDD_UserService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = main.bdd.selectLastID(bdd.sqlTableUsers);
        }

        public int getLastId(){return lastId;}
        public int getNextId(){lastId++;return lastId;}

        #region ******** public methods ********

        public List<User> selectUsers()
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableUsers, true);
            List<User> users = new List<User>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                users.Add(convertSQLToUser(row));
            return users;
        }
        public User selectUser(int _id)
        {
            SqlRow row = bdd.select(bdd.sqlTableUsers, true, _id);
            User user = convertSQLToUser(row);
            return user;
        }
        public User insertUser()
        {
            int id = getNextId();//selectLastIDPlusOne(bdd.sqlTableUsers);
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableUsers);

            bdd.setAttribute(sqlRowToInsert, "id", id);

            if (bdd.insert(bdd.sqlTableUsers, sqlRowToInsert))
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
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableUsers);

            //definition data
            bdd.setAttribute(sqlRowToInsert, "recoverMails", _userModel.recoverMails);
            bdd.setAttribute(sqlRowToInsert, "mail", _userModel.mail);
            bdd.setAttribute(sqlRowToInsert, "passwordHash", _userModel.passwordHash);
            bdd.setAttribute(sqlRowToInsert, "pseudo", _userModel.pseudo);
           
            bdd.setAttribute(sqlRowToInsert, "id", id);

            if (bdd.insert(bdd.sqlTableUsers, sqlRowToInsert))
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
            SqlRow sqlRowToUpdate = bdd.select(bdd.sqlTableUsers, true, id);
            if (sqlRowToUpdate == null) return null;

            //definition data
            bdd.setAttribute(sqlRowToUpdate, "recoverMails", _userModel.recoverMails);
            bdd.setAttribute(sqlRowToUpdate, "mail", _userModel.mail);
            bdd.setAttribute(sqlRowToUpdate, "passwordHash", _userModel.passwordHash);
            bdd.setAttribute(sqlRowToUpdate, "pseudo", _userModel.pseudo);

            bdd.setAttribute(sqlRowToUpdate, "id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableUsers, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.select(bdd.sqlTableUsers, true, id);
            User userUpdated = convertSQLToUser(sqlRowCheck);

            return userUpdated;
        }
        public User deleteUser(int _id)
        {
            SqlRow rowToDelete = bdd.select(bdd.sqlTableUsers, true, _id);
            User user = convertSQLToUser(rowToDelete);
            if (user != null)
                bdd.delete(bdd.sqlTableUsers, _id); //delete now
            return user;
        }

        #endregion

        #region ******** convertions ******** 
        private User convertSQLToUser(SqlRow _sqlrow)
        {
            try
            {
                User user = new User();
                user.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "id").value);

                //test id
                string idTest = Static.convertToString(bdd.getAttribute(_sqlrow, "id").value);
                if (!int.TryParse(idTest, out _)) return null;

                user.mail = Static.convertToString(bdd.getAttribute(_sqlrow, "mail").value);
                user.passwordHash = Static.convertToString(bdd.getAttribute(_sqlrow, "passwordHash").value);
                user.pseudo = Static.convertToString(bdd.getAttribute(_sqlrow, "pseudo").value);
                user.recoverMails = Static.convertToInteger(bdd.getAttribute(_sqlrow, "recoverMails").value);
                    
                return user;
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
