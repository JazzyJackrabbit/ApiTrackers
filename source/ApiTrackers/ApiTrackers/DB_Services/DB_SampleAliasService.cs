
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

namespace ApiSamples.Services
{
    public class DB_SampleAliasService
    {
        SqlDatabase bdd;
        Main main;
        SqlTable table;
        SqlCommand command;

        public DB_SampleAliasService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableSamplesAlias;
            command = table.command;
        }

        public int getLastId() { return command.ExecuteReaderMaxId(table.name); }
        public int getNextId() { return command.ExecuteReaderMaxId(table.name) + 1; }

        #region ******** public methods ********

        public List<SampleAlias> selectSamplesAliasByIdUser(int _idUser)
        {
            List<SqlRow> rows = command.Select().all(true, _idUser, "idUser");
            List<SampleAlias> samplesAlias = new List<SampleAlias>();
            if (rows == null)
            {
                return null;
            }
            foreach (SqlRow row in rows)
                samplesAlias.Add(convertSQLToSampleAlias(row));

            return samplesAlias;
        }
      
        public SampleAlias selectSampleAlias(int _idUser, int _idSample)
        {
            SqlRow row = command.Select().row(true, _idUser, "idUser", _idSample, "idSample");
            if (row == null)
            {
                return null;
            }
            SampleAlias sampleAlias = convertSQLToSampleAlias(row);

            return sampleAlias;
        }
        private SampleAlias selectSamplesAliasById(int _id)
        {
            SqlRow row = command.Select().row(true, _id, "id");
            SampleAlias sampleAlias;
            if (row == null)
            {
                return null;
            }
            sampleAlias = convertSQLToSampleAlias(row);

            return sampleAlias;
        }

        public SampleAlias insertSampleAlias(SampleAlias _sampleAlias)
        {
            try { 
                int id = getNextId();
                SqlRow sqlRowToInsert = new SqlRow(bdd.tableSamplesAlias, false);


                sqlRowToInsert = convertSampleAliasToSQL(sqlRowToInsert, _sampleAlias);
                sqlRowToInsert.SetAttribut("id", id);

                    if (command.Insert().insert(sqlRowToInsert))
                    {
                        SampleAlias checkSampleA = selectSamplesAliasById(id);
                            if (checkSampleA != null)
                                return checkSampleA;
                    }
                return null;
            }
            catch {
                throw new TODOEXCEPTION();
            }
        }

        public SampleAlias updateSampleAlias(SampleAlias _sampleAlias)
        {

            int id = _sampleAlias.id;

            SqlRow sqlRowToUpdate = command.Select().row(true, id);
            if (sqlRowToUpdate == null)
            {
                return null;
            }

            sqlRowToUpdate = convertSampleAliasToSQL(sqlRowToUpdate, _sampleAlias);
            sqlRowToUpdate.SetAttribut("id", id);

            bool checkUpdateCorrectly = command.Update().update(sqlRowToUpdate, id);
            if (!checkUpdateCorrectly)
            {
                return null;
            }

            SqlRow sqlRowCheck = command.Select().row(true, id);
            SampleAlias sampleUpdated = convertSQLToSampleAlias(sqlRowCheck);

            return sampleUpdated;
        }
        public SampleAlias deleteSampleAlias(int _idUser, int _idSample)
        {

            int _canControlSamples = 1;

            SqlRow rowToDelete = command.Select().row(true, _idUser, "idUser", _idSample, "idSample");
                
            SampleAlias sampleA = convertSQLToSampleAlias(rowToDelete);

            if (_canControlSamples == 1)
                if (sampleA != null) {
                    command.Delete().delete(_idUser, "idUser", _idSample, "idSample");

                    SqlRow rowCheckIsDelete = command.Select().row(true, _idUser, "idUser", _idSample, "idSample");

                    if (rowCheckIsDelete == null)
                    {
                        return sampleA;
                    }

                    throw new DatabaseRequestException();
                }
                else
                {
                    throw new ForbiddenException();
                }
            else
            {
                throw new UnauthorisedException();
            }
        }

        #endregion

        #region ******** convertions ******** 

        private SampleAlias convertSQLToSampleAlias(SqlRow _sqlrow)
        {


            if (_sqlrow == null)
            {
                return null;
            }
                
            int idSample = Static.convertToInteger(_sqlrow.GetAttribut("id").value);
            int idUser = Static.convertToInteger(_sqlrow.GetAttribut("idUser").value);
            SampleAlias sample = new SampleAlias(idUser, idSample); 
            
            //test id
            string idTest = Static.convertToString(_sqlrow.GetAttribut("id").value);
            if (!int.TryParse(idTest, out _))
            {
                return null;
            }

            sample.color = Static.convertToString(_sqlrow.GetAttribut("color").value);
            sample.idLogo = Static.convertToInteger(_sqlrow.GetAttribut("idLogo").value);
            sample.name = Static.convertToString(_sqlrow.GetAttribut("name").value);

            sample.idSample = Static.convertToInteger(_sqlrow.GetAttribut("idSample").value);

            sample.sample = main.bddSamples.selectSample(sample.idSample);

            return sample;
        }
        private SqlRow convertSampleAliasToSQL(SqlRow _sqlDest, SampleAlias _smpl)
        {
            if (_sqlDest == null) return null;
            if (_smpl == null) return null;

            _sqlDest.SetAttribut("idLogo", _smpl.idLogo);
            _sqlDest.SetAttribut("idSample", _smpl.idSample);
            _sqlDest.SetAttribut("color", _smpl.color);
            _sqlDest.SetAttribut("name", _smpl.name);
            _sqlDest.SetAttribut("idUser", _smpl.idUser);
            _sqlDest.SetAttribut("id", _smpl.id);

            return _sqlDest;
        }

        #endregion
    }
}
