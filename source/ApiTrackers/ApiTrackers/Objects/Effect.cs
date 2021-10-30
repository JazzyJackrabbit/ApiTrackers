﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Effect
    {
        public Effect()
        {
            id = 0;
            name = "none";
        }
        public Effect(int _id)
        {
            id = _id;
            name = "new effect";
        }
        public Effect(int _id, string _effectName)
        {
            id = _id;
            name = _effectName;
        }

        [JsonProperty(PropertyName = "id")]
        public int id;

        [JsonProperty(PropertyName = "name")]
        public string name;
    }
}
