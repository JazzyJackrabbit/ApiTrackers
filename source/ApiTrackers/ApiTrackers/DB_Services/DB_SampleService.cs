
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
    public class DB_SampleService
    {
        SqlDatabase bdd;
        Main main;
        readonly SqlTable table;
        readonly SqlCommand command;

        public DB_SampleService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableSamples;
            command = table.command;
        }

        public int getLastId() { return command.ExecuteReaderMaxId(table.name); }
        public int getNextId() { return command.ExecuteReaderMaxId(table.name) + 1; }

        #region ******** public methods ********

        public List<Sample> selectSamples()
        {
            List<SqlRow> rows = command.Select().all();
            List<Sample> samples = new List<Sample>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                samples.Add(convertSQLToSample(row));
            return samples;
        }
        public Sample selectSample(int _id)
        {
            SqlRow row = command.Select().row(true, _id);
            Sample sample = convertSQLToSample(row);
            return sample;
        }

        public Sample insertSample(Sample _sampleModel)
        {
            //TODO if idUser => user => admin ?
            int _canControlSamples = 1;

            int id = getNextId();
            SqlRow sqlRowToInsert = convertSampleToSQL(_sampleModel, id);

            if (_canControlSamples == 1)
                if (command.Insert().insert(sqlRowToInsert))
                {
                    //int id2 = getLastId();      << check for remove this line
                    Sample checkSample = selectSample(id);
                    if (checkSample != null)
                    {
                        return checkSample;
                    }
                }
            return null;
        }

        public Sample updateSample(Sample _sampleModel, int id)
        {
            //TODO if idUser => user => admin ?
            int _canControlSamples = 1;

            SqlRow sqlRowToUpdate = command.Select().row(true, id);
            if (sqlRowToUpdate == null)
            {
                return null;
            }
            if (_canControlSamples != 1)
            {
                return null;
            }

            sqlRowToUpdate = convertSampleToSQL(_sampleModel);

            bool checkUpdateCorrectly = command.Update().update(sqlRowToUpdate, id);
            if (!checkUpdateCorrectly)
            {
                return null;
            }

            SqlRow sqlRowCheck = command.Select().row(true, id);
            Sample sampleUpdated = convertSQLToSample(sqlRowCheck);

            return sampleUpdated;
        }
        public Sample deleteSample(int _id, int _idUser)
        {
            //TODO if idUser => user => admin ?
            int _canControlSamples = 1;
            
            SqlRow rowToDelete = command.Select().row(true, _id);
            if (rowToDelete == null)
                throw new OwnException(); //todo put a notFoundException
            Sample sample = convertSQLToSample(rowToDelete);

            if (_canControlSamples == 1)
                if (sample != null)
                    command.Delete().delete(_id);
                else
                {
                throw new UnauthorisedException();
            }
            return sample;
        }

        #endregion

        #region ******** convertions ******** 

        private Sample convertSQLToSample(SqlRow _sqlrow)
        {
            if (_sqlrow == null) return null;
            try
            {
                Sample sample = new Sample();
                sample.id = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);

                //test id
                string idTest = Utils.ConvertToString(_sqlrow.GetAttribut("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                sample.id = Utils.ConvertToInteger(_sqlrow.GetAttribut("id").value);
                sample.color = Utils.ConvertToString(_sqlrow.GetAttribut("color").value);
                sample.idLogo = Utils.ConvertToInteger(_sqlrow.GetAttribut("idLogo").value);
                sample.linkSample = Utils.ConvertToString(_sqlrow.GetAttribut("linkSample").value);
                sample.name = Utils.ConvertToString(_sqlrow.GetAttribut("name").value);
                return sample;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertSampleToSQL(Sample _smpl, int _id=-1)
        {
            SqlRow sqlRowToInsert = new(table);

            try
            {
                if (_smpl == null) return null;

                sqlRowToInsert.SetAttribut("idLogo", _smpl.idLogo);
                sqlRowToInsert.SetAttribut("linkSample", _smpl.linkSample);
                sqlRowToInsert.SetAttribut("color", _smpl.color);
                sqlRowToInsert.SetAttribut("name", _smpl.name);
                if(_id >= 0)
                    sqlRowToInsert.SetAttribut("id", _id);
                else if(_smpl.id > 0)
                    sqlRowToInsert.SetAttribut("id", _smpl.id);


            }
            catch (Exception ex)
            {
                Console.WriteLine("convert<<Object>>ToSQL - err: " + ex);
                throw new OwnException();
            }
            return sqlRowToInsert;
        }

        #endregion
    }
}
