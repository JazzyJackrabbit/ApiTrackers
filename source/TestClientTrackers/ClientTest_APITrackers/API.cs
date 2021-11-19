using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest_APITrackers
{
    public class API
    {
        static Uri ServerUrl;

        MainWindow main;
        public API(MainWindow _main, string server = "https://localhost:5001/")
        {
            main = _main;
            ServerUrl = new Uri(main.tb_server.Text);

        }

        public HttpClient newHttpClient()
        {
            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "blabla123");
            return c;
        }

        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        // CELL
        public JContainer SELECT_Cells(int _idTracker)
        {
            
            string url = ServerUrl + "Cells?idTracker=" + _idTracker, resp = "";
            main.logLine(">>> URL : " + url); 
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

       
        public JContainer SELECT_Cell(int _idTracker, int _idCell)
        {
            string url = ServerUrl + "Cells?idTracker=" + _idTracker + "&id=" + _idCell, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }


        public JObject INSERT_Cell(int _idTracker, JObject json)
        {
            string url = ServerUrl + "Cells", resp = "";
            main.logLine(">>> URL : " + url); 
            main.logLine(">>> JObject : " + json.ToString());
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject DELETE_Cell(int idTracker, int idCell)
        {
            string url = ServerUrl + "Cells?idTracker=" + idTracker + "&id=" + idCell, resp = "";
            main.logLine(">>> URL : " + url);
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }


        internal JObject UPDATE_Cell(int idTracker, JObject json)
        {
            string url = ServerUrl + "Cells", resp = "";
            main.logLine(">>> URL : " + url);
            main.logLine(">>> JObject : " + json.ToString());
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }


        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER
        // TRACKER

        internal JContainer SELECT_Tracker(int idTracker, int idUser)
        {
            string url = ServerUrl + "Trackers/" + idTracker; string resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        internal JContainer SELECT_Trackers(int idUser)
        {
            string url = ServerUrl + "Trackers"; string resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        

        internal JObject INSERT_Tracker(int idUser, JObject json)
        {
            string url = ServerUrl + "Trackers"; string resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            var response = newHttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject DELETE_Tracker(int idUser, int idTracker)
        {
            string url = ServerUrl + "Trackers?idUser="+idUser+"&id="+ idTracker; string resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject UPDATE_Tracker(int idUser, JObject json)
        {
            string url = ServerUrl + "Trackers"; string resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            var response = newHttpClient().PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 
        // USER 


        public JContainer SELECT_User(int _idUser)
        {
            string url = ServerUrl + "Users/" + _idUser, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public JContainer SELECT_Users()
        {
            string url = ServerUrl + "Users", resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        internal JObject INSERT_User(JObject json)
        {
            string url = ServerUrl + "Users"; string resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            var response = newHttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject UPDATE_User(JObject json)
        {
            string url = ServerUrl + "Users", resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }
        internal JObject DELETE_User(int idUser)
        {
            string url = ServerUrl + "Users?id=" + idUser; string resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 
        // SAMPLES 

        public JContainer SELECT_Sample(int _id)
        {
            string url = ServerUrl + "Samples/" + _id, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public JContainer SELECT_Samples()
        {
            string url = ServerUrl + "Samples", resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        internal JObject INSERT_Sample(JObject json)
        {
            string url = ServerUrl + "Samples"; string resp = "";
            main.logLine(">>> JObject : " + json.ToString());
            main.logSuite(">>> URL : " + url);
            var response = newHttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject UPDATE_Sample(JObject json)
        {
            string url = ServerUrl + "Samples", resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }
        internal JObject DELETE_Sample(int _id)
        {
            string url = ServerUrl + "Samples?id=" + _id; string resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 
        // RightMusic 


        public JContainer SELECT_RightMusic(int _idUser, int _idTracker)
        {
            string url = ServerUrl + "RightMusics?idUser=" + _idUser+ "&idTracker="+ _idTracker, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public JContainer SELECT_RightMusics_byTracker(int _idTracker)
        {
            string url = ServerUrl + "RightMusics?idTracker=" + _idTracker, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public JContainer SELECT_RightMusics_byUser(int _idUser)
        {
            string url = ServerUrl + "RightMusics?idUser=" + _idUser, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        internal JObject INSERT_RightMusic(JObject json )
        {
            string url = ServerUrl + "RightMusics", resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            var response = newHttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject UPDATE_RightMusic(JObject json)
        {
            string url = ServerUrl + "RightMusics", resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }


        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS
        // SAMPLE ALIAS

        internal JContainer SELECT_SamplesAlias(int idUser)
        {
            string url = ServerUrl + "SamplesAlias?idUser=" + idUser, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        internal JObject INSERT_SampleAlias(JObject json)
        {
            string url = ServerUrl + "SamplesAlias", resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            var response = newHttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); };
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject DELETE_SampleAlias(int idUser, int idSample)
        {
            string url = ServerUrl + "SamplesAlias?idUser=" + idUser + "&idSample=" + idSample, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JObject UPDATE_SampleAlias(JObject json)
        {
            string url = ServerUrl + "RightMusics", resp = "";
            main.logLine(">>> URL : " + url);
            main.logSuite(">>> JObject : " + json.ToString());
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal JContainer SELECT_SampleAlias(int idUser, int idSample)
        {
            string url = ServerUrl + "SamplesAlias?idUser=" + idUser + "&idSample=" + idSample, resp = "";
            main.logLine(">>> URL : " + url);
            var response = newHttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode){main.logErr(response.Content.ReadAsStream().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);}
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD
        // UPLOAD

        internal JObject downloadSamples(int idModArchive)
        {
            string url = ServerUrl + @"upload/ModArchive/Samples/"+ idModArchive, resp = "";
            main.logLine(">>> URL : " + url);
            HttpClient client = newHttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PostAsJsonAsync(url, new JObject()).Result;
            if (!response.IsSuccessStatusCode) { main.logErr(new StreamReader(response.Content.ReadAsStream()).ReadToEnd().ToString()); throw new Exception(response.StatusCode + ": " + response.ReasonPhrase); }
            resp = response.Content.ReadAsStringAsync().Result;
            main.logSuite(">>> RESP : " + resp);
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }


    }
}
