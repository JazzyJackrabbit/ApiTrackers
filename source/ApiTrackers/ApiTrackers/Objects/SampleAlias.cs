using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class SampleAlias
    {
        [JsonIgnore]
        public int id;
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "idUser")]
        public int idUser;
        [JsonProperty(PropertyName = "color")]
        public string color;
        [JsonProperty(PropertyName = "idLogo")]
        public int idLogo;
        [JsonProperty(PropertyName = "sample")]
        public Sample sample;
        [JsonIgnore]
        public int idSample;

        public SampleAlias(int _idUser, int _idSample)
        {
            idSample = _idSample;
            idUser = _idUser;
            sample = new Sample();
            name = "none";
            color = null;
            idLogo = 0;
        }
        public SampleAlias(int _idUser, int _idSample, string _nameSample, string _color, int _idLogo)
        {
            idSample = _idSample;
            idUser = _idUser;
            sample = new Sample();
            name = _nameSample;
            color = _color;
            idLogo = _idLogo;
        }
        public SampleAlias(int _idUser, Sample _sample, string _nameSample, string _color, int _idLogo)
        {
            if(_sample != null)
            idSample = _sample.id;
            idUser = _idUser;
            sample = _sample;
            name = _nameSample;
            color = _color;
            idLogo = _idLogo;
        }

        public static Type get_type()
        {
            return new SampleAlias(0,0).GetType();
        }
    }
}
