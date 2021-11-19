using SharpMod.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ApiTrackers.Objects;
using Newtonsoft.Json.Linq;

namespace ApiTrackers
{
    public class Tracker
    {
        public Tracker()
        {
            trackerContent = new TrackerContent();
            trackerMetadata = new TrackerMetadata();

            trackerContent.pistes = new List<Piste>();

            trackerContent.pistes.Add(new Piste());
            trackerContent.pistes[0].id = 1;
            trackerContent.pistes[0].name = "default";
            trackerContent.pistes[0].color = "#FFFFFF";
            trackerContent.pistes[0].notes = new List<Note>();
        }

        [JsonProperty(PropertyName = "metadata")]
        public TrackerMetadata trackerMetadata { get; set; }
        [JsonProperty(PropertyName = "content")]
        public TrackerContent trackerContent { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int idTracker { get; set; }
        [JsonProperty(PropertyName = "idUser")]
        public int idUser { get; set; }
    }
    public class TrackerMetadata
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "artist")]
        public string artist { get; set; }
        [JsonProperty(PropertyName = "copyrightInformation")]
        public string copyrightInformation { get; set; }
        [JsonProperty(PropertyName = "comments")]
        public string comments { get; set; }
    }
    public class TrackerContent
    {
        [JsonProperty(PropertyName = "bpm")]
        public double BPM { get; set; }
        [JsonIgnore]
        public IList<Piste> pistes { get; set; }


        [JsonProperty(PropertyName = "notes", NullValueHandling = NullValueHandling.Include)]
        private IList<Note> notes { get; set; }
/*        public JArray noteArray { 
            get {
                string json = Static.ArrayToJsonString_Converter((new Note()).GetType(), pistes[0].notes);
                return (JArray)JsonConvert.DeserializeObject(json);
            } set {
                string json = Static.ArrayToJsonString_Converter((new Note()).GetType(), pistes[0].notes);
                noteArray = (JArray)JsonConvert.DeserializeObject(json);
                *//*JArray ja = new JArray();
                foreach (Piste p in pistes)
                    ja.Add(p);*//*
            }
        }*/
    }
}
