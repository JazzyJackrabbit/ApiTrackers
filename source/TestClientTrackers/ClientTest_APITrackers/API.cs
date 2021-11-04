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
    class API
    {
        static Uri ServerUrl = new Uri("https://localhost:44328/");

        MainWindow main;
        public API(MainWindow _main)
        {
            main = _main;
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
        public static JContainer SELECT_Cells(int _idTracker)
        {
            string url = ServerUrl + "Cells?idTracker=" + _idTracker, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public static JContainer SELECT_Cell(int _idTracker, int _idCell)
        {
            string url = ServerUrl + "Cells?idTracker=" + _idTracker + "&id=" + _idCell, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public static JObject INSERT_Cell(int _idTracker, JObject json)
        {
            string url = ServerUrl + "Cells", resp = "";
            Debug.WriteLine(">>> URL : " + url); 
            Debug.WriteLine(">>> JObject : " + json.ToString());
            HttpClient client = new HttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject DELETE_Cell(int idTracker, int idCell)
        {
            string url = ServerUrl + "Cells?idTracker=" + idTracker + "&id=" + idCell, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            HttpClient client = new HttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject UPDATE_Cell(int idTracker, JObject json)
        {
            string url = ServerUrl + "Cells", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            HttpClient client = new HttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
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

        internal static JContainer SELECT_Tracker(int idTracker, int idUser)
        {
            string url = ServerUrl + "Trackers/" + idTracker; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        internal static JContainer SELECT_Trackers(int idUser)
        {
            string url = ServerUrl + "Trackers"; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        

        internal static JObject INSERT_Tracker(int idUser, JObject json)
        {
            string url = ServerUrl + "Trackers"; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            var response = new HttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject DELETE_Tracker(int idUser, int idTracker)
        {
            string url = ServerUrl + "Trackers?idUser="+idUser+"&id="+ idTracker; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject UPDATE_Tracker(int idUser, JObject json)
        {
            string url = ServerUrl + "Trackers"; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            var response = new HttpClient().PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
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


        public static JContainer SELECT_User(int _idUser)
        {
            string url = ServerUrl + "Users/" + _idUser, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public static JContainer SELECT_Users()
        {
            string url = ServerUrl + "Users", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        internal static JObject INSERT_User(JObject json)
        {
            string url = ServerUrl + "Users"; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            var response = new HttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject UPDATE_User(JObject json)
        {
            string url = ServerUrl + "Users", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            HttpClient client = new HttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }
        internal static JObject DELETE_User(int idUser)
        {
            string url = ServerUrl + "Users?id=" + idUser; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
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

        public static JContainer SELECT_Sample(int _id)
        {
            string url = ServerUrl + "Samples/" + _id, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public static JContainer SELECT_Samples()
        {
            string url = ServerUrl + "Samples", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        internal static JObject INSERT_Sample(JObject json)
        {
            string url = ServerUrl + "Samples"; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            var response = new HttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject UPDATE_Sample(JObject json)
        {
            string url = ServerUrl + "Samples", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            HttpClient client = new HttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }
        internal static JObject DELETE_Sample(int _id)
        {
            string url = ServerUrl + "Samples?id=" + _id; string resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().DeleteAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
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


        public static JContainer SELECT_RightMusic(int _idUser, int _idTracker)
        {
            string url = ServerUrl + "RightMusics?idUser=" + _idUser+ "&idTracker="+ _idTracker, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public static JContainer SELECT_RightMusics_byTracker(int _idTracker)
        {
            string url = ServerUrl + "RightMusics?idTracker=" + _idTracker, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }
        public static JContainer SELECT_RightMusics_byUser(int _idUser)
        {
            string url = ServerUrl + "RightMusics?idUser=" + _idUser, resp = "";
            Debug.WriteLine(">>> URL : " + url);
            var response = new HttpClient().GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            object v = JsonConvert.DeserializeObject(
               ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
            if (v.GetType() == typeof(JArray)) return (JArray)v;
            return (JObject)v;
        }

        internal static JObject INSERT_RightMusic(JObject json )
        {
            string url = ServerUrl + "RightMusics", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            var response = new HttpClient().PostAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
              ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }

        internal static JObject UPDATE_RightMusic(JObject json)
        {
            string url = ServerUrl + "RightMusics", resp = "";
            Debug.WriteLine(">>> URL : " + url);
            Debug.WriteLine(">>> JObject : " + json.ToString());
            HttpClient client = new HttpClient(); client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = client.PutAsJsonAsync(url, json).Result;
            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode + ": " + response.ReasonPhrase);
            resp = response.Content.ReadAsStringAsync().Result;
            return (JObject)JsonConvert.DeserializeObject(
                ((JObject)JsonConvert.DeserializeObject(resp)).Properties().Where((j) => j.Name == "response").ToArray()[0].Value.ToString());
        }


    }
}
