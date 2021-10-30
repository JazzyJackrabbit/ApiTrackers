
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.BDD_MainService;

namespace ApiCells.Services
{
    public class BDD_CellService
    {
        BDD_MainService bdd;
        MainService main;
        private int lastId = -1;

        public BDD_CellService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = main.bdd.selectLastID(bdd.sqlTableCells);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Note> selectCells(int _idTracker)
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableCells, false, _idTracker, "idTracker");
            List<Note> cells = new List<Note>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                cells.Add(convertSQLToCell(row));
            return cells;
        }
        public Note selectCell(int _id, int _idTracker)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.sqlTableCells, false, _id, "id", _idTracker, "idTracker");
            Note cell = convertSQLToCell(row);
            return cell;
        }

        public Note insertCell(Note _cellModel)
        { 
            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableCells);

            sqlRowToInsert = convertCellToSQL(sqlRowToInsert, _cellModel);

            bdd.setAttribute(sqlRowToInsert, "id", id);
            
            int idTracker =  Static.convertToInteger(
                bdd.getAttribute(sqlRowToInsert, "idTracker").value
                );

            if (bdd.insert(bdd.sqlTableCells, sqlRowToInsert))
            {
                int id2 = getLastId();
                Note checkCell = selectCell(id2, idTracker);
                if (checkCell != null)
                    return checkCell;
            }
            return null;
        }

        public Note updateCell(Note _cellModel, int id)
        {
            //TODO

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.sqlTableCells, false, id);
            if (sqlRowToUpdate == null) return null;

            bdd.setAttribute(sqlRowToUpdate, "id", id);
            
            sqlRowToUpdate = convertCellToSQL(sqlRowToUpdate, _cellModel);

            // TODO REWORK 

           /* Note cellToUpdate = convertSQLToCell(sqlRowToUpdate);
            SqlRow sqlRowToUpdate_2 = convertCellToSQL(sqlRowToUpdate, cellToUpdate);*/

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableCells, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.sqlTableCells, false, id);
            Note cellUpdated = convertSQLToCell(sqlRowCheck);

            return cellUpdated;
        }
        public Note deleteCell(int _id, int _idUser)
        {
            //TODO //TODO //TODO

            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.sqlTableCells, false, _id);
            Note cell = convertSQLToCell(rowToDelete);
            
            if (cell != null)   
                bdd.delete(bdd.sqlTableCells, _id); 
            else
                throw new ForbiddenException();
            return cell;
        }

        #endregion

        #region ******** convertions ******** 

        private Note convertSQLToCell(SqlRow _sqlrow)
        {
            try
            {
                Note cell = new Note();

                cell.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "id").value);

                //test id
                string idTest = Static.convertToString(bdd.getAttribute(_sqlrow, "id").value);
                if (!int.TryParse(idTest, out _)) return null;

                cell.effect = new Effect();
                cell.effect.name = "unknown";
                cell.sample = new Sample();
                cell.sample.name = "unknown";
                cell.piste = new Piste();
                cell.piste.name = "unknown";
                cell.parentTracker = new Tracker();
                cell.parentTracker.trackerContent.pistes = new List<Piste>();

                cell.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "id").value);
                cell.parentTracker.idTracker = Static.convertToInteger(bdd.getAttribute(_sqlrow, "idTracker").value);   //todo
                cell.sample.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "idSample").value);    //todo           
                cell.piste.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "idPiste").value);         //todo
                cell.position = Static.convertToInteger(bdd.getAttribute(_sqlrow, "position").value);
                cell.volume = Static.convertToDouble(bdd.getAttribute(_sqlrow, "volume").value);
                cell.effect.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "effect").value);         //todo
                cell.freqSample = Static.convertToDouble(bdd.getAttribute(_sqlrow, "frequence").value);
                cell.key = Static.convertToString(bdd.getAttribute(_sqlrow, "positionKey").value);

                return cell;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertCellToSQL(SqlRow _sqlDest, Note _smpl)
        {
            try
            {
                //definition data
                bdd.setAttribute(_sqlDest, "idTracker", _smpl.parentTracker.idTracker); //todo convert here
                bdd.setAttribute(_sqlDest, "idSample", _smpl.sample.id); //todo convert here
                bdd.setAttribute(_sqlDest, "idPiste", _smpl.piste.id);
                bdd.setAttribute(_sqlDest, "position", _smpl.position);
                bdd.setAttribute(_sqlDest, "volume", _smpl.volume);
                bdd.setAttribute(_sqlDest, "effect", _smpl.effect.id);
                bdd.setAttribute(_sqlDest, "frequence", _smpl.freqSample);
                bdd.setAttribute(_sqlDest, "positionKey", _smpl.key);
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
