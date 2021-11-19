
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

        public DB_CellService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;
        }

        public int getLastId() { return bdd.db_config.sqlTableCells.selectLastID(main);  }
        public int getNextId() { return bdd.db_config.sqlTableCells.selectLastID(main) + 1; }

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

            SqlRow sqlRowToInsert = convertCellToSQL(_cellModel, id);

            if (bdd.insert(bdd.db_config.sqlTableCells, sqlRowToInsert))
            {
                int id2 = getLastId();
                Note checkCell = selectCell(id2, _cellModel.idTracker);
                if (checkCell != null)
                    return checkCell;
            }
            return null;
        }

        public Note updateCell(Note _cellModel, int _id)
        {
            //TODO
            SqlRow sqlRowToUpdate = convertCellToSQL(_cellModel, _id);

            // TODO REWORK 

           /* Note cellToUpdate = convertSQLToCell(sqlRowToUpdate);
            SqlRow sqlRowToUpdate_2 = convertCellToSQL(sqlRowToUpdate, cellToUpdate);*/

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableCells, sqlRowToUpdate, _id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableCells, false, _id);
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

                cell.id = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);

                cell.effect = new Effect();
                cell.effect.name = "unknown";
                cell.sample = new Sample();
                cell.sample.name = "unknown";
                cell.piste = new Piste();
                cell.piste.name = "unknown";
                cell.parentTracker = new Tracker();
                cell.parentTracker.trackerContent.pistes = new List<Piste>();

                cell.id = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);
                cell.parentTracker.idTracker = Utils.ConvertToInteger(_sqlrow.getAttribute("idTracker").value);   //todo
                cell.sample.id = Utils.ConvertToInteger(_sqlrow.getAttribute("idSample").value);    //todo           
                cell.piste.id = Utils.ConvertToInteger(_sqlrow.getAttribute("idPiste").value);         //todo
                cell.position = Utils.ConvertToInteger(_sqlrow.getAttribute("position").value);
                cell.volume = Utils.ConvertToDouble(_sqlrow.getAttribute("volume").value);
                cell.effect.id = Utils.ConvertToInteger(_sqlrow.getAttribute("effect").value);         //todo
                cell.freqSample = Utils.ConvertToDouble(_sqlrow.getAttribute("frequence").value);
                cell.positionKey = Utils.ConvertToString(_sqlrow.getAttribute("positionKey").value);

                return cell;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertCellToSQL(Note _cell, int _id=-1)
        {
            SqlRow _sqlDest = new SqlRow(bdd.db_config.sqlTableCells);

            try
            {
                if (_cell == null) return null;

                //definition data
                _sqlDest.SetAttribut("idTracker", _cell.parentTracker.idTracker); //todo convert here
                _sqlDest.SetAttribut("idSample", _cell.sample.id); //todo convert here
                _sqlDest.SetAttribut("idPiste", _cell.piste.id);
                _sqlDest.SetAttribut("position", _cell.position);
                _sqlDest.SetAttribut("volume", _cell.volume);
                _sqlDest.SetAttribut("effect", _cell.effect.id);
                _sqlDest.SetAttribut("frequence", _cell.freqSample);
                _sqlDest.SetAttribut("positionKey", _cell.positionKey);

                if(_id >= 0)
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
