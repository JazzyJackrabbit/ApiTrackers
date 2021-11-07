
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
        private int lastId = -1;
        SqlTable table;
        SqlCommand command;

        public DB_SampleAliasService(Main _main)
        {
            main = _main;
            bdd = _main.bdd;
            table = bdd.tableSamplesAlias;
            command = table.command;
            lastId = command.select().lastId(true);
        }

        public int getLastId() { return lastId; }
        public int getNextId() { lastId++; return lastId; }

        #region ******** public methods ********

        public List<SampleAlias> selectSamplesAliasByIdUser(int _idUser, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);
            List<SqlRow> rows = command.select().all(true, _idUser, "idUser", false);
            List<SampleAlias> samplesAlias = new List<SampleAlias>();
            if (rows == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
            foreach (SqlRow row in rows)
                samplesAlias.Add(convertSQLToSampleAlias(row, false));

            command.connectClose(_selfOpenClose);
            return samplesAlias;
        }
      
        public SampleAlias selectSampleAlias(int _idUser, int _idSample, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);
            SqlRow row = command.select().row(true, _idUser, "idUser", _idSample, "idSample", false);
            if (row == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
            SampleAlias sampleAlias = convertSQLToSampleAlias(row, false);

            command.connectClose(_selfOpenClose);
            return sampleAlias;
        }
        private SampleAlias selectSamplesAliasById(int _id, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);
            SqlRow row = command.select().row(true, _id, "id", false);
            SampleAlias sampleAlias;
            if (row == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
            sampleAlias = convertSQLToSampleAlias(row, false);

            command.connectClose(_selfOpenClose);
            return sampleAlias;
        }

        public SampleAlias insertSampleAlias(SampleAlias _sampleAlias, bool _selfOpenClose)
        {
            bdd.connectOpen(_selfOpenClose);
            try { 
                int id = getNextId();
                SqlRow sqlRowToInsert = new SqlRow(bdd.tableSamplesAlias, false);


                sqlRowToInsert = convertSampleAliasToSQL(sqlRowToInsert, _sampleAlias);
                sqlRowToInsert.setAttribute("id", id);

                    if (command.insert().insert(sqlRowToInsert, false))
                    {
                        SampleAlias checkSampleA = selectSamplesAliasById(id, false);
                            if (checkSampleA != null)
                                return checkSampleA;
                    }
                return null;
            }
            catch {
                throw new TODOEXCEPTION();
            }
            finally
            {
                bdd.connectClose(_selfOpenClose);
            }
        }

        public SampleAlias updateSampleAlias(SampleAlias _sampleAlias, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);

            int id = _sampleAlias.id;

            SqlRow sqlRowToUpdate = command.select().row(true, id, false);
            if (sqlRowToUpdate == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }

            sqlRowToUpdate = convertSampleAliasToSQL(sqlRowToUpdate, _sampleAlias);
            sqlRowToUpdate.setAttribute("id", id);

            bool checkUpdateCorrectly = command.update().update(sqlRowToUpdate, id, false);
            if (!checkUpdateCorrectly)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }

            SqlRow sqlRowCheck = command.select().row(true, id, false);
            SampleAlias sampleUpdated = convertSQLToSampleAlias(sqlRowCheck, false);

            command.connectClose(_selfOpenClose);
            return sampleUpdated;
        }
        public SampleAlias deleteSampleAlias(int _idUser, int _idSample, bool _selfOpenClose)
        {
            command.connectOpen(_selfOpenClose);

            int _canControlSamples = 1;

            SqlRow rowToDelete = command.select().row(true, _idUser, "idUser", _idSample, "idSample", false);
                
            SampleAlias sampleA = convertSQLToSampleAlias(rowToDelete, false);

            if (_canControlSamples == 1)
                if (sampleA != null) {
                    command.delete().delete(_idUser, "idUser", _idSample, "idSample", false);

                    SqlRow rowCheckIsDelete = command.select().row(true, _idUser, "idUser", _idSample, "idSample", false);

                    if (rowCheckIsDelete == null)
                    {
                        command.connectClose(_selfOpenClose);
                        return sampleA;
                    }

                    throw new DatabaseRequestException();
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
        }

        #endregion

        #region ******** convertions ******** 

        private SampleAlias convertSQLToSampleAlias(SqlRow _sqlrow, bool _selfOpenClose)
        {

            command.connectOpen(_selfOpenClose);

            if (_sqlrow == null)
            {
                command.connectClose(_selfOpenClose);
                return null;
            }
                
            int idSample = Static.convertToInteger(_sqlrow.getAttribute("id").value);
            int idUser = Static.convertToInteger(_sqlrow.getAttribute("idUser").value);
            SampleAlias sample = new SampleAlias(idUser, idSample); 
            
            //test id
            string idTest = Static.convertToString(_sqlrow.getAttribute("id").value);
            if (!int.TryParse(idTest, out _))
            {
                command.connectClose(_selfOpenClose);
                return null;
            }

            sample.color = Static.convertToString(_sqlrow.getAttribute("color").value);
            sample.idLogo = Static.convertToInteger(_sqlrow.getAttribute("idLogo").value);
            sample.name = Static.convertToString(_sqlrow.getAttribute("name").value);

            sample.idSample = Static.convertToInteger(_sqlrow.getAttribute("idSample").value);

            sample.sample = main.bddSamples.selectSample(sample.idSample, false);

            command.connectClose(_selfOpenClose);
            return sample;
        }
        private SqlRow convertSampleAliasToSQL(SqlRow _sqlDest, SampleAlias _smpl)
        {
            if (_sqlDest == null) return null;
            if (_smpl == null) return null;

            _sqlDest.setAttribute("idLogo", _smpl.idLogo);
            _sqlDest.setAttribute("idSample", _smpl.idSample);
            _sqlDest.setAttribute("color", _smpl.color);
            _sqlDest.setAttribute("name", _smpl.name);
            _sqlDest.setAttribute("idUser", _smpl.idUser);
            _sqlDest.setAttribute("id", _smpl.id);

            return _sqlDest;
        }

        #endregion
    }
}
