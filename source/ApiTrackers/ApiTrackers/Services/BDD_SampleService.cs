
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

            lastId = main.bdd.selectLastID(bdd.sqlTableSamples);
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
            SqlRow row = bdd.select(bdd.sqlTableSamples, true, _id);
            Sample sample = convertSQLToSample(row);
            return sample;
        }

        public Sample insertSample(Sample _sampleModel)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.sqlTableSamples);

            //definition data
            bdd.setAttribute(sqlRowToInsert, "idLogo", _sampleModel.idLogo);
            bdd.setAttribute(sqlRowToInsert, "linkSample", _sampleModel.linkSample);
            bdd.setAttribute(sqlRowToInsert, "color", _sampleModel.color);
            bdd.setAttribute(sqlRowToInsert, "name", _sampleModel.name);

            bdd.setAttribute(sqlRowToInsert, "id", id);

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

            SqlRow sqlRowToUpdate = bdd.select(bdd.sqlTableSamples, true, id);
            if (sqlRowToUpdate == null) return null;

            if (_canControlSamples != 1) return null;

            //definition data
            bdd.setAttribute(sqlRowToUpdate, "idLogo", _sampleModel.idLogo);
            bdd.setAttribute(sqlRowToUpdate, "linkSample", _sampleModel.linkSample);
            bdd.setAttribute(sqlRowToUpdate, "color", _sampleModel.color);
            bdd.setAttribute(sqlRowToUpdate, "name", _sampleModel.name);

            bdd.setAttribute(sqlRowToUpdate, "id", id);

            bool checkUpdateCorrectly = bdd.update(bdd.sqlTableSamples, sqlRowToUpdate, id);
            if (!checkUpdateCorrectly) return null;

            SqlRow sqlRowCheck = bdd.select(bdd.sqlTableSamples, true, id);
            Sample sampleUpdated = convertSQLToSample(sqlRowCheck);

            return sampleUpdated;
        }
        public Sample deleteSample(int _id, int _idUser)
        {
            //TODO //TODO //TODO
            int _canControlSamples = 1;

            SqlRow rowToDelete = bdd.select(bdd.sqlTableSamples, true, _id);
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
            try
            {
                Sample sample = new Sample();
                sample.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "id").value);

                //test id
                string idTest = Static.convertToString(bdd.getAttribute(_sqlrow, "id").value);
                if (!int.TryParse(idTest, out _)) return null;

                sample.id = Static.convertToInteger(bdd.getAttribute(_sqlrow, "id").value);
                sample.color = Static.convertToString(bdd.getAttribute(_sqlrow, "color").value);
                sample.idLogo = Static.convertToInteger(bdd.getAttribute(_sqlrow, "idLogo").value);
                sample.linkSample = Static.convertToString(bdd.getAttribute(_sqlrow, "linkSample").value);
                sample.name = Static.convertToString(bdd.getAttribute(_sqlrow, "name").value);
                return sample;
            }
            catch (Exception ex)
            {
                Console.WriteLine("BDDService - convertSQLToSample - err: " + ex);
                return null;
            }
        }

        #endregion
    }
}
