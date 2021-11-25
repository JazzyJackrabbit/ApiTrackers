
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
        public int getNextId() {
            int id = command.ExecuteReaderMaxId(table.name);
            return   id == 0 ? 0 : id + 1; 
        }

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
            int id = getNextId();

            SqlRow sqlRowToInsert = convertSampleAliasToSQL(_sampleAlias, id);

            if (command.Insert().insert(sqlRowToInsert))
            {
                SampleAlias checkSampleA = selectSamplesAliasById(id);
                    if (checkSampleA != null)
                        return checkSampleA;
            }
            return null;
        }

        public SampleAlias updateSampleAlias(SampleAlias _sampleAlias)
        {


            SqlRow sqlRowToUpdate = command.Select().row(true, _sampleAlias.idUser, "idUser",_sampleAlias.idSample, "idSample");
            if (sqlRowToUpdate == null)
            {
                return null;
            }
            sqlRowToUpdate = convertSampleAliasToSQL(_sampleAlias);

            bool checkUpdateCorrectly = command.Update().update(sqlRowToUpdate, _sampleAlias.idUser, "idUser", _sampleAlias.idSample, "idSample");
            if (!checkUpdateCorrectly)
            {
                return null;
            }

            SqlRow sqlRowCheck = command.Select().row(true,_sampleAlias.idUser, "idUser", _sampleAlias.idSample, "idSample");
            SampleAlias sampleUpdated = convertSQLToSampleAlias(sqlRowCheck);

            return sampleUpdated;
        }
        public SampleAlias deleteSampleAlias(int _idSample, int _idUserTarget, int _idUser)
        {

            bool _canControlSamples = false;

            _canControlSamples = main.bddUser.selectUser(_idUser).adminMode == User.AdminMode.SuperAdmin
                || _idUser == _idUserTarget;

            SqlRow rowToDelete = command.Select().row(true, _idUser, "idUser", _idSample, "idSample");
                
            SampleAlias sampleA = convertSQLToSampleAlias(rowToDelete);

            if (_canControlSamples)
                if (sampleA != null) {
                    command.Delete().delete(_idUserTarget, "idUser", _idSample, "idSample");

                    SqlRow rowCheckIsDelete = command.Select().row(true, _idUserTarget, "idUser", _idSample, "idSample");

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
                
            int idSample = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);
            int idUser = Utils.ConvertToInteger(_sqlrow.GetAttribut("idUser").value);
            SampleAlias sample = new SampleAlias(idUser, idSample); 
            
            //test id
            string idTest = Utils.ConvertToString(_sqlrow.GetAttribut("id").value);
            if (!int.TryParse(idTest, out _))
            {
                return null;
            }

            sample.color = Utils.ConvertToString(_sqlrow.GetAttribut("color").value);
            sample.idLogo = Utils.ConvertToInteger(_sqlrow.GetAttribut("idLogo").value);
            sample.name = Utils.ConvertToString(_sqlrow.GetAttribut("name").value);

            sample.idSample = Utils.ConvertToInteger(_sqlrow.GetAttribut("idSample").value);

            sample.sample = main.bddSamples.selectSample(sample.idSample);

            return sample;
        }
        private SqlRow convertSampleAliasToSQL(SampleAlias _smpl, int _id=-1)
        {
            SqlRow _sqlDest = new SqlRow(bdd.tableSamplesAlias, false);

            if (_smpl == null) return null;

            _sqlDest.SetAttribut("idLogo", _smpl.idLogo);
            _sqlDest.SetAttribut("idSample", _smpl.idSample);
            _sqlDest.SetAttribut("color", _smpl.color);
            _sqlDest.SetAttribut("name", _smpl.name);
            _sqlDest.SetAttribut("idUser", _smpl.idUser);
            if(_smpl.id >= 0)
                _sqlDest.SetAttribut("id", _smpl.id);
            if(_id >= 0)
                _sqlDest.SetAttribut("id", _id);

            return _sqlDest;
        }

        #endregion
    }
}
