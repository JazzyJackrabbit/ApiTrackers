
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.DB_MainService;

namespace ApiSamples.Services
{
    public class DB_SampleAliasService
    {
        DB_MainService bdd;
        Main main;
        private int lastId = -1;

        public DB_SampleAliasService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = bdd.db_config.sqlTableSamples.selectLastID(_main,true);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<SampleAlias> selectSamplesAliasByIdUser(int _idUser)
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableSamplesAlias, true, _idUser, "idUser");
            List<SampleAlias> samplesAlias = new List<SampleAlias>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                samplesAlias.Add(convertSQLToSampleAlias(row));
            return samplesAlias;
        }
      
        public SampleAlias selectSampleAlias(int _idUser, int _idSample)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableSamplesAlias, true, _idUser, "idUser", _idSample, "idSample");
            if (row == null) return null;
            SampleAlias sampleAlias = convertSQLToSampleAlias(row);
            return sampleAlias;
        }
        private SampleAlias selectSamplesAliasById(int _id)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableSamplesAlias, true, _id, "id");
            SampleAlias sampleAlias;
            if (row == null) return null;
            sampleAlias = convertSQLToSampleAlias(row);
            return sampleAlias;
        }

        public SampleAlias insertSampleAlias(SampleAlias _sampleAlias)
        {
            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.db_config.sqlTableSamplesAlias);

            sqlRowToInsert = convertSampleAliasToSQL(sqlRowToInsert, _sampleAlias);
            sqlRowToInsert.setAttribute("id", id);

                if (bdd.insert(bdd.db_config.sqlTableSamplesAlias, sqlRowToInsert))
                {

                SampleAlias checkSampleA = selectSamplesAliasById(id);
                    if (checkSampleA != null)
                        return checkSampleA;
                }
            return null;
        }

        public SampleAlias updateSampleAlias(SampleAlias _sampleAlias)
        {
            int id = _sampleAlias.id;

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableSamplesAlias, true, id);
            if (sqlRowToUpdate == null) return null;

            sqlRowToUpdate = convertSampleAliasToSQL(sqlRowToUpdate, _sampleAlias);
            sqlRowToUpdate.setAttribute("id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableSamples, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, id);
            SampleAlias sampleUpdated = convertSQLToSampleAlias(sqlRowCheck);

            return sampleUpdated;
        }
        public SampleAlias deleteSampleAlias(int _idUser, int _idSample)
        {
            int _canControlSamples = 1;

            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, _idUser, "idUser", _idSample, "idSample");
                
            SampleAlias sampleA = convertSQLToSampleAlias(rowToDelete);

            if (_canControlSamples == 1)
                if (sampleA != null) { 
                    bdd.delete(bdd.db_config.sqlTableSamplesAlias, _idUser, "idUser", _idSample, "idSample");

                    SqlRow rowCheckIsDelete = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, _idUser, "idUser", _idSample, "idSample");

                    if (rowCheckIsDelete == null) return sampleA;

                    throw new DatabaseRequestException();
                }
                else
                    throw new ForbiddenException();
            else
                throw new UnauthorisedException();
        }

        #endregion

        #region ******** convertions ******** 

        private SampleAlias convertSQLToSampleAlias(SqlRow _sqlrow)
        {
            if (_sqlrow == null) return null;
                
            int idSample = Static.convertToInteger(_sqlrow.getAttribute("id").value);
            int idUser = Static.convertToInteger(_sqlrow.getAttribute("idUser").value);
            SampleAlias sample = new SampleAlias(idUser, idSample); 
            
            //test id
            string idTest = Static.convertToString(_sqlrow.getAttribute("id").value);
            if (!int.TryParse(idTest, out _)) return null;

            sample.color = Static.convertToString(_sqlrow.getAttribute("color").value);
            sample.idLogo = Static.convertToInteger(_sqlrow.getAttribute("idLogo").value);
            sample.name = Static.convertToString(_sqlrow.getAttribute("name").value);

            sample.idSample = Static.convertToInteger(_sqlrow.getAttribute("idSample").value);

            sample.sample = main.bddSamples.selectSample(sample.idSample);

            return sample;
        }
        private SqlRow convertSampleAliasToSQL(SqlRow _sqlDest, SampleAlias _smpl)
        {
            if (_sqlDest == null) return null;
            if (_smpl == null) return null;

            _sqlDest.setAttribute("idLogo", _smpl.idLogo);
            _sqlDest.setAttribute("idSample", _smpl.id);
            _sqlDest.setAttribute("color", _smpl.color);
            _sqlDest.setAttribute("name", _smpl.name);
            _sqlDest.setAttribute("idUser", _smpl.idUser);
            _sqlDest.setAttribute("id", _smpl.id);

            return _sqlDest;
        }

        #endregion
    }
}
