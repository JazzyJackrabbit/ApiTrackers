
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
    public class DB_SampleService
    {
        DB_MainService bdd;
        MainService main;

        public DB_SampleService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;
        }

        public int getLastId() { return bdd.db_config.sqlTableSamples.selectLastID(main); }
        public int getNextId() { return bdd.db_config.sqlTableSamples.selectLastID(main) + 1; }

        #region ******** public methods ********

        public List<Sample> selectSamples()
        {
            List<SqlRow> rows = bdd.select(bdd.db_config.sqlTableSamples, true);
            List<Sample> samples = new List<Sample>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                samples.Add(convertSQLToSample(row));
            return samples;
        }
        public Sample selectSample(int _id)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, _id);
            Sample sample = convertSQLToSample(row);
            return sample;
        }

        public Sample insertSample(Sample _sampleModel)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            int id = getNextId();

            SqlRow sqlRowToInsert = convertSampleToSQL(_sampleModel, id);

            if (_canControlSamples == 1)
                if (bdd.insert(bdd.db_config.sqlTableSamples, sqlRowToInsert))
                {
                    int id2 = getLastId();
                    Sample checkSample = selectSample(id2);
                    if (checkSample != null)
                        return checkSample;
                }
            return null;
        }

        public Sample updateSample(Sample _sampleModel, int id)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, id);
            if (sqlRowToUpdate == null) return null;

            if (_canControlSamples != 1) return null;

            sqlRowToUpdate = convertSampleToSQL(_sampleModel);

            bool checkUpdateCorrectly = bdd.update(bdd.db_config.sqlTableSamples, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, id);
            Sample sampleUpdated = convertSQLToSample(sqlRowCheck);

            return sampleUpdated;
        }
        public Sample deleteSample(int _id, int _idUser)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.db_config.sqlTableSamples, true, _id);
            if (rowToDelete == null)
                throw new OwnException(); //todo put a notFoundException
            Sample sample = convertSQLToSample(rowToDelete);
            
            if (_canControlSamples == 1)
                if (sample != null)   
                    bdd.delete(bdd.db_config.sqlTableSamples, _id); 
            else
                throw new UnauthorisedException();
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
                sample.id = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);

                //test id
                string idTest = Utils.ConvertToString(_sqlrow.getAttribute("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                sample.id = Utils.ConvertToInteger(_sqlrow.getAttribute("id").value);
                sample.color = Utils.ConvertToString(_sqlrow.getAttribute("color").value);
                sample.idLogo = Utils.ConvertToInteger(_sqlrow.getAttribute("idLogo").value);
                sample.linkSample = Utils.ConvertToString(_sqlrow.getAttribute("linkSample").value);
                sample.name = Utils.ConvertToString(_sqlrow.getAttribute("name").value);
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
            SqlRow sqlRowToInsert = new SqlRow(bdd.db_config.sqlTableSamples);

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
