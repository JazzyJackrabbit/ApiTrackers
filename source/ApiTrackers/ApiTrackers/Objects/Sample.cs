using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Sample
    {
        [JsonProperty(PropertyName = "id")]
        public int id;
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "linkSample")]
        public string linkSample;

        [JsonProperty(PropertyName = "color")]
        public string color;
        [JsonProperty(PropertyName = "idLogo")]
        public int idLogo;

        [JsonIgnore]
        public int localInstrumentId = 0;

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
