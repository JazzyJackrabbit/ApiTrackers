
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
        public TrackerMetadata trackerMetadata;
        [JsonProperty(PropertyName = "content")]
        public TrackerContent trackerContent;
        [JsonProperty(PropertyName = "id")]
        public int idTracker;
        [JsonProperty(PropertyName = "idUser")]
        public int idUser;
    }
    public class TrackerMetadata
    {
        [JsonProperty(PropertyName = "title")]
        public string title;
        [JsonProperty(PropertyName = "artist")]
        public string artist;
        [JsonProperty(PropertyName = "copyrightInformation")]
        public string copyrightInformation;
        [JsonProperty(PropertyName = "comments")]
        public string comments;
    }
    public class TrackerContent
    {
        [JsonProperty(PropertyName = "bpm")]
        public string BPM;
        [JsonIgnore]
        public IList<Piste> pistes { get; set; }

        [JsonProperty(PropertyName = "notes", NullValueHandling = NullValueHandling.Include)]
        public JArray noteArray { 
            get {
                return Static.ConvertToJArray<Note>(pistes[0].notes);
            } set {
                noteArray = Static.ConvertToJArray<Note>(pistes[0].notes);
            }
        }
    }
}
