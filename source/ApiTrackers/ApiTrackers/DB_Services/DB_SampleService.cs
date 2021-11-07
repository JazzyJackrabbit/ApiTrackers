
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
        private int lastId = -1;
        SqlTable table;
        SqlCommand command;

        public DB_SampleService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableSamples;
            command = table.command;
            lastId = command.select().lastId(true);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<Sample> selectSamples(bool _selfOpenClose)
        {
            List<SqlRow> rows = command.select().all(_selfOpenClose);
            List<Sample> samples = new List<Sample>();
            if (rows == null) return null;
            foreach (SqlRow row in rows)
                samples.Add(convertSQLToSample(row));
            return samples;
        }
        public Sample selectSample(int _id, bool _selfOpenClose)
        {
            SqlRow row = command.select().row(true, _id, _selfOpenClose);
            Sample sample = convertSQLToSample(row);
            return sample;
        }

        public Sample insertSample(Sample _sampleModel, bool _selfOpenClose)
        {
            //TODO if idUser => user => admin ?
            int _canControlSamples = 1;

            command.connectOpen(_selfOpenClose);

            int id = getNextId();
            SqlRow sqlRowToInsert = new SqlRow(bdd.tableSamples, false);

            sqlRowToInsert = convertSampleToSQL(sqlRowToInsert, _sampleModel);
            sqlRowToInsert.setAttribute("id", id);

            if (_canControlSamples == 1)
                if (command.insert().insert(sqlRowToInsert, false))
                {
                    //int id2 = getLastId();      << check for remove this line
                    Sample checkSample = selectSample(id, false);
                    if (checkSample != null)
                    {
                        command.connectClose(_selfOpenClose);
                        return checkSample;
                    }
                }
            command.connectClose(_selfOpenClose);
            return null;
        }

        public Sample updateSample(Sample _sampleModel, int id, bool _selfOpenClose /*<<< TODO remove this parameter, is accessible by sampleModel*/ )
        {
            //TODO if idUser => user => admin ?
            int _canControlSamples = 1;

            command.connectOpen(_selfOpenClose);

            SqlRow sqlRowToUpdate = command.select().row(true, id, false);
            if (sqlRowToUpdate == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
            if (_canControlSamples != 1)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }

            sqlRowToUpdate = convertSampleToSQL(sqlRowToUpdate, _sampleModel);
            sqlRowToUpdate.setAttribute("id", id);

            bool checkUpdateCorrectly = command.update().update(sqlRowToUpdate, id, false);
            if (!checkUpdateCorrectly)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }

            SqlRow sqlRowCheck = command.select().row(true, id, false);
            Sample sampleUpdated = convertSQLToSample(sqlRowCheck);

            command.connectClose(_selfOpenClose);
            return sampleUpdated;
        }
        public Sample deleteSample(int _id, int _idUser, bool _selfOpenClose)
        {
            //TODO if idUser => user => admin ?
            int _canControlSamples = 1;

            command.connectOpen(_selfOpenClose);

            SqlRow rowToDelete = command.select().row(true, _id, false);
            Sample sample = convertSQLToSample(rowToDelete);

            if (_canControlSamples == 1)
                if (sample != null)
                {   // todo delete
                    command.delete().delete(_id, false);
                }
                else
                {
                    command.connectClose(_selfOpenClose);
                    throw new ForbiddenException();
                }
            else
            {
                command.connectClose(_selfOpenClose);
                throw new UnauthorisedException();
            }
            command.connectClose(_selfOpenClose);
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
