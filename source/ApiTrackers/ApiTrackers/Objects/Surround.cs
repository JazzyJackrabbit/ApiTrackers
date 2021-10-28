using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Objects
{
    public class Surround
    {
        public int id;
        public List<Output> outputs = new List<Output>();  

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
        public int id;
        public double volume;

        public Output(double _volume)
        {
            volume = _volume;
        }
    }
}
