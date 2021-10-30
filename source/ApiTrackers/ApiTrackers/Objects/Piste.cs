using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Piste
    {
        [JsonProperty(PropertyName = "id")]
        public int id;
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "notes")]
        public List<Note> notes;
        [JsonProperty(PropertyName = "color")]
        public string color;
    }
}
