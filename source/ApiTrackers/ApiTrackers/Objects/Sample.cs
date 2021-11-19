using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Sample
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "linkSample")]
        public string linkSample { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string color { get; set; }
        [JsonProperty(PropertyName = "idLogo")]
        public int idLogo { get; set; }

        [JsonIgnore]
        public Stream streamModule;

        public Sample()
        {
            id = 1;
            name = "none";
            linkSample = null;
            color = null;
            idLogo = 0;
        }
        public Sample(int _id, string _nameSample, string _linkSample)
        {
            id = _id;
            name = _nameSample;
            linkSample = _linkSample;
            color = null;
            idLogo = 0;
        }
        public Sample(int _id, string _nameSample, string _linkSample, string _color, int _idLogo)
        {
            id = _id;
            name = _nameSample;
            linkSample = _linkSample;
            color = _color;
            idLogo = _idLogo;
        }
    }
}
