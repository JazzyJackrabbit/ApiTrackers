using ApiTrackers.DB_ORM;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;

namespace ApiTrackers.Services
{
    public class DB_UserService
    {
        SqlDatabase bdd;
        Main main;
        readonly SqlTable table;
        readonly SqlCommand command;

        public DB_UserService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableUsers;
            command = table.command;
        }

        public int getLastId(){ return command.ExecuteReaderMaxId(table.name); }
        public int getNextId(){ return command.ExecuteReaderMaxId(table.name) + 1; }

        #region ******** public methods ********

        public List<User> selectUsers()
        {
            List<SqlRow> rows = command.Select().all();
            List<User> users = new List<User>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                users.Add(convertSQLToUser(row));
            return users;
        }
        public User selectUser(int _id)
        {
            SqlRow row = command.Select().row(true, _id);
            User user = convertSQLToUser(row);
            return user;
        }

        public User insertUser(User _userModel)
        {
            int id = getNextId();
            SqlRow sqlRowToInsert = convertUserToSQL(_userModel, id);

            if (command.Insert().insert(sqlRowToInsert))
            {
                User checkUser = selectUser(id);
                if (checkUser != null)
                {
                    return checkUser;
                }   
                throw new DatabaseRequestException();
            }
            return null;
        }
        public User updateUser(User _userModel)
        {
            SqlRow sqlRowToUpdate = command.Select().row(true, _userModel.id);
            if (sqlRowToUpdate == null) return null;

            sqlRowToUpdate = convertUserToSQL(_userModel);

            bool checkUpdateCorrectly = command.Update().update(sqlRowToUpdate, _userModel.id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = command.Select().row(true, _userModel.id);
            User userUpdated = convertSQLToUser(sqlRowCheck);

            return userUpdated;
        }
        public User deleteUser(int _id)
        {

            SqlRow rowToDelete = command.Select().row(true, _id);
            User user = convertSQLToUser(rowToDelete);
            if (user != null)
                command.Delete().delete(_id); //delete now

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
                user.id = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);

                user.mail = Utils.ConvertToString(_sqlrow.GetAttribut( "mail").value);
                user.passwordHash = Utils.ConvertToString(_sqlrow.GetAttribut( "passwordHash").value);
                user.pseudo = Utils.ConvertToString(_sqlrow.GetAttribut( "pseudo").value);
                user.recoverMails = Utils.ConvertToInteger(_sqlrow.GetAttribut( "recoverMails").value);
                    
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
            SqlRow _sqlDest = new(table);
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
