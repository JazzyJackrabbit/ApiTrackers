
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.DB_MainService;

namespace ApiCells.Services
{
    public class DB_CellService
    {
        DB_MainService bdd;
        MainService main;
        private int lastId = -1;

        public DB_CellService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = bdd.db_config.sqlTableCells.selectLastID(_main);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Note> selectCells(int _idTracker)
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableCells, false, _idTracker, "idTracker");
            List<Note> cells = new List<Note>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                cells.Add(convertSQLToCell(row));
            return cells;
        }
        public Note selectCell(int _id, int _idTracker)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableCells, false, _id, "id", _idTracker, "idTracker");
            Note cell = convertSQLToCell(row);
            return cell;
        }

        public Note insertCell(Note _cellModel)
        { 
            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.db_config.sqlTableCells);

            sqlRowToInsert = convertCellToSQL(sqlRowToInsert, _cellModel);

            sqlRowToInsert.setAttribute("id", id);
            
            int idTracker =  Static.convertToInteger(
                sqlRowToInsert.getAttribute("idTracker").value
                );

            if (bdd.insert(bdd.db_config.sqlTableCells, sqlRowToInsert))
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

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableCells, false, id);
            if (sqlRowToUpdate == null) return null;

            sqlRowToUpdate.setAttribute("id", id);
            
            sqlRowToUpdate = convertCellToSQL(sqlRowToUpdate, _cellModel);

            // TODO REWORK 

           /* Note cellToUpdate = convertSQLToCell(sqlRowToUpdate);
            SqlRow sqlRowToUpdate_2 = convertCellToSQL(sqlRowToUpdate, cellToUpdate);*/

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableCells, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableCells, false, id);
            Note cellUpdated = convertSQLToCell(sqlRowCheck);

            return cellUpdated;
        }
        public Note deleteCell(int _id, int _idUser)
        {
            //TODO //TODO //TODO

            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.db_config.sqlTableCells, false, _id);
            Note cell = convertSQLToCell(rowToDelete);
            
            if (cell != null)   
                bdd.delete(bdd.db_config.sqlTableCells, _id); 
            else
                throw new ForbiddenException();
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
