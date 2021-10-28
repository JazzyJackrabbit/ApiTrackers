
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

        public List<Note> selectCells()
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableCells, true);
            List<Note> cells = new List<Note>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                cells.Add(convertSQLToCell(row));
            return cells;
        }
        public Note selectCell(int _id)
        {
            SqlRow row = bdd.select(bdd.sqlTableCells, true, _id);
            Note cell = convertSQLToCell(row);
            return cell;
        }

        public Note insertCell(Note _cellModel)
        {
            //TODO //TODO //TODO

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableCells);

            //definition data
            bdd.setAttribute(sqlRowToInsert, "idTracker", _cellModel.parentTracker); //todo convert here
            bdd.setAttribute(sqlRowToInsert, "idSample", _cellModel.sample); //todo convert here
            bdd.setAttribute(sqlRowToInsert, "idPiste", _cellModel.piste);
            bdd.setAttribute(sqlRowToInsert, "position", _cellModel.position);
            bdd.setAttribute(sqlRowToInsert, "volume", _cellModel.volume);
            bdd.setAttribute(sqlRowToInsert, "effect", _cellModel.effect);
            bdd.setAttribute(sqlRowToInsert, "frequence", _cellModel.freqSample);
            bdd.setAttribute(sqlRowToInsert, "positionKey", _cellModel.key);

            bdd.setAttribute(sqlRowToInsert, "id", id);

            if (bdd.insert(bdd.sqlTableCells, sqlRowToInsert))
            {
                int id2 = getLastId();
                Note checkCell = selectCell(id2);
                if (checkCell != null)
                    return checkCell;
            }
            return null;
        }

        public Note updateCell(Note _cellModel, int id)
        {
            //TODO //TODO //TODO

            SqlRow sqlRowToUpdate = bdd.select(bdd.sqlTableCells, true, id);
            if (sqlRowToUpdate == null) return null;

            //definition data
            bdd.setAttribute(sqlRowToUpdate, "idTracker", _cellModel.parentTracker); //todo convert here
            bdd.setAttribute(sqlRowToUpdate, "idSample", _cellModel.sample); //todo convert here
            bdd.setAttribute(sqlRowToUpdate, "idPiste", _cellModel.piste);
            bdd.setAttribute(sqlRowToUpdate, "position", _cellModel.position);
            bdd.setAttribute(sqlRowToUpdate, "volume", _cellModel.volume);
            bdd.setAttribute(sqlRowToUpdate, "effect", _cellModel.effect);
            bdd.setAttribute(sqlRowToUpdate, "frequence", _cellModel.freqSample);
            bdd.setAttribute(sqlRowToUpdate, "positionKey", _cellModel.key);

            bdd.setAttribute(sqlRowToUpdate, "id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableCells, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.select(bdd.sqlTableCells, true, id);
            Note cellUpdated = convertSQLToCell(sqlRowCheck);

            return cellUpdated;
        }
        public Note deleteCell(int _id, int _idUser)
        {
            //TODO //TODO //TODO

            SqlRow rowToDelete = bdd.select(bdd.sqlTableCells, true, _id);
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
                cell.piste.name = "unknown";

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
                Console.WriteLine("BDDService - convertSQLToCell - err: " + ex);
                return null;
            }
        }

        #endregion
    }
}
