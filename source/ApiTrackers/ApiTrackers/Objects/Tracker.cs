using SharpMod.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ApiTrackers.Objects;

namespace ApiTrackers
{
    public class Tracker
    {

        [JsonProperty(PropertyName = Static.JsonClassIndicator + "metadata")]
        public TrackerMetadata trackerMetadata = new TrackerMetadata();
        [JsonProperty(PropertyName = Static.JsonClassIndicator + "content")]
        public TrackerContent trackerContent = new TrackerContent();
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
        [JsonProperty(PropertyName = "cells")]
        public IList<Piste> pistes;
    }
}
