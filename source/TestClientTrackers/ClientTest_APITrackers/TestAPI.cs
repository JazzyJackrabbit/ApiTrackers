using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest_APITrackers
{
    class TestAPI
    {

        private MainWindow main;

        public TestAPI(MainWindow _main){
            main = _main;

            logLine("= = !! START TEST !! = = ");
           
            // test routes:

            logLine("= =  INSERT = = ");  // + SELECT + UPDATE + SELECT + DELETE + SELECT
            TestINSERT();

            // get from modArchive API:


        }

        


        private void logLine(string v)
        {
            main.logLine(v);
        }


        // JSONs


        JObject jsonUser = new JObject{
                new JProperty("mail", "imjazzyjackrabbit@gmail.com"),
                new JProperty("passwordHash", "password"),
                new JProperty("pseudo", "pseudoTest"),
                new JProperty("wantReceiveMails", 1),
                new JProperty("id", 000)
            };
        JObject jsonUser2 = new JObject{
                new JProperty("mail", "imjazzyjackrabbit@gmail.com"),
                new JProperty("passwordHash", "password"),
                new JProperty("pseudo", "pseudoTest2"),
                new JProperty("wantReceiveMails", 1),
                new JProperty("id", 000)
            };

        JObject jsonTracker = new JObject{
                new JProperty("id", 1),
                new JProperty("idUser", 000),
                new JProperty("artist", "artistTest"),
                new JProperty("title", "titleTest"),
                new JProperty("bpm", "100"),
                new JProperty("comments", "commentsTest"),
                new JProperty("coprightInformations", "test")
            };

        JObject jsonSample = new JObject{
                new JProperty("id", 1),
                new JProperty("idLogo", 1),
                new JProperty("name", "nameTestOriginal"),
                new JProperty("color", "colornameTestOriginal"),
                new JProperty("linkSample", @"http://185.98.139.76/ress/curryking%20-%2039%20-%20untitled.wav")
            };

        JObject jsonSampleAlias = new JObject{
                    new JProperty("idUser", 000),
                    new JProperty("idLogo", 1),
                    new JProperty("name", "nameTestALIAS"),
                    new JProperty("color", "colorTestALIAS"),
                    new JProperty("idSample", 1)
            };
        JObject jsonCell = new JObject
            {
                new JProperty("volume", "080"),
                new JProperty("frequence","085"),
                new JProperty("idTracker", 000),
                new JProperty("id", 1),
                new JProperty("idUser", 000),
                new JProperty("idSample", 000)
            };
        JObject jsonRightMusic = new JObject{
                    new JProperty("idTracker", 000),
                    new JProperty("idUser", 000),
                    new JProperty("right", "Read"),
            };

        public void TestINSERT()
        {
            
            JObject resultUser =
                new API(main).INSERT_User(jsonUser);
            int idUser = Convert.ToInt32(resultUser.GetValue("id").ToString());
            jsonUser.Property("id").Value = idUser;

            JObject resultUser2 =
                new API(main).INSERT_User(jsonUser2);
            int idUser2 = Convert.ToInt32(resultUser2.GetValue("id").ToString());
            jsonUser2.Property("id").Value = idUser2;

            jsonTracker.Property("idUser").Value = idUser;

            JObject resultTracker =
                new API(main).INSERT_Tracker(idUser, jsonTracker);
            int idTracker = Convert.ToInt32(resultTracker.GetValue("id").ToString());

            JObject resultSample = new API(main).INSERT_Sample(jsonSample);
            int idSample = Convert.ToInt32(resultSample.GetValue("id").ToString());

            jsonSampleAlias.Property("idSample").Value = idSample;
            jsonSampleAlias.Property("idUser").Value = idUser;

            JObject resultSampleAlias = new API(main).INSERT_SampleAlias(jsonSampleAlias);
            int idUserSA = Convert.ToInt32(resultSampleAlias.Property("idUser").Value);
            JObject samplej = (JObject)resultSampleAlias.Property("sample").Value;
            int idSampleSA = Convert.ToInt32(samplej.Property("id").Value);

            jsonCell.Property("idTracker").Value = idTracker;
            jsonCell.Property("idUser").Value = idUser;
            jsonCell.Property("idSample").Value = idSample;
            //jsonCell.Remove("id"); 

            JObject resultCell = new API(main).INSERT_Cell(idTracker, jsonCell);
            int idCell = Convert.ToInt32(resultCell.Property("id").Value);

            jsonRightMusic.Property("idTracker").Value = idTracker;
            jsonRightMusic.Property("idUser").Value = idUser2;

            JObject resultRM = new API(main).INSERT_RightMusic(jsonRightMusic);
            int idUser2RM = Convert.ToInt32(resultRM.Property("idUser").Value);
            int idTrackerRM = Convert.ToInt32(resultRM.Property("idTracker").Value);


            TestSELECT(
                idUser, idUser2, idTracker, idSample, idUserSA, idSampleSA, idCell, idUser2RM, idTrackerRM);

            TestUPDATE(
                idUser, idUser2, idTracker, idSample, idUserSA, idSampleSA, idCell, idUser2RM, idTrackerRM);

            TestSELECT(
                   idUser, idUser2, idTracker, idSample, idUserSA, idSampleSA, idCell, idUser2RM, idTrackerRM);
            
            TestDELETE(
                idUser, idUser2, idTracker, idSample, idUserSA, idSampleSA, idCell, idUser2RM, idTrackerRM);

            
        }



        public void TestSELECT(
            int idUser, 
            int idUser2,
            int idTracker,
            int idSample,
            int idUserSA,
            int idSampleSA,
            int idCell,
            int idUser2RM,
            int idTrackerRM
            )
        {
            new API(main).SELECT_Users();
            new API(main).SELECT_User(idUser);
            new API(main).SELECT_User(idUser2);

            new API(main).SELECT_Trackers(idUser);
            new API(main).SELECT_Tracker(idTracker, idUser);

            new API(main).SELECT_Samples();
            new API(main).SELECT_Sample(idSample);

            new API(main).SELECT_SamplesAlias(idUserSA);
            new API(main).SELECT_SampleAlias(idUserSA, idSampleSA);

            new API(main).SELECT_Cells(idTracker);
            new API(main).SELECT_Cell(idTracker, idCell);

            new API(main).SELECT_RightMusic(idUser2RM, idTrackerRM);
        }

        private void TestUPDATE(
            int idUser,
            int idUser2,
            int idTracker,
            int idSample,
            int idUserSA,
            int idSampleSA,
            int idCell,
            int idUser2RM,
            int idTrackerRM
            )
        {
            jsonUser.Property("pseudo").Value = "UPDATED";
            new API(main).UPDATE_User(jsonUser);

            jsonTracker.Property("title").Value = "UPDATED";
            jsonTracker.Property("bpm").Value = 115;
            jsonTracker.Property("id").Value = idTracker;
            jsonTracker.Property("idUser").Value = idUser;

            //TODO:  BUG
            // BUG
            // new API(main).UPDATE_Tracker(idUser, jsonTracker); 

            jsonSample.Property("name").Value = "UPDATED";
            jsonSample.Property("color").Value = "RED";
            jsonSample.Property("id").Value = idSample;

            new API(main).UPDATE_Sample(jsonSample);

            jsonSampleAlias.Property("name").Value = "UPDATED ALIAS";
            jsonSampleAlias.Property("color").Value = "BLUE";
            jsonSampleAlias.Property("idUser").Value = idUserSA;
            jsonSampleAlias.Property("idSample").Value = idSampleSA;

            //TODO:  BUG
            // BUG
            // new API(main).UPDATE_SampleAlias(jsonSampleAlias);

            jsonCell.Property("volume").Value = "0444";
            jsonCell.Property("frequence").Value = "15678";
            new API(main).UPDATE_Cell(idTracker, jsonCell);

            jsonRightMusic.Property("right").Value = "Edit";
            new API(main).UPDATE_RightMusic(jsonRightMusic);
        }

        private void TestDELETE(
            int idUser,
            int idUser2,
            int idTracker,
            int idSample,
            int idUserSA,
            int idSampleSA,
            int idCell,
            int idUser2RM,
            int idTrackerRM
            )
        {
            new API(main).DELETE_Cell(idTracker, idCell);

            //TODO: BUG
            //BUG
            //new API(main).DELETE_SampleAlias(idUserSA, idSampleSA);

            new API(main).DELETE_Sample(idSample);
            new API(main).DELETE_Tracker(idUser, idTracker);
            new API(main).DELETE_User(idUser);
        }



    }
}
