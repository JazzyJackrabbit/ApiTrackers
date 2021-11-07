﻿
using ApiTrackers;
using ApiTrackers.DB_ORM;
using ApiTrackers.DB_Services.ORM;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.SqlDatabase;

namespace ApiCells.Services
{
    public class DB_CellService
    {
        SqlDatabase bdd;
        Main main;
        private int lastId = -1;

        SqlTable table;
        SqlCommand command;

        public DB_CellService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableCells;
            command = table.command;
            lastId = command.select().lastId(true);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Note> selectCells(int _idTracker, bool _selfOpenClose)
        {
            List<SqlRow> rows = command.select().all(false, _idTracker, "idTracker", _selfOpenClose);
            List<Note> cells = new List<Note>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                cells.Add(convertSQLToCell(row));
            return cells;
        }
        public Note selectCell(int _id, int _idTracker, bool _selfOpenClose)
        {
            SqlRow row = command.select().row(false, _id, "id", _idTracker, "idTracker", _selfOpenClose);
            Note cell = convertSQLToCell(row);
            return cell;
        }

        public Note insertCell(Note _cellModel, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.tableCells, false);

            sqlRowToInsert = convertCellToSQL(sqlRowToInsert, _cellModel);

            sqlRowToInsert.setAttribute("id", id);
            
            int idTracker =  Static.convertToInteger(
                sqlRowToInsert.getAttribute("idTracker").value
                );

            if (command.insert().insert(sqlRowToInsert, false))
            {
                int id2 = getLastId();
                Note checkCell = selectCell(id2, idTracker, false);
                if (checkCell != null)
                {
                    command.connectClose(_selfOpenClose);
                    return checkCell;
                }
            }

            command.connectClose(_selfOpenClose);
            return null;
        }

        public Note updateCell(Note _cellModel, int id, bool _selfOpenClose)
        {
            //TODO

            command.connectOpen(_selfOpenClose);

            SqlRow sqlRowToUpdate = command.select().row(false, id, false);
            if (sqlRowToUpdate == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
            sqlRowToUpdate.setAttribute("id", id);
            
            sqlRowToUpdate = convertCellToSQL(sqlRowToUpdate, _cellModel);

            // TODO REWORK 

            bool checkUpdateCorrectly = command.update().update(sqlRowToUpdate, id, false);
            if (!checkUpdateCorrectly)
            {
                command.connectClose(_selfOpenClose); 
                return null;
            }
            SqlRow sqlRowCheck = command.select().row(false, id, false);
            Note cellUpdated = convertSQLToCell(sqlRowCheck);

            command.connectClose(_selfOpenClose);
            return cellUpdated;
        }
        public Note deleteCell(int _id, int _idUser, bool _selfOpenClose)
        {
            //TODO idUser
            command.connectOpen(_selfOpenClose);

            SqlRow rowToDelete = command.select().row(false, _id, false);
            Note cell = convertSQLToCell(rowToDelete);
            
            if (cell != null)
                command.delete().delete(_id, false);
            else
            {
                command.connectClose(_selfOpenClose);
                throw new ForbiddenException();
            }

            command.connectClose(_selfOpenClose);

            return cell;
        }

        #endregion

        #region ******** convertions ******** 

        private Note convertSQLToCell(SqlRow _sqlrow)
        {
            if (_sqlrow == null) return null;
            try
            {

                Note cell = new Note();

                cell.id = Static.convertToInteger(_sqlrow.getAttribute("id").value);

                //test id
                string idTest = Static.convertToString(_sqlrow.getAttribute("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                cell.effect = new Effect();
                cell.effect.name = "unknown";
                cell.sample = new Sample();
                cell.sample.name = "unknown";
                cell.piste = new Piste();
                cell.piste.name = "unknown";
                cell.parentTracker = new Tracker();
                cell.parentTracker.trackerContent.pistes = new List<Piste>();

                cell.id = Static.convertToInteger(_sqlrow.getAttribute("id").value);
                cell.parentTracker.idTracker = Static.convertToInteger(_sqlrow.getAttribute("idTracker").value);   //todo
                cell.sample.id = Static.convertToInteger(_sqlrow.getAttribute("idSample").value);    //todo           
                cell.piste.id = Static.convertToInteger(_sqlrow.getAttribute("idPiste").value);         //todo
                cell.position = Static.convertToInteger(_sqlrow.getAttribute("position").value);
                cell.volume = Static.convertToDouble(_sqlrow.getAttribute("volume").value);
                cell.effect.id = Static.convertToInteger(_sqlrow.getAttribute("effect").value);         //todo
                cell.freqSample = Static.convertToDouble(_sqlrow.getAttribute("frequence").value);
                cell.key = Static.convertToString(_sqlrow.getAttribute("positionKey").value);

                return cell;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertCellToSQL(SqlRow _sqlDest, Note _cell)
        {
            try
            {
                if (_sqlDest == null) return null;
                if (_cell == null) return null;

                //definition data
                _sqlDest.setAttribute("idTracker", _cell.parentTracker.idTracker); //todo convert here
                _sqlDest.setAttribute("idSample", _cell.sample.id); //todo convert here
                _sqlDest.setAttribute("idPiste", _cell.piste.id);
                _sqlDest.setAttribute("position", _cell.position);
                _sqlDest.setAttribute("volume", _cell.volume);
                _sqlDest.setAttribute("effect", _cell.effect.id);
                _sqlDest.setAttribute("frequence", _cell.freqSample);
                _sqlDest.setAttribute("positionKey", _cell.key);
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
