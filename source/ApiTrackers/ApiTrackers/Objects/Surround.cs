using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Surround
    {

        [JsonIgnore]
        public int id { get; set; }

        [JsonIgnore]
        public List<Output> outputs { get; set; } = new List<Output>();  

        public Surround setStereo()
        {
            outputs = new List<Output>();
            outputs.Add(new Output(1));
            outputs.Add(new Output(1));
            return this;
        }
    }
    public class Output
    {

        [JsonIgnore]
        public int id;

        [JsonIgnore]
        public double volume;

        public Output(double _volume)
        {
            volume = _volume;
        }
    }
}
