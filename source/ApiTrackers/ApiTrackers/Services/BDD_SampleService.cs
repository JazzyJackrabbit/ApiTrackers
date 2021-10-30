
using ApiTrackers;
using ApiTrackers.Exceptions;
using ApiTrackers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiTrackers.BDD_MainService;

namespace ApiSamples.Services
{
    public class BDD_SampleService
    {
        BDD_MainService bdd;
        MainService main;
        private int lastId = -1;

        public BDD_SampleService(MainService _main)
        {
            main = _main;
            bdd = _main.bdd;

            lastId = bdd.sqlTableSamples.selectLastID(_main);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Sample> selectSamples()
        {
            List<SqlRow> rows = bdd.select(bdd.sqlTableSamples, true);
            List<Sample> samples = new List<Sample>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                samples.Add(convertSQLToSample(row));
            return samples;
        }
        public Sample selectSample(int _id)
        {
            SqlRow row = bdd.selectOnlyRow(bdd.sqlTableSamples, true, _id);
            Sample sample = convertSQLToSample(row);
            return sample;
        }

        public Sample insertSample(Sample _sampleModel)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableSamples);

            sqlRowToInsert = convertSampleToSQL(sqlRowToInsert, _sampleModel);
            sqlRowToInsert.setAttribute("id", id);

            if (_canControlSamples == 1)
                if (bdd.insert(bdd.sqlTableSamples, sqlRowToInsert))
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

            SqlRow sqlRowToUpdate = bdd.selectOnlyRow(bdd.sqlTableSamples, true, id);
            if (sqlRowToUpdate == null) return null;

            if (_canControlSamples != 1) return null;

            sqlRowToUpdate = convertSampleToSQL(sqlRowToUpdate, _sampleModel);
            sqlRowToUpdate.setAttribute("id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableSamples, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.selectOnlyRow(bdd.sqlTableSamples, true, id);
            Sample sampleUpdated = convertSQLToSample(sqlRowCheck);

            return sampleUpdated;
        }
        public Sample deleteSample(int _id, int _idUser)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            SqlRow rowToDelete = bdd.selectOnlyRow(bdd.sqlTableSamples, true, _id);
            Sample sample = convertSQLToSample(rowToDelete);
            
            if (_canControlSamples == 1)
                if (sample != null)   
                    bdd.delete(bdd.sqlTableSamples, _id); 
                else
                    throw new ForbiddenException();
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
                sample.id = Static.convertToInteger(_sqlrow.getAttribute("id").value);

                //test id
                string idTest = Static.convertToString(_sqlrow.getAttribute("id").value);
                if (!int.TryParse(idTest, out _)) return null;

                sample.id = Static.convertToInteger(_sqlrow.getAttribute("id").value);
                sample.color = Static.convertToString(_sqlrow.getAttribute("color").value);
                sample.idLogo = Static.convertToInteger(_sqlrow.getAttribute("idLogo").value);
                sample.linkSample = Static.convertToString(_sqlrow.getAttribute("linkSample").value);
                sample.name = Static.convertToString(_sqlrow.getAttribute("name").value);
                return sample;
            }
            catch (Exception ex)
            {
                Console.WriteLine("convertSQLTo<<Object>> - err: " + ex);
                return null;
            }
        }
        private SqlRow convertSampleToSQL(SqlRow _sqlDest, Sample _smpl)
        {
            try
            {
                if (_sqlDest == null) return null;
                if (_smpl == null) return null;

                _sqlDest.setAttribute("idLogo", _smpl.idLogo);
                _sqlDest.setAttribute("linkSample", _smpl.linkSample);
                _sqlDest.setAttribute("color", _smpl.color);
                _sqlDest.setAttribute("name", _smpl.name);
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
