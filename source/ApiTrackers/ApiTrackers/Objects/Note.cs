﻿using ApiTrackers.Objects;
using Newtonsoft.Json;

namespace ApiTrackers
{
    public class Note
    {
        [JsonProperty(PropertyName = "id")]
        public int id;

        [JsonIgnore]
        public Tracker parentTracker;

        [JsonProperty(PropertyName = "idTracker", NullValueHandling = NullValueHandling.Include)]
        public int idTracker { get { return parentTracker.idTracker; } set { idTracker = parentTracker.idTracker; } }

        [JsonProperty(PropertyName = "position")]
        public double position;
        [JsonIgnore]
        public Sample sample;
        [JsonProperty(PropertyName = "frequence")]
        public double freqSample;
        [JsonProperty(PropertyName = "volume")]
        public double volume;
        [JsonIgnore]
        public Surround surround;

        [JsonIgnore]
        public Effect effect;
        [JsonIgnore]
        public Piste piste;

        [JsonProperty(PropertyName = "idSample", NullValueHandling = NullValueHandling.Include)]
        public int idSample { get { return sample.id; } set { idSample = sample.id; } }

        [JsonProperty(PropertyName = "idEffect", NullValueHandling = NullValueHandling.Include)]
        public int idEffect { get { return effect.id; } set { idEffect = effect.id; } }

        [JsonProperty(PropertyName = "idPiste", NullValueHandling = NullValueHandling.Include)]
        public int idPiste { get { return piste.id; } set { idPiste = piste.id; } }

        [JsonProperty(PropertyName = "key")]
        public string key;


        public Note(Tracker _parentTracker, Piste _parentPiste, double _position, string _key)
        {
            parentTracker = _parentTracker;
            piste                 = _parentPiste;
            position = _position;
            freqSample = 1;
            volume = 1;
            effect = new Effect();
            sample = new Sample();
            surround = new Surround().setStereo();
            key = _key;
        }

        public Note()
        {
            parentTracker = new Tracker();
            piste                 = new Piste();
            position = 0;
            freqSample = 1;
            volume = 1;
            effect = new Effect();
            sample = new Sample();
            surround = new Surround().setStereo();
          
            key = "C-5";
        }

    }
}